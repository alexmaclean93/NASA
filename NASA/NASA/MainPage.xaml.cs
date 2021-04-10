using System;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using NASA.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NASA
{
    public sealed partial class MainPage : Page
    {

        public ViewModels.DaysViewModel DaysVM { get; set; }
        public Root dayModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            this.DaysVM = new ViewModels.DaysViewModel();

            dayModel = new Root();

            string dateString = "temp";


            datePickStart.MinYear = new DateTime(1995, 1, 1);
            datePickStart.MaxYear = DateTime.Now;

            datePickEnd.MinYear = new DateTime(1995, 1, 1);
            datePickEnd.MaxYear = DateTime.Now;
            GetDataFromAPI(dateString);
        }

        private async void GetDataFromAPI(string dateString)
        {
            
            // Example Date range string 
            //string url = "https://apodapi.herokuapp.com/api/?start_date=2020-01-01&end_date=2020-12-31";

            // Start with 50 random
            string url = "https://apodapi.herokuapp.com/api/?count=50";

            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<List<DaysModel>>(response);

            string temp = "Break point";
        }
    }
}
