using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class Source_type_info
    {
        private int _source_id;
        private string _source_title;
        private DateTime _source_time;
        /// <summary>
        /// 来源类型编号
        /// </summary>
         [KeyAttribute]
        public int source_id
        {
            set { _source_id = value; }
            get { return _source_id; }
        }
        /// <summary>
        /// 来源类型标题
        /// </summary>
        public string source_title
        {
            set { _source_title = value; }
            get { return _source_title; }
        }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime source_time
        {
            set { _source_time = value; }
            get { return _source_time; }
        }
    }
}