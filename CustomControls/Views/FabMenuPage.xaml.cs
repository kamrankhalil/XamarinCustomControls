using System;
using System.Collections.Generic;
using CustomControls.ViewModels;
using CustomControls.Models;

using Xamarin.Forms;

namespace CustomControls.Views
{
    public partial class FabMenuPage : ContentPage
    {
		public new FabMenuViewModel ViewModel => this.BindingContext as FabMenuViewModel;

		public FabMenuPage()
        {
            InitializeComponent();
			if (ViewModel == null)
				BindingContext = new FabMenuViewModel();
		}
    }
}
