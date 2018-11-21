using SuperBook.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuperBook.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PhotoGridView : Grid
	{
        private int MaxRows;
        private int MaxColumns;

        public List<Photo> ItemSource
        {
            get
            {
                return (List<Photo>)GetValue(ItemSourceProperty);
            }
            set
            {
                SetValue(ItemSourceProperty, value);
                OnPropertyChanged(nameof(ItemSource));
            }
        }

        public static readonly BindableProperty ItemSourceProperty =
            BindableProperty.Create("ItemSource", typeof(List<Photo>), 
            typeof(PhotoGridView), propertyChanged: OnItemSourceChanged, defaultBindingMode:BindingMode.TwoWay);

        public PhotoGridView ()
		{
			InitializeComponent();
            this.MaxColumns = ColumnDefinitions.Count();
        }

        private static void OnItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((PhotoGridView)bindable).OnItemSourceChanged((List<Photo>)oldValue, (List<Photo>)newValue);
        }

        private void OnItemSourceChanged(List<Photo> oldValue, List<Photo> newValue)
        {
            this.SetAlbumFrames(newValue);
        }

        void SetAlbumFrames(List<Photo> newValue)
        {
            this.MaxRows = newValue.Count % this.MaxColumns == 0 ? newValue.Count / this.MaxColumns
                : newValue.Count / this.MaxColumns + 1;

            Children?.Clear();
            RowDefinitions?.Clear();

            this.BuildAlbumFrames(newValue);
        }
        public void BuildAlbumFrames(List<Photo> photoList)
        {
            try
            {
                if (photoList == null || photoList.Count() == 0)
                {
                    this.Children?.Clear();

                    return;
                }


                for (int rowCounter = 0; rowCounter < this.MaxRows; rowCounter++)
                {
                    this.RowDefinitions?.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                }

                for (int index = 0; index < photoList.Count; index++)
                {
                    var columnIndex = index % this.MaxColumns;
                    var rowIndex = (int)Math.Floor(index / (float)this.MaxColumns);

                    var albumFrame = this.BuildAlbumFrame(photoList[index]);

                    this.Children?.Add(albumFrame, columnIndex, rowIndex);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private Frame BuildAlbumFrame(Photo photo)
        {
            var albumFrame = new Frame
            {
                Content = new Image
                {
                    HeightRequest = 100,
                    WidthRequest = 100,
                    BackgroundColor = Color.Aqua,
                    Source = photo.Url
                }
            };

            return albumFrame;
        }
    }
}