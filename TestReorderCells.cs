using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace TestReorderCells {
	public class App : Application {
		public App() {
			ObservableCollection<string> items = new ObservableCollection<string>();
			for (int i = 1; i < 6; i++) items.Add("Item " + i.ToString());

			var list = new ListView {
				ItemsSource = items,
				ItemTemplate = new DataTemplate(() => {
					var c = new DataCell();
					c.SetBinding(DataCell.NameProperty, ".");
					c.ItemMoved += (sender, e) => {
						var ix = items.IndexOf(e.Item);
						if ((e.Up && ix < 1) || (!e.Up && ix >= items.Count - 1)) {
							return;
						}
						items.Move(ix, ix + (e.Up ? -1 : 1));
					};
					return c;
				})
			};
			list.ItemSelected += (sender, e) => list.SelectedItem = null;

			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = { list }
				}
			};
		}
	}

	class DataCell : ViewCell {
		public static BindableProperty NameProperty =
			BindableProperty.Create<DataCell, string>(c => c.Name, null);
				
		public string Name {
			get { return (string)GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}

		public class ItemMovedEventArgs : EventArgs {
			public string Item { get; set; }
			public bool Up { get; set; }
		}
		public event EventHandler<ItemMovedEventArgs> ItemMoved;

		public DataCell() {
			var lbl = new Label {
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Gray,
				TextColor = Color.Black
			};
			lbl.SetBinding(Label.TextProperty, ".");

			var up = new Button { Text = "↑", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)) };
			var down = new Button { Text = "↓", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)) };

			up.Clicked += (sender, e) => {
				if (ItemMoved != null) ItemMoved(this, new ItemMovedEventArgs { Item = Name, Up = true });
			};
			down.Clicked += (sender, e) => {
				if (ItemMoved != null) ItemMoved(this, new ItemMovedEventArgs { Item = Name, Up = false });
			};

			View = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Children = { lbl, up, down }
			};
		}
	}
}

