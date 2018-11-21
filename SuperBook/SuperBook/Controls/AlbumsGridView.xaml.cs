using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuperBook.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AlbumsGridView : Grid
	{
        private int MaxRows;
        private int MaxColumns;

        public List<int> ItemSource
        {
            get
            {
                return (List<int>)GetValue(ItemSourceProperty);
            }
            set
            {
                SetValue(ItemSourceProperty, value);
                OnPropertyChanged(nameof(ItemSource));
            }
        }
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
                OnPropertyChanged(nameof(Command));
            }
        }

        public static readonly BindableProperty ItemSourceProperty =
            BindableProperty.Create("ItemSource", typeof(List<int>)
                , typeof(AlbumsGridView), propertyChanged: OnItemSourceChanged);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(AlbumsGridView), propertyChanged: OnItemTappedChanged);

        public AlbumsGridView ()
		{
			InitializeComponent();
            this.MaxColumns = ColumnDefinitions.Count();
        }

        private static void OnItemTappedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((AlbumsGridView)bindable).OnItemTappedChanged((ICommand)oldValue, (ICommand)newValue);
        }

        private void OnItemTappedChanged(ICommand oldValue, ICommand newValue)
        {
            this.Command = newValue;
        }

        private static void OnItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((AlbumsGridView)bindable).OnItemSourceChanged((List<int>)oldValue, (List<int>)newValue);
        }

        private void OnItemSourceChanged(List<int> oldValue, List<int> newValue)
        {
            this.SetAlbumFrames(newValue);
        }

        void SetAlbumFrames(List<int> newValue)
        {
            this.MaxRows = newValue.Count % this.MaxColumns == 0 ? newValue.Count / this.MaxColumns
                : newValue.Count / this.MaxColumns + 1;

            Children?.Clear();
            RowDefinitions?.Clear();

            this.BuildAlbumFrames(newValue);
        }
        public void BuildAlbumFrames(List<int> albumIdList)
        {
            try
            {
                if (albumIdList == null || albumIdList.Count() == 0)
                {
                    this.Children?.Clear();

                    return;
                }


                for (int rowCounter = 0; rowCounter < this.MaxRows; rowCounter++)
                {
                    this.RowDefinitions?.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                }

                for (int index = 0; index < albumIdList.Count; index++)
                {
                    var columnIndex = index % this.MaxColumns;
                    var rowIndex = (int)Math.Floor(index / (float)this.MaxColumns);

                    var albumFrame = this.BuildAlbumFrame(albumIdList[index]);

                    albumFrame.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = this.Command,
                        CommandParameter = albumIdList[index]
                    });
                    this.Children?.Add(albumFrame, columnIndex, rowIndex);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private Frame BuildAlbumFrame(object albumId)
        {
            var albumFrame = new Frame
            {
                Content = new Label
                {
                    Text = string.Format("Album {0}", albumId),
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                },
                BackgroundColor = Color.White,
                HeightRequest = 100,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            return albumFrame;
        }
    }
}