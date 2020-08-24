using System;

using Xamarin.Forms;

namespace XamCustomControl
{
    public class SliderLabelControl : AbsoluteLayout
    {
        private Label _sliderValue;
        private Slider _slider;

        public SliderLabelControl()
        {
            AddControls();
        }

        private void AddControls()
        {
            _slider = new Slider { Margin = new Thickness(0, 20, 0, 0) };
            _slider.ValueChanged += (s, e) =>
            {
                PositionLabel(e.NewValue);
                Value = e.NewValue;
            };

            _sliderValue = new Label();
            _sliderValue.SetBinding(Label.TextProperty, new Binding("Value", source: _slider) { StringFormat = "{0:F0}" });

            SetLayoutFlags(_slider, AbsoluteLayoutFlags.All);
            SetLayoutBounds(_slider, new Rectangle(0f, 1f, 1f, 1f));

            Children.Add(_sliderValue);
            Children.Add(_slider);
        }

        private void PositionLabel(double newValue)
        {
            if (newValue == 0.0) return;

            var xPosition = (newValue - _slider.Minimum) / (_slider.Maximum - _slider.Minimum);

            SetLayoutFlags(_sliderValue, AbsoluteLayoutFlags.PositionProportional);
            SetLayoutBounds(_sliderValue, new Rectangle(xPosition, 0f, AutoSize, AutoSize));
        }

        public double Minimum
        {
            get => (double)_slider.GetValue(Slider.MinimumProperty);
            set => _slider.SetValue(Slider.MinimumProperty, value);
        }

        public double Maximum
        {
            get => (double)_slider.GetValue(Slider.MaximumProperty);
            set => _slider.SetValue(Slider.MaximumProperty, value);
        }

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create("Value", typeof(double), typeof(SliderLabelControl), 0.0, BindingMode.TwoWay, propertyChanging: HandlePropertyChanging);

        private static void HandlePropertyChanging(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is SliderLabelControl sliderLabelControl)
            {
                sliderLabelControl.PositionLabel((double)newValue);
                sliderLabelControl._slider.Value = (double)newValue;
            }
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set
            {
                var oldValue = Value;
                SetValue(ValueProperty, value);
                ValueChanged?.Invoke(this, new ValueChangedEventArgs(oldValue, value));
            }
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged;
    }
}