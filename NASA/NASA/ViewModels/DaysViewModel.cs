using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NASA.Models;
using NASA.Repositories;

namespace NASA.ViewModels
{
    public class DaysViewModel : INotifyPropertyChanged
    {
        #region Variable Definitions
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<DaysModel> Days { get; set; }
        public string apod_site { get; set; }
        public string copyright { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public string hdurl { get; set; }
        public string media_type { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        private DaysModel _selectedDay;

        public DaysModel selectedDay
        {
            get { return _selectedDay; }
            set
            {
                _selectedDay = value;
                if (value != null)
                {
                    apod_site = value.apod_site;
                    copyright = value.copyright;
                    date = value.date;
                    description = value.description;
                    hdurl = value.hdurl;
                    media_type = value.media_type;
                    title = value.title;
                    url = value.url;
                }

                // Raise property chaned event


            }
        }
        #endregion

        public DaysViewModel()
        {
            Days = new ObservableCollection<DaysModel>();

            DaysModel tempDay = new DaysModel();

            tempDay.apod_site = "https://apod.nasa.gov/apod/ap181009.html";
            tempDay.copyright = "Hubble Legacy Archive, NASA, ESA; Processing & Copyright: Domingo Pestana & Raul Villaverde";
            tempDay.date = "2018-10-09";
            tempDay.description = "Many spiral galaxies have bars across their centers. Even our own Milky Way Galaxy is thought to have a modest central bar. Prominently barred spiral galaxy NGC 1672, featured here, was captured in spectacular detail in an image taken by the orbiting Hubble Space Telescope. Visible are dark filamentary dust lanes, young clusters of bright blue stars, red emission nebulas of glowing hydrogen gas, a long bright bar of stars across the center, and a bright active nucleus that likely houses a supermassive black hole. Light takes about 60 million years to reach us from NGC 1672, which spans about 75,000 light years across. NGC 1672, which appears toward the constellation of the Dolphinfish (Dorado), is being studied to find out how a spiral bar contributes to star formation in a galaxy's central regions.";
            tempDay.hdurl = "https://apod.nasa.gov/apod/image/1810/NGC1672_Hubble_3600.jpg";
            tempDay.media_type = "image";
            tempDay.title = "NGC 1672: Barred Spiral Galaxy from Hubble";
            tempDay.url = "https://apod.nasa.gov/apod/image/1810/NGC1672_Hubble_1080.jpg";

            this.Days.Add(tempDay);


            //LoadImages("2018-10-05", "2018-10-10");
        }

        public async void LoadImages(string startDate, string endDate)
        {
            this.Days = await NasaPicturesRepo.GetDateRange(startDate, endDate);
            string temp = "break point";
        }
    }
}
