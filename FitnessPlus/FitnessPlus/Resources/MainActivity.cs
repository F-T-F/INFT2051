using Android.App;
using Android.Content;
using Android.OS;

namespace FitnessPlus
{
    // 將用戶立即重定向至主功能頁面Workout
    [Activity(Label = "Fitness Plus", MainLauncher = true, Theme = "@style/Theme.AppCompat.NoActionBar")]
    public class MainActivity : AndroidX.AppCompat.App.AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Workout作為應用的主頁面
            StartActivity(new Intent(this, typeof(Workout)));
            Finish();
        }
    }
}