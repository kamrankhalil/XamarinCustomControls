using System;
using System.Collections.Generic;
using Xamarin.Forms;
using CustomControls.ViewModels;
using CustomControls.Models;
using CustomControls.Controls.ButtonRound;

namespace CustomControls.Views
{
    public partial class FabMenuView : ContentView
    {

        public FabMenuView()
        {
            InitializeComponent();
            //MenuListModel menuModel = new MenuListModel();
            //menuModel.Name = "Menu Item";
            //ViewModel.MenuListItems.Add(menuModel);
            //ViewModel.MenuListItems.Add(menuModel);
            //ViewModel.MenuListItems.Add(menuModel);
            //ViewModel.MenuListItems.Add(menuModel);
        }

        void OnViewClicked(object sender, EventArgs e)
        {
            FabMenuViewModel ViewModel = BindingContext as FabMenuViewModel;

            if (ViewModel != null)
                ViewModel.SelectViewCommand.Execute((sender as ButtonRound).Source);
        }
    }
}
