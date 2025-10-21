using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConditerShop
{
    /// <summary>
    /// Логика взаимодействия для DataPage.xaml
    /// </summary>
    public partial class DataPage : Page
    {
        string _pageName;
        public DataPage(string pageName)
        {
            _pageName = pageName;   

            InitializeComponent();
            LoadPage();
        }

        private void LoadPage()
        {
            PageNameText.Text = _pageName;
        }

        private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.Frame.Content = null;
            }
        }
    }
}
