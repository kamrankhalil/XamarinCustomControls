using System;
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using CustomControls.iOS.CustomRenderers.DrawingControls;
using CustomControls.Controls.DrawingControls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BubbleShape), typeof(BubbleShapeRenderer))]

namespace CustomControls.iOS.CustomRenderers.DrawingControls
{
	public class BubbleShapeRenderer : VisualElementRenderer<BubbleShape>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<BubbleShape> e)
		{
			base.OnElementChanged(e);

			var element = (BubbleShape)Element;

			if (element == null)
				return;

			element.OnInvalidate = OnInvalidate;
			SetNeedsDisplay();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var element = (BubbleShape)Element;
			if (element == null)
				return;

			SetNeedsDisplay();
		}

		private void OnInvalidate()
		{
			SetNeedsDisplay();
		}

		public override void Draw(CGRect rect)
		{
			using (var context = UIGraphics.GetCurrentContext())
			{
				if (Bounds.Width < 0 || Bounds.Height < 0)
					return;

				var element = (BubbleShape)Element;

				PointF p1 = new PointF(0, 0);
				PointF p2 = new PointF((float)element.WidthRequest, 0);
				PointF p3 = new PointF((float)element.WidthRequest, (float)element.HeightRequest);
				PointF p4 = new PointF(0, (float)element.HeightRequest);

				float pointerHeight = 12;

				if ((element.PointerPositionPosition & BubblePointerPosition.Top) != 0)
				{
					p1.Y += pointerHeight;
					p2.Y += pointerHeight;
				}
				else if ((element.PointerPositionPosition & BubblePointerPosition.Bottom) != 0)
				{
					p3.Y -= pointerHeight;
					p4.Y -= pointerHeight;
				}

				float bubbleCornerRadius = (float)element.CornerRadius;

				CGPath bubblePath = new CGPath();

				bubblePath.AddArc((nfloat)(p1.X + bubbleCornerRadius), (nfloat)(p1.Y + bubbleCornerRadius),
					(nfloat)bubbleCornerRadius, DegreeToRad(180), DegreeToRad(270), false);

				if ((element.PointerPositionPosition & BubblePointerPosition.Top) != 0)
					AddBubblePointer(ref bubblePath);

				bubblePath.AddArc((nfloat)(p2.X - bubbleCornerRadius), (nfloat)(p2.Y + bubbleCornerRadius),
					(nfloat)bubbleCornerRadius, DegreeToRad(270), DegreeToRad(360), false);

				bubblePath.AddArc((nfloat)(p3.X - bubbleCornerRadius), (nfloat)(p3.Y - bubbleCornerRadius),
					(nfloat)bubbleCornerRadius, DegreeToRad(0), DegreeToRad(90), false);

				if ((element.PointerPositionPosition & BubblePointerPosition.Bottom) != 0)
					AddBubblePointer(ref bubblePath);

				bubblePath.AddArc((nfloat)(p4.X + bubbleCornerRadius), (nfloat)(p4.Y - bubbleCornerRadius),
					(nfloat)bubbleCornerRadius, DegreeToRad(90), DegreeToRad(180), false);

				context.SetFillColor(element.ShapeColor.ToUIColor().CGColor);
				context.AddPath(bubblePath);
				context.DrawPath(CGPathDrawingMode.Fill);

				bubblePath.Dispose();
			}
		}

		private void AddBubblePointer(ref CGPath bubblePath)
		{
			var element = (BubbleShape)Element;

			float arcRadius = 4;
			float padding = 20;
			float pointerWidth = 14;
			float pointerHeight = 12;

			PointF vertexCenter = new PointF(0, 0);
			nfloat vertexStartAngle = 0;
			nfloat vertexEndAngle = 0;

			//calculate rectangle for vertex arc

			if ((element.PointerPositionPosition & BubblePointerPosition.Left) != 0)
				vertexCenter.X = padding + arcRadius;
			else if ((element.PointerPositionPosition & BubblePointerPosition.Center) != 0)
				vertexCenter.X = (float)(element.WidthRequest / 2);
			else if ((element.PointerPositionPosition & BubblePointerPosition.Right) != 0)
				vertexCenter.X = (float)(element.WidthRequest - arcRadius - padding);

			if ((element.PointerPositionPosition & BubblePointerPosition.Top) != 0)
			{
				vertexCenter.Y = 0 + arcRadius;
				vertexStartAngle = 225;
				vertexEndAngle = 315;
			}
			else if ((element.PointerPositionPosition & BubblePointerPosition.Bottom) != 0)
			{
				vertexCenter.Y = (float)(element.HeightRequest - arcRadius);
				vertexStartAngle = 45;
				vertexEndAngle = 135;
			}

			PointF groundCenter1 = new PointF(vertexCenter.X, vertexCenter.Y);
			float groundCenter1StartAngle = 0;
			float groundCenter1EndAngle = 0;

			PointF groundCenter2 = new PointF(vertexCenter.X, vertexCenter.Y);
			float groundCenter2StartAngle = 0;
			float groundCenter2EndAngle = 0;

			if ((element.PointerPositionPosition & BubblePointerPosition.Top) != 0)
			{
				groundCenter1.X -= pointerWidth;
				groundCenter1.Y += (pointerHeight - 2 * arcRadius);
				groundCenter1StartAngle = 90;
				groundCenter1EndAngle = 45;

				groundCenter2.X += pointerWidth;
				groundCenter2.Y += (pointerHeight - 2 * arcRadius);
				groundCenter2StartAngle = 135;
				groundCenter2EndAngle = 90;
			}
			else if ((element.PointerPositionPosition & BubblePointerPosition.Bottom) != 0)
			{
				groundCenter1.X += pointerWidth;
				groundCenter1.Y -= (pointerHeight - 2 * arcRadius);
				groundCenter1StartAngle = 270;
				groundCenter1EndAngle = 225;

				groundCenter2.X -= pointerWidth;
				groundCenter2.Y -= (pointerHeight - 2 * arcRadius);
				groundCenter2StartAngle = 315;
				groundCenter2EndAngle = 270;
			}

			bubblePath.AddArc((nfloat)(groundCenter1.X), (nfloat)(groundCenter1.Y),
				(nfloat)arcRadius, DegreeToRad(groundCenter1StartAngle), DegreeToRad(groundCenter1EndAngle), true);

			bubblePath.AddArc((nfloat)(vertexCenter.X), (nfloat)(vertexCenter.Y),
				(nfloat)arcRadius, DegreeToRad(vertexStartAngle), DegreeToRad(vertexEndAngle), false);

			bubblePath.AddArc((nfloat)(groundCenter2.X), (nfloat)(groundCenter2.Y),
				(nfloat)arcRadius, DegreeToRad(groundCenter2StartAngle), DegreeToRad(groundCenter2EndAngle), true);

		}

		private nfloat DegreeToRad(nfloat value)
		{
			return (nfloat)(value * Math.PI / 180);
		}
	}
}
