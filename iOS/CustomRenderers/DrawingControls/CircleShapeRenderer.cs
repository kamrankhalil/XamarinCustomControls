using System;
using System.ComponentModel;
using CoreGraphics;
using CustomControls.Controls.DrawingControls;
using UIKit;
using CustomControls.iOS.CustomRenderers.DrawingControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(CircleShape), typeof(CircleShapeRenderer))]

namespace CustomControls.iOS.CustomRenderers.DrawingControls
{
	public class CircleShapeRenderer : VisualElementRenderer<CircleShape>
	{
		const float FULL_CIRCLE = 2 * (float)Math.PI;

		protected override void OnElementChanged(ElementChangedEventArgs<CircleShape> e)
		{
			base.OnElementChanged(e);
			SetNeedsDisplay();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			SetNeedsDisplay(); // Force a call to Draw
		}

		public override void Draw(CGRect rect)
		{
			base.Draw(rect);
			using (var context = UIGraphics.GetCurrentContext())
			{
				var element = (CircleShape)Element;

				context.SetFillColor(element.ShapeColor.ToUIColor().CGColor);
				CGPath path = new CGPath();
				path.AddArc(this.Bounds.Width / 2, this.Bounds.Height / 2, (nfloat)element.CircleRadius, 0, FULL_CIRCLE, false);
				context.AddPath(path);
				context.DrawPath(CGPathDrawingMode.Fill);

				path.Dispose();
			}
		}
	}
}
