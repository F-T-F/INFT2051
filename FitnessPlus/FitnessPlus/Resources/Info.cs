using Android.App;
using Android.OS;
using Android.Widget;

namespace FitnessPlus
{
    // Info：用於收集和保存用戶的個人健身資料
    [Activity(Label = "Fitness Plus", Theme = "@style/Theme.AppCompat.NoActionBar")]
    public class Info : Base
    {
        // 記錄當前選擇的性別，默認為男性
        bool isMale = true;

        Button btnMale;
        Button btnFemale;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // 設置佈局
            SetContentView(Resource.Layout.L_Info);

            // 高亮底部導航欄中的「資料」按鈕
            SetupBottomNav(Resource.Id.btnNavInfo);

            // 獲取 SharedPreferences，用於本地持久化存儲用戶數據
            var prefs = GetSharedPreferences("FitnessPlus", Android.Content.FileCreationMode.Private);

            // 綁定界面控件
            var etName = FindViewById<EditText>(Resource.Id.etName);
            var etHeight = FindViewById<EditText>(Resource.Id.etHeight);
            var etWeight = FindViewById<EditText>(Resource.Id.etWeight);
            var etAge = FindViewById<EditText>(Resource.Id.etAge);
            var btnSave = FindViewById<Button>(Resource.Id.btnSave);
            btnMale = FindViewById<Button>(Resource.Id.btnMale);
            btnFemale = FindViewById<Button>(Resource.Id.btnFemale);

            // 從本地存儲讀取已保存的用戶資料，並填入對應輸入框
            etName.Text = prefs.GetString("name", "");
            etHeight.Text = prefs.GetInt("height", 165).ToString();
            etWeight.Text = prefs.GetInt("weight", 55).ToString(); 
            etAge.Text = prefs.GetInt("age", 20).ToString(); 

            // 讀取已保存的性別，並更新按鈕顯示狀態
            isMale = prefs.GetBoolean("isMale", true);
            UpdateGenderButtons();

            // M
            btnMale.Click += (s, e) =>
            {
                isMale = true;
                UpdateGenderButtons();
            };

            // F
            btnFemale.Click += (s, e) =>
            {
                isMale = false;
                UpdateGenderButtons();
            };

            // save將用戶輸入資料寫入本地存儲
            btnSave.Click += (s, e) =>
            {
                var editor = prefs.Edit();

                // 保存姓名與性別
                editor.PutString("name", etName.Text);
                editor.PutBoolean("isMale", isMale);

                // 嘗試解析數字輸入，解析成功才保存，防止格式錯誤
                if (int.TryParse(etHeight.Text, out int h)) editor.PutInt("height", h);
                if (int.TryParse(etWeight.Text, out int w)) editor.PutInt("weight", w);
                if (int.TryParse(etAge.Text, out int a)) editor.PutInt("age", a);

                // 異步提交更改
                editor.Apply();

                Toast.MakeText(this, "Saved!", ToastLength.Short).Show();
            };
        }

        // 根據當前性別選擇，更新兩個性別按鈕的背景色和文字色，選中的按鈕高亮顯示，未選中的按鈕變為暗色
        void UpdateGenderButtons()
        {
            if (isMale)
            {
                // M按鈕綠（S）
                btnMale.SetBackgroundColor(Android.Graphics.Color.ParseColor("#7FFF5A"));
                btnMale.SetTextColor(Android.Graphics.Color.Black);

                // F按鈕暗（US）
                btnFemale.SetBackgroundColor(Android.Graphics.Color.ParseColor("#1A2A1A"));
                btnFemale.SetTextColor(Android.Graphics.Color.ParseColor("#AAAAAA"));
            }
            else
            {
                // F按鈕粉（S）
                btnFemale.SetBackgroundColor(Android.Graphics.Color.ParseColor("#F54275"));
                btnFemale.SetTextColor(Android.Graphics.Color.Black);

                // M按鈕暗（US）
                btnMale.SetBackgroundColor(Android.Graphics.Color.ParseColor("#1A2A1A"));
                btnMale.SetTextColor(Android.Graphics.Color.ParseColor("#AAAAAA"));
            }
        }
    }
}