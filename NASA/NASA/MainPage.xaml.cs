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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NASA
{
    public sealed partial class MainPage : Page
    {

        public ViewModels.DaysViewModel DaysVM { get; set; }
        public Root DayModel { get; set; }
        
        private string startDateStr { get; set; }
        private string endDateStr { get; set; }
        private bool startDateSet = false;
        private bool endDateSet = false;

        public MainPage()
        {
            this.InitializeComponent();

            this.DaysVM = new ViewModels.DaysViewModel(this);

            DayModel = new Root();

            datePickStart.MinYear = new DateTime(1995, 1, 1);
            datePickStart.MaxYear = DateTime.Now;

            datePickEnd.MinYear = new DateTime(1995, 1, 1);
            datePickEnd.MaxYear = DateTime.Now;
        }

       
        //Go to the about page
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        //Exit the program
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void datePickStart_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {

            // NEED TO CHECK FOR CORRECT START DATE (NOT BEFORE 1995-06-16)

            var date = args.NewDate;

            startDateStr = date.Value.Year.ToString() + "-" +
                date.Value.Month.ToString() + "-" +
                date.Value.Day.ToString();

            startDateSet = true;

            if (startDateSet && endDateSet)
            {
                DaysVM.Days.Clear();
                // GET IMAGES/DAYS
                DaysVM.LoadImages(startDateStr, endDateStr);
            }           
        }

        private void datePickEnd_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {

            // NEED TO CHECK FOR CORRECT END DATE (NOT BEFORE START DATE)

            var date = args.NewDate;

            endDateStr = date.Value.Year.ToString() + "-" +
                date.Value.Month.ToString() + "-" +
                date.Value.Day.ToString();

            endDateSet = true;

            if (startDateSet && endDateSet)
            {
                DaysVM.Days.Clear();
                // GET IMAGES/DAYS
                DaysVM.LoadImages(startDateStr, endDateStr);
            }

            
        }
    }
}
