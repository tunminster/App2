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

namespace App2.Services
{
    [Service(IsolatedProcess = true)]
    [IntentFilter(new String[] { "com.xamarin.DemoService" })]
    public class DemoService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}