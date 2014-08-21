using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class DrrModes
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}