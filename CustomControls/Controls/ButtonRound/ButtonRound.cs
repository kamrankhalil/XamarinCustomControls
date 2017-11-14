using System;
using Xamarin.Forms;

namespace CustomControls.Controls.ButtonRound
{
    public class ButtonRound : Button
    {
        public string Tag { get; set; } = string.Empty;

        public string ImageActive { get; set; } = string.Empty;
        public string ImageInactive { get; set; } = string.Empty;

        public int LeftInnerPadding { get; set; } = 0;
        public int TopInnerPadding { get; set; } = 0;
        public int RightInnerPadding { get; set; } = 0;
        public int BottomInnerPadding { get; set; } = 0;
        public int CornerRadius { get; set; } = 0;

        #region IsRoundedRightCorners
        public static readonly BindableProperty IsRoundedRightCornersProperty =
            BindableProperty.Create("IsRoundedRightCorners", typeof(bool), typeof(ButtonRound), true);

        public bool IsRoundedRightCorners
        {
            get => (bool)GetValue(IsRoundedRightCornersProperty);
            set => SetValue(IsRoundedRightCornersProperty, value);
        }
        #endregion IsRoundedRightCorners

        #region IsRoundedLeftCorners
        public static readonly BindableProperty IsRoundedLeftCornersProperty =
            BindableProperty.Create("IsRoundedLeftCorners", typeof(bool), typeof(ButtonRound), true);

        public bool IsRoundedLeftCorners
        {
            get => (bool)GetValue(IsRoundedLeftCornersProperty);
            set => SetValue(IsRoundedLeftCornersProperty, value);
        }
        #endregion IsRoundedLeftCorners

        #region Source
        public static readonly BindableProperty SourceProperty =
                BindableProperty.Create("Source", typeof(object), typeof(ButtonRound), null);

        public object Source
        {
            get => (object)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }
        #endregion Source
    }
}
