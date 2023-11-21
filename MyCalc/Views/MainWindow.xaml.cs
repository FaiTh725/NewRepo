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
using System.Windows.Shapes;

namespace MyCalc.Views
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

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double appHeight = this.Height;

            NumberTB.FontSize = appHeight/20;
            ExpressionTB.FontSize = appHeight/50;

            double appWidth = this.Width;

            if(appWidth>850)
            {
                HistoryColumn.Width = new GridLength((appWidth * 2) / 5);
                MainColumn.Width = new GridLength((appWidth * 3) / 5);
                HeadGrid.Width = appWidth - 25;
            }
            else
            {
                MainColumn.Width = new GridLength(appWidth-15);
                HistoryColumn.Width = new GridLength(0);
                HeadGrid.Width = appWidth - 13;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Application.Current.MainWindow.WindowState == WindowState.Normal)
            {
                BtnMaximizeImage.Source = new BitmapImage(new Uri(@"\Views\Images\Head\NormalScreen.png", UriKind.Relative));
            }
            else
            {
                BtnMaximizeImage.Source = new BitmapImage(new Uri(@"\Views\Images\Head\MaximizeScreen.png", UriKind.Relative));
            }
        }
    }
}
