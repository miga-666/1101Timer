using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;
using System.Timers;

namespace _1101Timer
{
    public partial class MainPage : ContentPage
    {
        public System.Timers.Timer aTimer; //定義全域的一個計時器
        public int count; //經過時間(秒)

        public MainPage()
        {
            //初始化
            InitializeComponent();
        }

        //Count_Clicked(): 確定倒數時間
        public void Count_Clicked(object sender, EventArgs esender)
        {
            aTimer = new System.Timers.Timer(1000); //1000毫秒 = 1 秒
            aTimer.Elapsed += OnTimeEvent;

            count = Int32.Parse(totalTime.Text);
            thetime.Text = totalTime.Text;
        }

        //Start_Clicked(): 開始 timer
        private void Start_Clicked(object sender, EventArgs e)
        {
            aTimer.Start();
        }
        //Stop_Clicked(): 暫停 timer
        private void Stop_Clicked(object sender, EventArgs e)
        {
            aTimer.Stop();
        }
        //Reset_Clicked(): 重設 timer
        private void Reset_Clicked(object sender, EventArgs e)
        {
            //Dispose()和Close()都可以
            aTimer.Close();
            count = Int32.Parse(totalTime.Text);
            thetime.Text = totalTime.Text;
        }
        //OnTimeEvent(): 觸發事件(當時間間隔到了, 要做什麼事)
        public void OnTimeEvent(Object source, ElapsedEventArgs e)
        {
            //xamarin特有的方法, 在裝置UI主執行緒上呼叫動作。
            Device.InvokeOnMainThreadAsync(() => {
                //每一秒減 1
                count--;
                thetime.Text = count.ToString();
                //Console.WriteLine(count);
                if (count == 0)
                {
                    //時間到 -> timer 停止 & 跳出通知
                     Stop();
                }
            });
        }
        private void Stop()
        {
            aTimer.Stop();
            aTimer.Dispose();

            DependencyService.Get<IAudio>().PlayAudioFile("grinch.mp3");
            Device.InvokeOnMainThreadAsync(async () => {
                await DisplayAlert("", "TIMEOUT!", null, "OK");
                DependencyService.Get<IAudio>().StopAudioFile();
            });
        }
    }
}
