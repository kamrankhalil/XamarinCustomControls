using Xamarin.Forms;

namespace CustomControls.Controls.DrawingControls
{
	public class CircleShape : View
	{
		#region CircleRadius
		public static readonly BindableProperty CircleRadiusProperty =
			BindableProperty.Create("CircleRadius", typeof(double), typeof(CircleShape), 0.0);

		public double CircleRadius
		{
			get { return (double)GetValue(CircleRadiusProperty); }
			set { SetValue(CircleRadiusProperty, value); }
		}
		#endregion CircleRadius

		#region ShapeColor
		public static readonly BindableProperty ShapeColorProperty =
			BindableProperty.Create("ShapeColor", typeof(Color), typeof(CircleShape), Color.Black);

		public Color ShapeColor
		{
			get { return (Color)GetValue(ShapeColorProperty); }
			set { SetValue(ShapeColorProperty, value); }
		}
		#endregion ShapeColor
	}
}
