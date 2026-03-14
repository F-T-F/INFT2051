using Android.Content;
using Android.Widget;

namespace FitnessPlus
{
    // 所有頁面都繼承這個，共用底部導覽列點擊邏輯
    public class Base : AndroidX.AppCompat.App.AppCompatActivity
    {
        protected void SetupBottomNav(int activeButtonId)
        {
            var btnWorkout = FindViewById<ImageButton>(Resource.Id.btnNavWorkout);
            var btnHistory = FindViewById<ImageButton>(Resource.Id.btnNavHistory);
            var btnInfo = FindViewById<ImageButton>(Resource.Id.btnNavInfo);

            btnWorkout.Click += (s, e) =>
            {
                if (activeButtonId != Resource.Id.btnNavWorkout)
                {
                    StartActivity(new Intent(this, typeof(Workout)));
                    Finish();
                }
            };

            btnInfo.Click += (s, e) =>
            {
                if (activeButtonId != Resource.Id.btnNavInfo)
                {
                    StartActivity(new Intent(this, typeof(Info)));
                    Finish();
                }
            };
        }
    }
}