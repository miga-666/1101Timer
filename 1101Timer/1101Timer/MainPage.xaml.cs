using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace _1101Timer
{

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();



            int count = 0;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {

                // do something every 60 seconds
                Device.BeginInvokeOnMainThread(() =>
                {
                    count++;
                    printCount.Text = count.ToString();
                    Console.WriteLine(count);
                    // interact with UI elements
                });
                return true; // runs again, or false to stop
            });
        }
    }
}
