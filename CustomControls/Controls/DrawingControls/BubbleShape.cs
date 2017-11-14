using System;
using Xamarin.Forms;

namespace CustomControls.Controls.DrawingControls
{
	[Flags]
	public enum BubblePointerPosition
	{
		None = 0,
		Top = 1,
		Bottom = 2,
		Left = 4,
		Center = 8,
		Right = 16,

		TopLeft = Top | Left,
		TopCenter = Top | Center,
		TopRight = Top | Right,

		BottomLeft = Bottom | Left,
		BottomCenter = Bottom | Center,
		BottomRight = Bottom | Right,
	}

	public class BubbleShape : View
	{
		#region CornerRadius
		public static readonly BindableProperty CornerRadiusProperty =
			BindableProperty.Create("CornerRadius", typeof(double), typeof(BubbleShape), 0.0);

		public double CornerRadius
		{
			get { return (double)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}
		#endregion CornerRadius

		#region ShapeColor
		public static readonly BindableProperty ShapeColorProperty =
			BindableProperty.Create("ShapeColor", typeof(Color), typeof(BubbleShape), Color.Black);

		public Color ShapeColor
		{
			get { return (Color)GetValue(ShapeColorProperty); }
			set { SetValue(ShapeColorProperty, value); }
		}
		#endregion ShapeColor

		#region PointerPositionPosition
		public static readonly BindableProperty PointerPositionProperty =
			BindableProperty.Create("PointerPosition", typeof(BubblePointerPosition), typeof(BubbleShape), BubblePointerPosition.None);

		public BubblePointerPosition PointerPositionPosition
		{
			get { return (BubblePointerPosition)GetValue(PointerPositionProperty); }
			set { SetValue(PointerPositionProperty, value); }
		}
		#endregion PointerPositionPosition

		public Action OnInvalidate { get; set; }

		public void Invalidate()
		{
			OnInvalidate?.Invoke();
		}
	}
}
