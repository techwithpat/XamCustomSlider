using Xamarin.Forms;

namespace XamCustomControl
{
    internal class MainPageViewModel : BindableObject
    {
        private double sliderValue = 150;

        public double SliderValue
        {
            get => sliderValue;
            set
            {
                sliderValue = value;
                OnPropertyChanged();
            }
        }
    }
}