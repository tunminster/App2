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
using Android.Util;

namespace App2.Services
{
    [Service]
    public class DemoIntentService : IntentService
    {
        static readonly string TAG = typeof(TimestampService).FullName;

        public DemoIntentService() : base("DemoIntentService")
        {

        }

        protected override void OnHandleIntent(Intent intent)
        {
            string fileToDownload = intent.GetStringExtra("file_to_download");
            Log.Debug(TAG, "Demo intent service started");

            Log.Debug(TAG, $"Received - {fileToDownload}");

        }
    }
}