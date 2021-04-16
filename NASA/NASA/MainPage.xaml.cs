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
using System.Collections.ObjectModel;
using NASA.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NASA
{
    public sealed partial class MainPage : Page
    {

        public ViewModels.DaysViewModel DaysVM { get; set; }
        public Root DayModel { get; set; }
        
        private DateTimeOffset? startDate { get; set; }
        private DateTimeOffset? endDate { get; set; }
        private string startDateStr { get; set; }
        private string endDateStr { get; set; }
        private bool startDateSet = false;
        private bool endDateSet = false;

        public MainPage()
        {
            this.InitializeComponent();

            this.DaysVM = new ViewModels.DaysViewModel(this);

            DayModel = new Root();

            datePickStart.MinYear = new DateTime(1996, 1, 1);
            datePickStart.MaxYear = DateTime.Now;

            datePickEnd.MinYear = new DateTime(1996, 1, 1);
            datePickEnd.MaxYear = DateTime.Now;
        }

        //Go to the details page
        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            String address = this.DaysVM.selectedDay.apod_site;
            String copyright = this.DaysVM.selectedDay.copyright;
            String date = this.DaysVM.selectedDay.date;
            String title = this.DaysVM.selectedDay.title;
            String description = this.DaysVM.selectedDay.description;
            String url = this.DaysVM.selectedDay.hdurl;

            String Param = string.Format("Name: " + title +
                "\nAddress: " + address +
                "\nDate: " + date +
                "\nDescription: " + description +
                "\nURL: " + url +
                "\nCopyright " + copyright);

            Frame.Navigate(typeof(Details), Param);
        }
       
        //Go to the about page
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        //Exit the program
        private async void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ExitDialog ed = new ExitDialog();
            ContentDialogResult result = await ed.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                System.Environment.Exit(0);
            }

        }

        private void datePickStart_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {

            var date = args.NewDate;

            startDate = date;

            startDateStr = date.Value.Year.ToString() + "-" +
                date.Value.Month.ToString() + "-" +
                date.Value.Day.ToString();

            startDateSet = true;

            if (startDateSet && endDateSet)
            {

                var range = endDate.Value.Year - 5;

                if (range > startDate.Value.Year)
                {
                    showError();
                } else
                {
                    DaysVM.Days.Clear();
                    // GET IMAGES/DAYS
                    DaysVM.LoadImages(startDateStr, endDateStr);
                }
                
                
            }           
        }

        private void datePickEnd_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {

            // NEED TO CHECK FOR CORRECT END DATE (NOT BEFORE START DATE)

            var date = args.NewDate;

            endDate = date;

            endDateStr = date.Value.Year.ToString() + "-" +
                date.Value.Month.ToString() + "-" +
                date.Value.Day.ToString();

            endDateSet = true;

            if (startDateSet && endDateSet)
            {

                var range = endDate.Value.Year - 5;

                if (range > startDate.Value.Year)
                {
                    showError();
                }
                else
                {
                    DaysVM.Days.Clear();
                    // GET IMAGES/DAYS
                    DaysVM.LoadImages(startDateStr, endDateStr);

                    // Make Details Button Accessible
                    DetailsBtn.Visibility = Visibility.Visible;
                }

                
            }
        }

        private async static void showError()
        {
            ContentDialog rangeErrorDialog= new ContentDialog()
            {
                Title = "Error",
                Content = "That range is too large, please select a smaller range",
                PrimaryButtonText = "OK"
            };
            await rangeErrorDialog.ShowAsync();
        }
    }
}
