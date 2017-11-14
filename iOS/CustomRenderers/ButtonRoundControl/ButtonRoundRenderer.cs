using UIKit;
using CustomControls.Controls.ButtonRound;
using CustomControls.iOS.CustomRenderers.ButtonRoundControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;
using CoreAnimation;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(ButtonRound), typeof(ButtonRoundRenderer))]

namespace CustomControls.iOS.CustomRenderers.ButtonRoundControl
{
	public class ButtonRoundRenderer : ButtonRenderer
	{
		private static UIColor DisabledTextColor => Color.White.ToUIColor();

		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);
			var view = (ButtonRound)this.Element;

			if (view == null)
				return;

			if (Control == null)
				return;

			if (view.CornerRadius == 0 && view.BorderRadius != 0)
			{
				view.CornerRadius = view.BorderRadius;
				view.BorderRadius = 0;
			}

			Control.ContentEdgeInsets = new UIEdgeInsets(
				(int)view.TopInnerPadding,
				(int)view.LeftInnerPadding,
				(int)view.BottomInnerPadding,
				(int)view.RightInnerPadding
				);

			//Setting light text color on disabled button.
			/* if (Control.CurrentTitle != null)
			 {
				 var title = new NSAttributedString(Control.CurrentTitle, new UIStringAttributes { ForegroundColor = DisabledTextColor });
				 Control.SetAttributedTitle(title, UIControlState.Disabled);
			 }*/
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			SetNeedsDisplay();
		}

		private void ClipCorners()
		{
			var view = (ButtonRound)this.Element;

			Control.ClipsToBounds = true;

			CAShapeLayer maskLayer = new CAShapeLayer();

			UIRectCorner corners = UIRectCorner.AllCorners;

			if (view.IsRoundedLeftCorners && !view.IsRoundedRightCorners)
				corners = UIRectCorner.BottomLeft | UIRectCorner.TopLeft;
			else if (view.IsRoundedRightCorners && !view.IsRoundedLeftCorners)
				corners = UIRectCorner.BottomRight | UIRectCorner.TopRight;

			UIBezierPath cropPath = UIBezierPath.FromRoundedRect(Control.Bounds, corners, new CGSize(view.CornerRadius, view.CornerRadius));
			maskLayer.Frame = Control.Bounds;
			maskLayer.Path = cropPath.CGPath;
			Control.Layer.Mask = maskLayer;
			Control.Layer.MasksToBounds = true;

			if (view.BackgroundColor == Color.Transparent ||
				view.BackgroundColor == Color.White)
			{
				bool reuseExistingSublayer = false;
				foreach (CALayer layer in Control?.Layer.Sublayers)
				{
					if (layer is CAShapeLayer)
					{
						((CAShapeLayer)layer).Path = maskLayer.Path;
						((CAShapeLayer)layer).FillColor = Color.Transparent.ToCGColor();
						((CAShapeLayer)layer).StrokeColor = view.BorderColor.ToCGColor();
						((CAShapeLayer)layer).LineWidth = 2;
						((CAShapeLayer)layer).Frame = Control.Bounds;

						reuseExistingSublayer = true;
					}

				}

				if (!reuseExistingSublayer)
				{
					CAShapeLayer borderLayer = new CAShapeLayer();
					borderLayer.Path = maskLayer.Path;
					borderLayer.FillColor = Color.Transparent.ToCGColor();
					borderLayer.StrokeColor = view.BorderColor.ToCGColor();
					borderLayer.LineWidth = 2;
					borderLayer.Frame = Control.Bounds;

					Control.Layer.AddSublayer(borderLayer);
				}
			}
		}

		public override void Draw(CGRect rect)
		{
			var view = (ButtonRound)this.Element;

			if (view.IsRoundedLeftCorners && view.IsRoundedRightCorners)
				Control.Layer.CornerRadius = view.CornerRadius;
			else
				Control.Layer.CornerRadius = 0;

			if (view.IsRoundedLeftCorners || view.IsRoundedRightCorners)
				ClipCorners();
		}
	}
}
