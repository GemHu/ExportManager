using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dothan.DzControl
{
    public class ImageButton : Button
    {
        public ImageButton()
        {
            this.Loaded += new RoutedEventHandler(ImageButton_Loaded);
            this.IsEnabledChanged += new DependencyPropertyChangedEventHandler(ImageButton_IsEnabledChanged);
        }

        void ImageButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_img != null)
                _img.IsEnabled = this.IsEnabled;
        }

        /// <summary>
        /// 图片缓存
        /// </summary>
        private AutoGrayImage _img;

        void ImageButton_Loaded(object sender, RoutedEventArgs e)
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;

            try
            {
                if (this.ImageSource != null)
                {
                    this._img = new AutoGrayImage() { Source2 = this.ImageSource };
                    sp.Children.Add(this._img);
                }

                sp.Children.Add(new TextBlock() { Text = MyText, Margin = new Thickness(5, 0, 0, 0), HorizontalAlignment = System.Windows.HorizontalAlignment.Center });

                this.Content = sp;
            }
            catch { }
        }

        public static DependencyProperty ImageSourceProperty =
           DependencyProperty.Register("ImageSource", typeof(BitmapSource), typeof(ImageButton),
          new FrameworkPropertyMetadata(null));

        /// <summary>
        /// 图片源、地址
        /// </summary>
        public BitmapSource ImageSource
        {
            get { return (BitmapSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static DependencyProperty TextProperty =
          DependencyProperty.Register("MyText", typeof(string), typeof(ImageButton),
         new FrameworkPropertyMetadata(null));
        /// <summary>
        /// Text内容
        /// </summary>
        public string MyText
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
