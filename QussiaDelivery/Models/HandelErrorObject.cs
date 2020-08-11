﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QussiaDelivery.Models
{
    public class HandelErrorObject<T>
    {
        public T data { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
    }
}