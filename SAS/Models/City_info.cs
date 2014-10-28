using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class City_info
    {
        private string city_id;
            [KeyAttribute]
        public string City_id
        {
            get { return city_id; }
            set { city_id = value; }
        }
        private string city_name;

        public string City_name
        {
            get { return city_name; }
            set { city_name = value; }
        }
        private string province_id;

        public string Province_id
        {
            get { return province_id; }
            set { province_id = value; }
        }
    }
}