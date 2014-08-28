using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class TempAdvise
    {
        #region Model
        private int _id;
        private string _content;
        private string _contact;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string contact
        {
            set { _contact = value; }
            get { return _contact; }
        }
        #endregion Model

    }
}