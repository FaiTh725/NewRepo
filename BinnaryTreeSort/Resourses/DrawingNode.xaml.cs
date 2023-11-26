using System.Windows.Controls;
using System.Windows.Media;

namespace BinnaryTreeSort.Resourses
{
    /// <summary>
    /// Логика взаимодействия для DrawingNode.xaml
    /// </summary>
    public partial class DrawingNode : UserControl
    {
        public DrawingNode()
        {
            InitializeComponent();
        }
        #region Properties

        private string text;

        public string Text
        {
            get => text;
            set
            {
                text = value;
                tb.Text = text;
            }
        }

        private SolidColorBrush backGround;

        public SolidColorBrush BackGround
        {
            get => backGround;
            set
            {
                backGround = value;
                el.Fill = backGround;
            }
        }

        private double fontSize;

        public double FontSize
        {
            get => fontSize;
            set
            {
                fontSize = value;
                tb.FontSize = fontSize;
            }
        }

        #endregion
    }
}

