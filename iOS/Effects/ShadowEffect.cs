using System;
using System.ComponentModel;
using CoreGraphics;
using CustomControls.iOS.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("CustomControls")]
[assembly: ExportEffect(typeof(ShadowEffect), "ShadowEffect")]
namespace CustomControls.iOS.Effects
{
	public class ShadowEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			Container.Layer.ShadowOffset = new CGSize(0, 0);
			UpdateSize();
			UpdateColor();
			UpdateOpacity();
		}

		protected override void OnDetached()
		{
			Container.Layer.ShadowOpacity = 0;
		}

		protected override void OnElementPropertyChanged(PropertyChangedEventArgs e)
		{
			if (e.PropertyName == CustomControls.Effects.ViewEffects.HasShadowProperty.PropertyName)
			{
				UpdateOpacity();
			}
			else if (e.PropertyName == CustomControls.Effects.ViewEffects.ShadowColorProperty.PropertyName)
			{
				UpdateColor();
			}
			else if (e.PropertyName == CustomControls.Effects.ViewEffects.ShadowSizeProperty.PropertyName)
			{
				UpdateSize();
			}
		}

		private void UpdateOpacity()
		{
			Container.Layer.ShadowOpacity = (float)CustomControls.Effects.ViewEffects.GetShadowOpacity(Element);
		}

		private void UpdateColor()
		{
			var color = CustomControls.Effects.ViewEffects.GetShadowColor(Element);
			Container.Layer.ShadowColor = color.ToUIColor().CGColor;
		}

		private void UpdateSize()
		{
			Container.Layer.ShadowRadius = (nfloat)CustomControls.Effects.ViewEffects.GetShadowSize(Element);
		}
	}
}
