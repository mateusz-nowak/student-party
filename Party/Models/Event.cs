using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Party.Models
{
    public class Event
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Text { set; get; }
    }
}