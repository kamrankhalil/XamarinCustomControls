using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomControls.Controls.Fab
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FabView : ContentView
	{
		public FabView()
		{
			InitializeComponent();

			FabButton.Clicked += FabButton_Clicked; ;
		}

		private void FabButton_Clicked(object sender, EventArgs e)
		{
			Clicked?.Invoke(sender, e);
		}

		#region TapCommand
		public static BindableProperty TapCommandProperty =
			BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(FabView), null, BindingMode.TwoWay,
			propertyChanged: (bindable, oldValue, newValue) =>
			{
				((FabView)bindable).FabButton.Command = (ICommand)newValue;
			});

		public ICommand TapCommand
		{
			get { return (ICommand)this.GetValue(TapCommandProperty); }
			set { SetValue(TapCommandProperty, value); }
		}
		#endregion TapCommand

		#region Image
		public static BindableProperty ImageProperty =
			BindableProperty.Create(nameof(Image), typeof(FileImageSource), typeof(FabView), null, BindingMode.TwoWay,
			propertyChanged: (bindable, oldValue, newValue) =>
			{
				((FabView)bindable).FabButton.Image = (FileImageSource)newValue;
			});

		public FileImageSource Image
		{
			get { return (FileImageSource)this.GetValue(ImageProperty); }
			set { SetValue(ImageProperty, value); }
		}
		#endregion Image

		#region ButtonStyle
		public static BindableProperty ButtonStyleProperty =
			BindableProperty.Create(nameof(ButtonStyle), typeof(Style), typeof(FabView), null, BindingMode.TwoWay,
			propertyChanged: (bindable, oldValue, newValue) =>
			{
				((FabView)bindable).FabButton.Style = (Style)newValue;
			});

		public Style ButtonStyle
		{
			get { return (Style)this.GetValue(ButtonStyleProperty); }
			set { SetValue(ButtonStyleProperty, value); }
		}
		#endregion ButtonStyle

		public event EventHandler Clicked;
	}
}