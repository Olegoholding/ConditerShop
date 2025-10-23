using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ConditerShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DateBox.Content = DateTime.UtcNow;
        }
            private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => 
            Application.Current.Resources["Current"] = (ThemeComboBox.SelectedIndex == 0) ? 
            (SolidColorBrush)Application.Current.Resources["LightTheme"] :
            (SolidColorBrush)Application.Current.Resources["DarkTheme"];
        private void CandyTypeButton_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new DataPage(((((Button)sender).Content as Grid).Children[1] as TextBlock).Text, ((Button)sender).Tag.ToString(), ((Button)sender).Uid, $"Select * From {((Button)sender).Tag.ToString()}_view"));
    }
}
