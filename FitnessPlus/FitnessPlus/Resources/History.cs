using Android.App;
using Android.OS;
using Android.Widget;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FitnessPlus
{
    [Activity(Label = "Fitness Plus", Theme = "@style/Theme.AppCompat.NoActionBar")]
    public class History : Base
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.L_History);
            SetupBottomNav(Resource.Id.btnNavHistory);

            var container = FindViewById<LinearLayout>(Resource.Id.recordsContainer);
            string recordsFilePath = Path.Combine(FilesDir.AbsolutePath, "records.json");

            // 若記錄文件不存在，顯示提示文字後直接返回
            if (!File.Exists(recordsFilePath))
            {
                ShowEmpty(container);
                return;
            }

            string json = File.ReadAllText(recordsFilePath);
            var records = JsonConvert.DeserializeObject<List<WorkoutRecord>>(json);

            // 若記錄列表為空，顯示提示文字
            if (records == null || records.Count == 0)
            {
                ShowEmpty(container);
                return;
            }

            // 動態生成每筆記錄的卡片並加入容器
            foreach (var record in records)
            {
                var card = CreateRecordCard(record);
                container.AddView(card);
            }
        }

        // 動態建立單筆記錄的卡片視圖
        Android.Views.View CreateRecordCard(WorkoutRecord record)
        {
            // 外層卡片容器
            var card = new LinearLayout(this);
            card.Orientation = Orientation.Vertical;
            card.SetBackgroundColor(Android.Graphics.Color.ParseColor("#1A2A1A"));
            var cardParams = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MatchParent,
                LinearLayout.LayoutParams.WrapContent);
            cardParams.SetMargins(0, 0, 0, 12);
            card.LayoutParameters = cardParams;
            card.SetPadding(32, 24, 32, 24);

            // 第一行：運動類型（左）+ 日期（右）
            var row1 = new LinearLayout(this);
            row1.Orientation = Orientation.Horizontal;
            row1.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MatchParent,
                LinearLayout.LayoutParams.WrapContent);

            var tvType = new TextView(this);
            tvType.Text = record.WorkoutType;
            tvType.TextSize = 18f;
            tvType.SetTextColor(Android.Graphics.Color.ParseColor("#7FFF5A"));
            tvType.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
            tvType.LayoutParameters = new LinearLayout.LayoutParams(0,
                LinearLayout.LayoutParams.WrapContent, 1f);

            var tvDate = new TextView(this);
            tvDate.Text = record.Date;
            tvDate.TextSize = 14f;
            tvDate.SetTextColor(Android.Graphics.Color.ParseColor("#AAAAAA"));
            tvDate.Gravity = Android.Views.GravityFlags.End;

            row1.AddView(tvType);
            row1.AddView(tvDate);

            // 第二行：時長（左）+ 卡路里（右）
            var row2 = new LinearLayout(this);
            row2.Orientation = Orientation.Horizontal;
            var row2Params = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MatchParent,
                LinearLayout.LayoutParams.WrapContent);
            row2Params.SetMargins(0, 8, 0, 0);
            row2.LayoutParameters = row2Params;

            var tvDuration = new TextView(this);
            tvDuration.Text = "⏱ " + record.Duration;
            tvDuration.TextSize = 15f;
            tvDuration.SetTextColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            tvDuration.LayoutParameters = new LinearLayout.LayoutParams(0,
                LinearLayout.LayoutParams.WrapContent, 1f);

            var tvCalories = new TextView(this);
            tvCalories.Text = record.Calories + " kcal";
            tvCalories.TextSize = 15f;
            tvCalories.SetTextColor(Android.Graphics.Color.ParseColor("#42A5F5"));
            tvCalories.Gravity = Android.Views.GravityFlags.End;

            row2.AddView(tvDuration);
            row2.AddView(tvCalories);

            card.AddView(row1);
            card.AddView(row2);

            return card;
        }

        // 顯示無記錄提示
        void ShowEmpty(LinearLayout container)
        {
            var tv = new TextView(this);
            tv.Text = "No workout records yet.";
            tv.TextSize = 16f;
            tv.SetTextColor(Android.Graphics.Color.ParseColor("#AAAAAA"));
            tv.Gravity = Android.Views.GravityFlags.Center;
            tv.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MatchParent,
                LinearLayout.LayoutParams.WrapContent);
            container.AddView(tv);
        }
    }
}