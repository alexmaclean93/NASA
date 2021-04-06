using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASA.Models
{
    public class Root
    {
        public List<DayModel> data { get; set; }
    }

    public class DayModel
    {
        public string apod_site { get; set; }
        public string copyright { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public string hdurl { get; set; }
        public string media_type { get; set; }
        public string title { get; set; }
        public string url { get; set; }
    }
}
