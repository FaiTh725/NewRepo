using BinnaryTreeSort.Resourses;
using LanguageExt.Pretty;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BinnaryTreeSort.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal)
            {
                maximizeBtn.Source = new BitmapImage(new Uri(@"\View\Images\HeadWindow\NormalScreen.png", UriKind.Relative));
            }
            else
            {
                maximizeBtn.Source = new BitmapImage(new Uri(@"\View\Images\HeadWindow\MaximizeScreen.png", UriKind.Relative));
            }
        }
    }
}
