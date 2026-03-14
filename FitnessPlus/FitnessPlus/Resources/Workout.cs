using Android.App;
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
            FindViewById<ImageButton>(Resource.Id.btnStartWalking).Click += (s, e) => { };
            FindViewById<ImageButton>(Resource.Id.btnStartRunning).Click += (s, e) => { };
            FindViewById<ImageButton>(Resource.Id.btnStartHiking).Click += (s, e) => { };
            FindViewById<ImageButton>(Resource.Id.btnStartBasketball).Click += (s, e) => { };
            FindViewById<ImageButton>(Resource.Id.btnStartCycling).Click += (s, e) => { };
        }
    }
}