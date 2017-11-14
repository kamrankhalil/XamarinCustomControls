using System.Linq;
using Xamarin.Forms;

namespace CustomControls.Effects
{
	public static class ViewEffects
	{
		#region HasShadow

		public static readonly BindableProperty HasShadowProperty =
			BindableProperty.CreateAttached("HasShadow", typeof(bool), typeof(ViewEffects), false,
				propertyChanged: OnHasShadowChanged);

		public static void SetHasShadow(BindableObject view, bool hasShadow)
		{
			view.SetValue(HasShadowProperty, hasShadow);
		}

		public static bool GetHasShadow(BindableObject view)
		{
			return (bool)view.GetValue(HasShadowProperty);
		}

		private static void OnHasShadowChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var view = bindable as View;
			if (view == null)
				return;

			var hasShadow = (bool)newValue;
			if (hasShadow)
			{
				view.Effects.Add(new ShadowEffect());
			}
			else
			{
				var toRemove = view.Effects.FirstOrDefault(e => e is ShadowEffect);
				if (toRemove != null)
					view.Effects.Remove(toRemove);
			}
		}

		#endregion HasShadow

		#region ShadowSize

		public static readonly BindableProperty ShadowSizeProperty =
			BindableProperty.CreateAttached("ShadowSize", typeof(double), typeof(ViewEffects), 0d);

		public static void SetShadowSize(BindableObject view, double size)
		{
			view.SetValue(ShadowSizeProperty, size);
		}

		public static double GetShadowSize(BindableObject view)
		{
			return (double)view.GetValue(ShadowSizeProperty);
		}

		#endregion ShadowSize

		#region ShadowOpacity

		public static readonly BindableProperty ShadowOpacityProperty =
			BindableProperty.CreateAttached("ShadowOpacity", typeof(double), typeof(ViewEffects), 1d);

		public static void SetShadowOpacity(BindableObject view, double value)
		{
			view.SetValue(ShadowOpacityProperty, value);
		}

		public static double GetShadowOpacity(BindableObject view)
		{
			return (double)view.GetValue(ShadowOpacityProperty);
		}

		#endregion ShadowOpacity

		#region ShadowColor

		public static readonly BindableProperty ShadowColorProperty =
			BindableProperty.CreateAttached("ShadowColor", typeof(Color), typeof(ViewEffects), Color.Default);

		public static void SetShadowColor(BindableObject view, Color color)
		{
			view.SetValue(ShadowColorProperty, color);
		}

		public static Color GetShadowColor(BindableObject view)
		{
			return (Color)view.GetValue(ShadowColorProperty);
		}
		#endregion ShadowColor
	}
}
