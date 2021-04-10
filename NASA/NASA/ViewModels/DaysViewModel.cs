using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NASA.Models;

namespace NASA.ViewModels
{
    public class DaysViewModel
    {
        public ObservableCollection<DaysModel> Days { get; set; }

        public DaysViewModel()
        {
            Days = new ObservableCollection<DaysModel>();
        }


    }
}
