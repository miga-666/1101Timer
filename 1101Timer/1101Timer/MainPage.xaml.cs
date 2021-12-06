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
        public System.Timers.Timer aTimer; //定義一個計時器
        public int count; //經過時間(秒)
        public MainPage()
        {
            InitializeComponent();

            aTimer = new System.Timers.Timer(1000); //1000毫秒 = 1 秒
            aTimer.Elapsed += OnTimeEvent;
            
        }

        public class StatusChecker
        {
            private int invokeCount;
            private int maxCount;
            public StatusChecker(int count)
            {
                invokeCount = 0;
                maxCount = count;
            }

            //啟動
            // This method is called by the timer delegate.
            public void CheckStatus(Object stateInfo)
            {
                AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;

                invokeCount++;
                //thetime.Text = invokeCount.ToString();

                if (invokeCount == maxCount)
                {
                    invokeCount = 0;
                    autoEvent.Set();
                }
            }
        }

        private void Start_Clicked(object sender, EventArgs e)
        {
            aTimer.Start();
            //Start(): aTimer.Enabled = true
        }
        //暫停
        private void Stop_Clicked(object sender, EventArgs e)
        {

            aTimer.Stop();
            //Stop(): aTimer.Enabled = false
        }
        //重設
        private void Reset_Clicked(object sender, EventArgs e)
        {
            //aTimer.Dispose();
            //Dispose()和Close()都可以
            aTimer.Close();
            count = 0; //時間歸零
            //thetime.Text = ConvertTime(count);
            thetime.Text = count.ToString();
        }
        //觸發事件(當時間間隔到了, 要做什麼事)
        public void OnTimeEvent(Object source, ElapsedEventArgs e)
        {

            //xamarin特有的方法, 在裝置UI主執行緒上呼叫動作。
            Device.InvokeOnMainThreadAsync(() =>
            {
                count++;
                thetime.Text = count.ToString();
            });
        }
    }
}
