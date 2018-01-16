using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App2.Core;
using App2.Services;

namespace App2.Binders
{
    public class TimestampBinder : Binder, IGetTimestamp
    {
        public TimestampBinder(TimestampService service)
        {
            this.Service = service;
        }

        public TimestampService Service { get; private set; }

        public string GetFormattedTimestamp()
        {
            return Service?.GetFormattedTimestamp();
        }
    }
}