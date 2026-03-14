using Android.App;
using Android.Content;
using Android.OS;

namespace FitnessPlus
{
    [Activity(Label = "Fitness Plus", MainLauncher = true, Theme = "@style/Theme.AppCompat.NoActionBar")]
    public class MainActivity : AndroidX.AppCompat.App.AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // 跳到 Workout 頁面
            StartActivity(new Intent(this, typeof(Workout)));
            Finish();
        }
    }
}