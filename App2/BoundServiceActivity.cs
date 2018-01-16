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
using App2.Services;
using App2.Connections;

namespace App2
{
    [Activity(Label = "BoundServiceActivity")]
    public class BoundServiceActivity : Activity
    {
        Button timestampButton;
        Button stopServiceButton;
        Button restartServiceButton;
        internal TextView timestampMessageTextView;

        TimestampServiceConnection serviceConnection;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.BoundServiceDemo);

            timestampButton = FindViewById<Button>(Resource.Id.btnGetTimestamp);
            timestampButton.Click += GetTimestampButton_Click;

            stopServiceButton = FindViewById<Button>(Resource.Id.btnStopTimestamp);
            stopServiceButton.Click += StopServiceButton_Click;

            restartServiceButton = FindViewById<Button>(Resource.Id.btnRestartTimestampService);
            restartServiceButton.Click += RestartServiceButton_Click;

            timestampMessageTextView = FindViewById<TextView>(Resource.Id.etMessageDisplay);

        }

        protected override void OnStart()
        {
            base.OnStart();
            if (serviceConnection == null)
            {
                serviceConnection = new TimestampServiceConnection(this);
            }
            DoBindService();
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (serviceConnection.IsConnected)
            {
                UpdateUiForBoundService();
            }
            else
            {
                UpdateUiForUnboundService();
            }
        }

        protected override void OnPause()
        {
            timestampButton.Click -= GetTimestampButton_Click;
            stopServiceButton.Click -= StopServiceButton_Click;
            restartServiceButton.Click -= RestartServiceButton_Click;

            base.OnPause();
        }

        protected override void OnStop()
        {
            DoUnBindService();
            base.OnStop();
        }



        void GetTimestampButton_Click(object sender, System.EventArgs e)
        {
            if (serviceConnection.IsConnected)
            {
                timestampMessageTextView.Text = serviceConnection.Binder.Service.GetFormattedTimestamp();
            }
            else
            {
                timestampMessageTextView.SetText(Resource.String.service_not_connected);
            }
        }
        void StopServiceButton_Click(object sender, System.EventArgs e)
        {
            DoUnBindService();
            UpdateUiForUnboundService();

        }

        void RestartServiceButton_Click(object sender, System.EventArgs e)
        {
            DoBindService();
            UpdateUiForBoundService();
        }

        void DoBindService()
        {
            Intent serviceToStart = new Intent(this, typeof(TimestampService));
            BindService(serviceToStart, serviceConnection, Bind.AutoCreate);
            timestampMessageTextView.Text = "";
        }

        void DoUnBindService()
        {
            UnbindService(serviceConnection);
            restartServiceButton.Enabled = true;
            timestampMessageTextView.Text = "";
        }

        internal void UpdateUiForBoundService()
        {
            timestampButton.Enabled = true;
            stopServiceButton.Enabled = true;
            restartServiceButton.Enabled = false;

        }
        internal void UpdateUiForUnboundService()
        {
            timestampButton.Enabled = false;
            stopServiceButton.Enabled = false;
            restartServiceButton.Enabled = true;
        }
    }
}