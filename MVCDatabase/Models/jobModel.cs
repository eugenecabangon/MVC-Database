using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCDatabase.Models
{
    public class jobModel
    {
        public int jobID { get; set; }


        [DisplayName("Task Name")]
        public string taskName { get; set; }


        [DisplayName("Description")]
        public string description { get; set; }


        [DisplayName("Date Started")]
        public string dateStarted { get; set; }

        [DisplayName("Date Finished")]
        public string dateFinished { get; set; }
 
        [DisplayName("Status")]
        public string status { get; set; }
 
    }
}