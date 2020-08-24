using System;
using Xamarin.Forms;

namespace XamCustomControl
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();

            SliderLabelControl.ValueChanged += (s, e) =>
            {
                Console.WriteLine(e.NewValue);
            };
        }
    }
}
