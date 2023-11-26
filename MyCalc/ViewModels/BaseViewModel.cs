using MyCalc.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MyCalc.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            Number = "0";
            Expresion = string.Empty;
            historyItems = new ObservableCollection<HistoryItem>();
            HistoryMessage = "Тут пока пусто";
        }

        #region Propertyes

        private bool lastButtonIsOper;
        private bool lastButtonIsCalculate;

        private string historyMessage;

        public string HistoryMessage
        {
            get => historyMessage; 
            set
            {
                historyMessage = value;
                OnPropertyChanged(nameof(HistoryMessage));
            }
        }

        private ObservableCollection<HistoryItem> historyItems;

        public ObservableCollection<HistoryItem> HistoryItems
        {
            get => historyItems;
            set
            {
                historyItems = value;
                OnPropertyChanged("HistoryItems");
            }
        }

        private string number;

        public string Number
        {
            get => number;
            set
            {
                if (number == "0" && value != "0" && value != "0,")
                {
                    number = value[1..];
                    OnPropertyChanged(nameof(Number));
                    return;
                }

                number = value;
                OnPropertyChanged(nameof(Number));


            }
        }

        private string expresion;

        public string Expresion
        {
            get => expresion;
            set
            {
                expresion = value;
                OnPropertyChanged(nameof(Expresion));
            }
        }

        #endregion

        #region Commands

        /*private void SelectHistory(object? selectedIndex)
        {
            
        }

        private RelayCommand selecteHistoryCommand;

        public RelayCommand SelecteHistoryCommand
        {
            get
            {
                return selecteHistoryCommand ?? new RelayCommand(obj =>
                {
                    SelectHistory(obj);
                });
            }
        }*/

        private RelayCommand clearHistoryCommand;

        public RelayCommand ClearHistoryCommand
        {
            get
            {
                return clearHistoryCommand ?? new RelayCommand(obj => 
                {
                    Action<object> action = (object obj) => HistoryItems.Clear(); HistoryMessage = "Тут пока пусто" ;

                    action.Invoke(null);
                });
            }
        }

        private void SqrtX()
        {
            var val = double.Parse(Number);

            var res = Math.Sqrt(val);

            Number = res.ToString();
            Expresion = string.Empty;
        }

        private RelayCommand sqrtXCommand;

        public RelayCommand SqrtXCommand
        {
            get
            {
                return sqrtXCommand ?? new RelayCommand(obj => 
                { 
                    SqrtX();
                });
            }
        }

        private void Pow2()
        {
            double value = double.Parse(Number);

            Expresion = Number + '×' + Number + '=' ;
            Number = (value*value).ToString();
        }

        private RelayCommand pow2Command;

        public RelayCommand Pow2Command
        {
            get
            {
                return pow2Command ?? new RelayCommand(obj => 
                {
                    Pow2();
                });
            }
        }

        private void DivideOnNumber()
        {
            if(Number != "0")
            {
                double res = 1d / double.Parse(Number);

                Expresion = "1÷" + Number+'='; 
                Number = res.ToString();
            }
        }

        private RelayCommand divideOneOnNumberCommand;

        public RelayCommand DivideOneOnNumberCommand
        {
            get
            {
                return divideOneOnNumberCommand ?? new RelayCommand(obj => 
                {
                    DivideOnNumber();
                });
            }
        }

        private void CalculateExpression()
        {
            try
            {
                int indexOperation = Expresion.IndexOfAny(new char[] { '+', '-', '×', '÷' }, 1);

                double leftValue = 0;
                double rightValue = 0;

                if(lastButtonIsCalculate)
                {
                    leftValue = double.Parse(Number);
                    rightValue = double.Parse(Expresion[(indexOperation+1) .. ^1]);    
                }
                else
                {
                    leftValue = double.Parse(Expresion[..indexOperation]);
                    rightValue = double.Parse(Number);
                }

                double result = 0;

                char operation = Expresion[indexOperation];

                if (operation == '+')
                {
                    result = leftValue + rightValue;
                }
                else if (operation == '-')
                {
                    result = leftValue - rightValue;
                }
                else if (operation == '×')
                {
                    result = (double)leftValue * rightValue;
                }
                else
                {
                    result = (double)leftValue / rightValue;
                }

                result = Math.Round(result, 12);

                Number = result.ToString();


                Expresion = leftValue.ToString()+operation+rightValue.ToString()+'=';
                

                lastButtonIsCalculate = true;
                lastButtonIsOper = false;

                HistoryItems.Add(new HistoryItem(Expresion, Number));
                HistoryMessage = string.Empty;
            }
            catch
            {
                MessageBox.Show("Что то с операцией", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private RelayCommand calculateExpressionCommand;

        public RelayCommand CalculateExpressionCommand
        {
            get
            {
                return calculateExpressionCommand ?? new RelayCommand(obj =>
                {
                    CalculateExpression();
                });
            }
        }

        private void SimpleOperation(object obj)
        {
            if (obj is Button btn)
            {
                var content = btn.Content as TextBlock;

                Expresion = Number + content.Text;
                lastButtonIsOper = true;
                lastButtonIsCalculate = false;
            }
        }

        private RelayCommand simpleOperationCommand;

        public RelayCommand SimpleOperationCommand
        {
            get
            {
                return simpleOperationCommand ?? new RelayCommand(obj =>
                {
                    SimpleOperation(obj);
                });
            }
        }

        private RelayCommand clearAllNumberCommand;

        public RelayCommand ClearAllNumberCommand
        {
            get
            {
                return clearAllNumberCommand ?? new RelayCommand(obj =>
                {
                    Action<object> clear = (object a) => Number = "0";
                    Action<object> clear1 = (object a) => Expresion = string.Empty;
                    clear.Invoke(null);
                    clear1.Invoke(null);
                });
            }
        }

        private void ReverseNumber()
        {
            if (Number != "0")
            {
                if (double.Parse(Number) > 0)
                {
                    Number = '-' + Number;
                }
                else
                {
                    Number = Number[1..];
                }
            }
        }

        private RelayCommand reverseNumberCommand;

        public RelayCommand ReverseNumberCommand
        {
            get
            {
                return reverseNumberCommand ?? new RelayCommand(obj =>
                {
                    ReverseNumber();
                });
            }
        }

        private void ClearLastSymbol()
        {
            if (Number.Length == 1 || Number.Length == 2 && Number.StartsWith('-'))
            {
                Number = "0";
            }
            else
            {
                Number = Number[..^1];
            }

        }

        private RelayCommand clearLastSymbolCommand;

        public RelayCommand ClearLastSymbolCommand
        {
            get
            {
                return clearLastSymbolCommand ?? new RelayCommand(obj =>
                {
                    ClearLastSymbol();
                });
            }
        }

        #region WindowCommand
        private void MaximizeWindow()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private RelayCommand maximizeWindowCommand;

        public RelayCommand MaximizeWindowCommand
        {
            get
            {
                return maximizeWindowCommand ?? new RelayCommand(obj =>
                {
                    MaximizeWindow();
                });
            }
        }

        private void MinimizeWindow()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal ||
               Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private RelayCommand minimizeWindowCommand;

        public RelayCommand MinimizeWindowCommand
        {
            get
            {
                return minimizeWindowCommand ?? new RelayCommand(obj =>
                {
                    MinimizeWindow();
                });
            }
        }

        private void CloseApp()
        {
            Application.Current.Shutdown();
            Application.Current.MainWindow.Close();
        }

        private RelayCommand closeAppCommand;

        public RelayCommand CloseAppCommand
        {
            get
            {
                return closeAppCommand ?? new RelayCommand(obj =>
                {
                    CloseApp();
                });
            }
        }
        #endregion

        private void ClickOnNumber(object number)
        {
            if (number is Button btn)
            {
                TextBlock textBlock = btn.Content as TextBlock;
                if (textBlock != null)
                {
                    if (lastButtonIsOper)
                    {
                        Number = textBlock.Text;
                    }
                    else
                    {
                        Number += textBlock.Text;
                    }

                    lastButtonIsOper = false;
                    lastButtonIsCalculate = false;
                }
            }
        }

        private RelayCommand clickOnNumberCommand;

        public RelayCommand ClickOnNumberCommand
        {
            get
            {
                return clickOnNumberCommand ?? new RelayCommand(obj =>
                {
                    ClickOnNumber(obj);
                });
            }
        }

        private bool CanClickOnComma()
        {
            if (Number.Contains(",") || (Number.Length < 2 && Number.StartsWith('-')))
            {
                return false;
            }

            return true;
        }

        private RelayCommand clickOnCommaCommand;

        public RelayCommand ClickOnCommaCommand
        {
            get
            {
                return clickOnCommaCommand ?? new RelayCommand(obj =>
                {
                    ClickOnNumber(obj);
                }, obj => CanClickOnComma());
            }
        }

        #endregion

        #region INPC
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
