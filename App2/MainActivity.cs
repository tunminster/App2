using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Content;
using Core;

namespace App2
{
    [Activity(Label = "App2", MainLauncher = true)]
    public class MainActivity : Activity
    {
        static readonly List<string> phoneNumbers = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            // Get UI Controls
            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.etPhoneNumber);
            TextView translatedPhoneWord = FindViewById<TextView>(Resource.Id.tvTranslatedPhoneWord);
            Button translateButton = FindViewById<Button>(Resource.Id.btnTranslate);
            Button translationHistoryButton = FindViewById<Button>(Resource.Id.btnTranslateHistory);
            Button goBoundServiceButton = FindViewById<Button>(Resource.Id.btnGoBoundService);

            // Add code to translate number
            translateButton.Click += (sender, e) =>
            {
                // Translate user’s alphanumeric phone number to numeric
                string translatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(translatedNumber))
                {
                    translatedPhoneWord.Text = string.Empty;
                }
                else
                {
                    translatedPhoneWord.Text = translatedNumber;
                    phoneNumbers.Add(translatedNumber);
                    translationHistoryButton.Enabled = true;
                }
            };

            translationHistoryButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(TranslationHistoryActivity));
                intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                StartActivity(intent);
            };

            goBoundServiceButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(BoundServiceActivity));
                StartActivity(intent);
            };
        }
    }
}

