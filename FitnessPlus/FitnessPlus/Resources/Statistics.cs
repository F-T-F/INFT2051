using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.IO;
using System.Timers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FitnessPlus
{
    [Activity(Label = "Fitness Plus", Theme = "@style/Theme.AppCompat.NoActionBar")]
    public class Statistics : AndroidX.AppCompat.App.AppCompatActivity
    {
        string workoutType = "Walking";
        int totalSeconds = 0;
        bool isPaused = false;
        Timer timer;

        TextView tvWorkoutName;
        TextView tvTimer;
        TextView tvCalories;
        Button btnPause;
        Button btnEnd;

        // 記錄文件路徑：/data/data/com.FitnessPlus/files/records.json
        string recordsFilePath;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.L_Statistics);

            workoutType = Intent.GetStringExtra("workoutType") ?? "Walking";

            // 初始化文件路徑，FilesDir 指向應用的私有文件目錄
            recordsFilePath = Path.Combine(FilesDir.AbsolutePath, "records.json");

            tvWorkoutName = FindViewById<TextView>(Resource.Id.tvWorkoutName);
            tvTimer = FindViewById<TextView>(Resource.Id.tvTimer);
            tvCalories = FindViewById<TextView>(Resource.Id.tvCalories);
            btnPause = FindViewById<Button>(Resource.Id.btnPause);
            btnEnd = FindViewById<Button>(Resource.Id.btnEnd);

            tvWorkoutName.Text = workoutType;

            timer = new Timer(1000);
            timer.Elapsed += (s, e) =>
            {
                totalSeconds++;

                var prefs = GetSharedPreferences("FitnessPlus", Android.Content.FileCreationMode.Private);
                int weight = prefs.GetInt("weight", 60);

                double met = GetMet(workoutType);
                double hours = totalSeconds / 3600.0;
                double calories = met * weight * hours;

                RunOnUiThread(() =>
                {
                    tvTimer.Text = TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss");
                    tvCalories.Text = ((int)calories).ToString();
                });
            };
            timer.Start();

            btnPause.Click += (s, e) =>
            {
                if (!isPaused)
                {
                    timer.Stop();
                    isPaused = true;
                    btnPause.Text = "▶  Resume";
                }
                else
                {
                    timer.Start();
                    isPaused = false;
                    btnPause.Text = "⏸  Pause";
                }
            };

            btnEnd.Click += (s, e) =>
            {
                timer.Stop();
                SaveRecord();
                Finish();
            };
        }

        // 根據運動類型返回對應的 MET 值
        double GetMet(string type)
        {
            switch (type)
            {
                case "Walking": return 3.5;
                case "Running": return 8.0;
                case "Hiking": return 6.0;
                case "Basketball": return 8.0;
                case "Cycling": return 7.5;
                default: return 4.0;
            }
        }

        void SaveRecord()
        {
            var prefs = GetSharedPreferences("FitnessPlus", Android.Content.FileCreationMode.Private);
            int weight = prefs.GetInt("weight", 60);

            double met = GetMet(workoutType);
            double hours = totalSeconds / 3600.0;
            double calories = met * weight * hours;
            string duration = TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss");
            string date = DateTime.Now.ToString("dd/MM/yyyy");

            // 從 records.json 讀取現有記錄，若文件不存在則初始化空列表
            List<WorkoutRecord> records;
            if (File.Exists(recordsFilePath))
            {
                string existingJson = File.ReadAllText(recordsFilePath);
                records = JsonConvert.DeserializeObject<List<WorkoutRecord>>(existingJson)
                          ?? new List<WorkoutRecord>();
            }
            else
            {
                records = new List<WorkoutRecord>();
            }

            // 將新記錄插入列表最前面（最新的在最上面）
            records.Insert(0, new WorkoutRecord
            {
                WorkoutType = workoutType,
                Date = date,
                Duration = duration,
                Calories = (int)calories
            });

            // 將更新後的列表寫回 records.json
            File.WriteAllText(recordsFilePath, JsonConvert.SerializeObject(records, Formatting.Indented));
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            timer?.Stop();
            timer?.Dispose();
        }
    }

    public class WorkoutRecord
    {
        public string WorkoutType { get; set; }
        public string Date { get; set; }
        public string Duration { get; set; }
        public int Calories { get; set; }
    }
}