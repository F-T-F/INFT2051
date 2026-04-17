using Android.Content;
using Android.Widget;

namespace FitnessPlus
{
    // 所有頁面都使用
    public class Base : AndroidX.AppCompat.App.AppCompatActivity
    {
        // 初始化底部導覽列，並設置各按鈕的頁面跳轉邏輯
        // activeButtonId：當前頁面對應的導覽按鈕 ID，用於防止重複跳轉到同一頁面
        protected void SetupBottomNav(int activeButtonId)
        {
            // 底部dock按鈕
            var btnWorkout = FindViewById<ImageButton>(Resource.Id.btnNavWorkout);
            var btnHistory = FindViewById<ImageButton>(Resource.Id.btnNavHistory);
            var btnInfo = FindViewById<ImageButton>(Resource.Id.btnNavInfo);

            // 若當前不在Workout則跳轉
            btnWorkout.Click += (s, e) =>
            {
                if (activeButtonId != Resource.Id.btnNavWorkout)
                {
                    StartActivity(new Intent(this, typeof(Workout)));
                    Finish();
                }
            };

            btnHistory.Click += (s, e) =>
            {
                if (activeButtonId != Resource.Id.btnNavHistory)
                {
                    StartActivity(new Intent(this, typeof(History)));
                    Finish();
                }
            };

            // 若當前不在Info頁面則跳轉
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