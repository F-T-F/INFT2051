using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace FitnessPlus
{
    [Activity(Label = "Fitness Plus", Theme = "@style/Theme.AppCompat.NoActionBar")]
    public class Workout : Base
    {
        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.L_Workout);
            SetupBottomNav(Resource.Id.btnNavWorkout);

            // 點擊後跳Statistics頁面
            FindViewById<ImageButton>(Resource.Id.btnStartWalking).Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(Statistics));
                intent.PutExtra("workoutType", "Walking");
                StartActivity(intent);
            };

            FindViewById<ImageButton>(Resource.Id.btnStartRunning).Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(Statistics));
                intent.PutExtra("workoutType", "Running");
                StartActivity(intent);
            };

            FindViewById<ImageButton>(Resource.Id.btnStartHiking).Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(Statistics));
                intent.PutExtra("workoutType", "Hiking");
                StartActivity(intent);
            };

            FindViewById<ImageButton>(Resource.Id.btnStartBasketball).Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(Statistics));
                intent.PutExtra("workoutType", "Basketball");
                StartActivity(intent);
            };

            FindViewById<ImageButton>(Resource.Id.btnStartCycling).Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(Statistics));
                intent.PutExtra("workoutType", "Cycling");
                StartActivity(intent);
            };
        }
    }
}