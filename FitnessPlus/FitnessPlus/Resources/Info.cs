using Android.App;
using Android.OS;
using Android.Widget;

namespace FitnessPlus
{
    [Activity(Label = "Fitness Plus", Theme = "@style/Theme.AppCompat.NoActionBar")]
    public class Info : Base
    {
        bool isMale = true;
        Button btnMale;
        Button btnFemale;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.L_Info);
            SetupBottomNav(Resource.Id.btnNavInfo);

            var prefs = GetSharedPreferences("FitnessPlus", Android.Content.FileCreationMode.Private);
            var etName = FindViewById<EditText>(Resource.Id.etName);
            var etHeight = FindViewById<EditText>(Resource.Id.etHeight);
            var etWeight = FindViewById<EditText>(Resource.Id.etWeight);
            var etAge = FindViewById<EditText>(Resource.Id.etAge);
            var btnSave = FindViewById<Button>(Resource.Id.btnSave);
            btnMale = FindViewById<Button>(Resource.Id.btnMale);
            btnFemale = FindViewById<Button>(Resource.Id.btnFemale);

            etName.Text = prefs.GetString("name", "");
            etHeight.Text = prefs.GetInt("height", 170).ToString();
            etWeight.Text = prefs.GetInt("weight", 60).ToString();
            etAge.Text = prefs.GetInt("age", 20).ToString();
            isMale = prefs.GetBoolean("isMale", true);
            UpdateGenderButtons();

            btnMale.Click += (s, e) =>
            {
                isMale = true;
                UpdateGenderButtons();
            };

            btnFemale.Click += (s, e) =>
            {
                isMale = false;
                UpdateGenderButtons();
            };

            btnSave.Click += (s, e) =>
            {
                var editor = prefs.Edit();
                editor.PutString("name", etName.Text);
                editor.PutBoolean("isMale", isMale);
                if (int.TryParse(etHeight.Text, out int h)) editor.PutInt("height", h);
                if (int.TryParse(etWeight.Text, out int w)) editor.PutInt("weight", w);
                if (int.TryParse(etAge.Text, out int a)) editor.PutInt("age", a);
                editor.Apply();
                Toast.MakeText(this, "Saved!", ToastLength.Short).Show();
            };
        }

        void UpdateGenderButtons()
        {
            if (isMale)
            {
                btnMale.SetBackgroundColor(Android.Graphics.Color.ParseColor("#7FFF5A"));
                btnMale.SetTextColor(Android.Graphics.Color.Black);
                btnFemale.SetBackgroundColor(Android.Graphics.Color.ParseColor("#1A2A1A"));
                btnFemale.SetTextColor(Android.Graphics.Color.ParseColor("#AAAAAA"));
            }
            else
            {
                btnFemale.SetBackgroundColor(Android.Graphics.Color.ParseColor("#F54275"));
                btnFemale.SetTextColor(Android.Graphics.Color.Black);
                btnMale.SetBackgroundColor(Android.Graphics.Color.ParseColor("#1A2A1A"));
                btnMale.SetTextColor(Android.Graphics.Color.ParseColor("#AAAAAA"));
            }
        }
    }
}