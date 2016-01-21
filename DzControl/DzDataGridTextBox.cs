using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;

namespace Dothan.DzControl
{
    public class DzDataGridTextBox : TextBox
    {
        public DzDataGridTextBox()
        {
            this.GotKeyboardFocus += MyTextBox_GotKeyboardFocus;
            this.BorderThickness = new Thickness(0);
            this.Background = new SolidColorBrush(Colors.Transparent);
            this.VerticalContentAlignment = VerticalAlignment.Center;
        }

        void MyTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.SelectAll();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            this.BorderThickness = new Thickness(2);
            this.BorderBrush = new SolidColorBrush(Colors.Orange);
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);
            this.BorderThickness = new Thickness(0);
            this.BorderBrush = new SolidColorBrush(Colors.Gray);
        }
    }
}
