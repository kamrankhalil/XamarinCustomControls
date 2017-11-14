using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CustomControls.Droid.Effects;
using CustomControls.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("CustomControls")]
[assembly: ExportEffect(typeof(ShadowEffect), "ShadowEffect")]
namespace CustomControls.Droid.Effects
{
	public class ShadowEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
			{
				Container.Elevation = 40;
			}
		}

		protected override void OnDetached()
		{
		}

		protected override void OnElementPropertyChanged(PropertyChangedEventArgs e)
		{
		}


		private Drawable GetShadowBackground()
		{
			GradientDrawable shape = new GradientDrawable();
			shape.SetShape(ShapeType.Rectangle);
			shape.SetColor(Color.Lime.ToAndroid());
			return shape;
		}
	}
}
