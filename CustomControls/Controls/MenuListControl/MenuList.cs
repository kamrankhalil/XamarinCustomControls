using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomControls.Controls.MenuListControl
{
	public class MenuList : ContentView
	{
		private List<View> _itemsViews = new List<View>();
		private StackLayout _mainLayout = null;

		private const string OpeningAnimation = "OpeningAnimation";
		private const string ClosingAnimation = "ClosingAnimation";

		public MenuList()
		{
			_mainLayout = new StackLayout();

			base.Content = _mainLayout;
		}

		#region ItemsSource
		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(MenuList),
				null,
				BindingMode.TwoWay,
				propertyChanged: (bindable, oldValue, newValue) =>
				{
					((MenuList)bindable).ItemsSourceChanged(bindable, (IList)oldValue, (IList)newValue);
				}
			);

		public IList ItemsSource
		{
			get => (IList)GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}

		void ItemsSourceChanged(BindableObject bindable, IList oldValue, IList newValue)
		{
			if (ItemsSource == null)
				return;

			var control = bindable as MenuList;
			if (control == null)
				return;

			var notifyCollection = newValue as INotifyCollectionChanged;
			if (notifyCollection != null)
			{
				notifyCollection.CollectionChanged += (sender, args) =>
				{
				};

				_itemsViews.Clear();
				_mainLayout.Children.Clear();
				_mainLayout.Spacing = ItemSpace;


				foreach (var newItem in newValue)
				{
					var view = GetView(newItem);
					view.Opacity = 0;
					_mainLayout.Children.Add(view);

					_itemsViews.Add(view);
				}

			}
		}
		#endregion ItemSource

		#region ItemTemplate
		public static readonly BindableProperty ItemTemplateProperty =
			BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(MenuList), null, BindingMode.TwoWay);

		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate)GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}
		#endregion ItemTemplate

		#region ItemTapped
		public event EventHandler<ItemTappedEventArgs> ItemTapped;
		private void OnItemTapped(ItemTappedEventArgs e)
		{
			ItemTapped?.Invoke(this, e);
		}
		#endregion ItemTapped

		#region ItemTappedCommand
		public static BindableProperty ItemTappedCommandProperty =
			BindableProperty.Create<MenuList, ICommand>(x => x.ItemTappedCommand, null);

		public ICommand ItemTappedCommand
		{
			get { return (ICommand)this.GetValue(ItemTappedCommandProperty); }
			set { SetValue(ItemTappedCommandProperty, value); }
		}
		#endregion ItemTappedCommnad

		#region ItemSpace
		public static readonly BindableProperty ItemSpaceProperty =
			BindableProperty.Create("ItemSpace", typeof(double), typeof(MenuList), 0.0);

		public double ItemSpace
		{
			get { return (double)GetValue(ItemSpaceProperty); }
			set { SetValue(ItemSpaceProperty, value); }
		}
		#endregion ItemSpace

		#region IsOpened
		public static readonly BindableProperty IsOpenedProperty =
			BindableProperty.Create(nameof(IsOpened), typeof(bool), typeof(MenuList), false, BindingMode.TwoWay, propertyChanged: IsOpenedChanged);

		private static void IsOpenedChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (!(oldValue is bool) || !(newValue is bool))
				return;

			if (oldValue == newValue)
				return;

			if ((bool)newValue)
			{
				((MenuList)bindable).Opening();
			}
			else
				((MenuList)bindable).Closing();
		}

		public bool IsOpened
		{
			get { return (bool)GetValue(IsOpenedProperty); }
			set { SetValue(IsOpenedProperty, value); }
		}
		#endregion IsOpened

		private View GetView(object item)
		{
			View view = null;

			var template = GetTemplateFor(item);
			var content = template.CreateContent();

			if (!(content is View) && !(content is ViewCell))
				return null;

			view = (content is View) ? content as View : ((ViewCell)content).View;
			try
			{
				view.BindingContext = item;
				view.GestureRecognizers.Add(new TapGestureRecognizer { Command = ItemTappedCommand, CommandParameter = item });
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			ICommand TapItemCommand = new Command(() => OnItemTapped(new ItemTappedEventArgs(null, item)));
			view.GestureRecognizers.Add(new TapGestureRecognizer { Command = TapItemCommand });

			return view;
		}

		private DataTemplate GetTemplateFor(object item)
		{
			if (ItemTemplate is DataTemplateSelector)
			{
				DataTemplateSelector templateSelector = (DataTemplateSelector)ItemTemplate;
				return templateSelector.SelectTemplate(item, this);
			}

			return ItemTemplate;
		}

		private void Opening()
		{
			this.AbortAnimation(ClosingAnimation);

			if (_itemsViews.Count > 0)
			{
				var openAnimation = new Animation();
				for (var i = _itemsViews.Count - 1; i >= 0; i--)
				{
					int index = i;

					var opacityAnimation = new Animation(x => _itemsViews[index].Opacity = x, 0, 1, Easing.Linear);
					double beginAt = ((_itemsViews.Count - 1) - index) * 1.0 / _itemsViews.Count;
					openAnimation.Add(Math.Min(beginAt, 1), 1, opacityAnimation);
				}

				openAnimation.Commit(this, OpeningAnimation, 16, 400, null, (d, b) =>
				{
				});
			}
		}

		private void Closing()
		{
			this.AbortAnimation(OpeningAnimation);

			for (var i = 0; i < _itemsViews.Count; i++)
				_itemsViews[i].Opacity = 0;
		}
	}
}
