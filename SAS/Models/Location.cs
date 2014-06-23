using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class Location
    {
        public object id;
        public object name;
        public Location(object id, object name)
        {
            this.id = id;
            this.name = name;
        }
    }
}