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
            LoadImages("2018-10-05", "2018-10-10");
        }

        public async void LoadImages(string startDate, string endDate)
        {
            this.Days = await NasaPicturesRepo.GetDateRange(startDate, endDate);
        }
    }
}
