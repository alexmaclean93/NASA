﻿using NASA.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using NASA.ViewModels;
using Windows.UI.Xaml.Media.Imaging;

namespace NASA.Repositories
{
    public static class NasaPicturesRepo
    {

        public async static Task GetDateRange(DaysViewModel daysVM, string startDate, string endDate)
        {

            string url = $"https://apodapi.herokuapp.com/api/?start_date={startDate}&end_date={endDate}";
            // Gets the JSON from the site.
            HttpClient http = new HttpClient();
            HttpResponseMessage response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            result = result.Replace("[", "").Replace("]", "");

            // Goes through each record and deserializes each one into a DayModel.
            foreach (string record in result.Split("},"))
            {
                if (!record.Contains("}"))
                {
                    DaysModel newDay = JsonConvert.DeserializeObject<DaysModel>(record + "}");

                    if (newDay.url != null && newDay.media_type == "image")
                    {
                        daysVM.Days.Add(newDay);
                    }

                    //daysVM.Days.Add(JsonConvert.DeserializeObject<DaysModel>(record + "}"));
                }

                else
                {
                    Debug.WriteLine(record);
                    //daysVM.Days.Add(JsonConvert.DeserializeObject<DaysModel>(record));

                    DaysModel newDay = JsonConvert.DeserializeObject<DaysModel>(record);

                    if (newDay.url != null && newDay.media_type == "image")
                    {
                        daysVM.Days.Add(newDay);
                    }
                }

            }
            return;
        }

        public static BitmapImage GetImage(DaysModel selectedDay)
        {
            BitmapImage bitmapImage = new BitmapImage();
            Uri uri = new Uri(selectedDay.url);
            bitmapImage.UriSource = uri;

            return bitmapImage;
        }


        // OLD, FOR REFERENCE

        //public async static Task<ObservableCollection<DaysModel>> GetDateRange(string startDate, string endDate)
        //{

        //    string url = $"https://apodapi.herokuapp.com/api/?start_date={startDate}&end_date={endDate}";
        //    ObservableCollection<DaysModel> pictureData = new ObservableCollection<DaysModel>();

        //    // Gets the JSON from the site.
        //    HttpClient http = new HttpClient();
        //    HttpResponseMessage response = await http.GetAsync(url);
        //    var result = await response.Content.ReadAsStringAsync();

        //    result = result.Replace("[", "").Replace("]", "");

        //    // Goes through each record and deserializes each one into a DayModel.
        //    foreach (string record in result.Split("},"))
        //    {
        //        if (!record.Contains("}"))
        //        {
        //            pictureData.Add(JsonConvert.DeserializeObject<DaysModel>(record + "}"));
        //        }

        //        else
        //        {
        //            Debug.WriteLine(record);
        //            pictureData.Add(JsonConvert.DeserializeObject<DaysModel>(record));
        //        }

        //    }
        //    return pictureData;
        //}
    }
}