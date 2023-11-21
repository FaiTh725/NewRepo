using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MyCalc.Model;

namespace MyCalc.ViewModels
{
    public class BaseViewModel: INotifyPropertyChanged
    {
        public BaseViewModel() 
        {
            Number = "0";
            Expresion = string.Empty;
        }

        #region Propertyes

        private string number;

        public string Number
        {
            get => number;
            set
            {
                if (number == "0" && value != "0" && value !="0,")
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


        private RelayCommand clearAllNumberCommand;

        public RelayCommand ClearAllNumberCommand
        {
            get 
            {
                return clearAllNumberCommand ?? new RelayCommand(obj => 
                {
                    Action<object> clear = (object a) => Number = "0";
                    clear.Invoke(null);
                });
            }
        }

        private void ReverseNumber()
        {
            if(Number != "0")
            {
                if(double.Parse(Number)>0)
                {
                    Number = '-'+Number;
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
            if(Number.Length == 1 || Number.Length == 2 && Number.StartsWith('-'))
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
            if(Application.Current.MainWindow.WindowState == WindowState.Normal)
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
            if(Application.Current.MainWindow.WindowState == WindowState.Normal ||
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
            if(number is Button btn)
            {
                TextBlock textBlock = btn.Content as TextBlock;
                if (textBlock != null) 
                {
                    Number += textBlock.Text;
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
            if(Number.Contains(",") || (Number.Length<2 && Number.StartsWith('-')))
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
                },obj => CanClickOnComma());
            }
        }

        #endregion

        #region INPC
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
