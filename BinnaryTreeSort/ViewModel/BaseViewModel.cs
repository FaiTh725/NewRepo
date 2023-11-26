using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BinnaryTreeSort.Model;
using BinnaryTreeSort.Extension;

namespace BinnaryTreeSort.ViewModel
{
    internal class BaseViewModel : INotifyPropertyChanged
    {

        public BaseViewModel()
        { 
            binnaryTree = new DrawingTree();
        }

        #region Propertyes

        private string sortedArrayString;

        public string SortedArrayString
        {
            get => sortedArrayString;
            set
            {
                sortedArrayString = string.Join(" ", binnaryTree.GetSortedList());
                OnPropertyChanged(nameof(SortedArrayString));
            }
        }

        private double addValue;

        public double AddValue
        {
            get => addValue;
            set
            {
                addValue = value;
                OnPropertyChanged(nameof(AddValue));
            }
        }

        private DrawingTree binnaryTree;

        #endregion

        #region Commands

        private void ClearTree(object obj)
        {
            if(obj is Canvas canvas)
            {
                binnaryTree.Clear();
                SortedArrayString = "";
                canvas.Children.Clear();    
            }
            
        }

        private RelayCommand clearTreeCommand;

        public RelayCommand ClearTreeCommand
        {
            get
            {
                return clearTreeCommand ?? new RelayCommand(obj => 
                {
                    ClearTree(obj);
                });
            }
        }

        private RelayCommand getSortedArrayCommand;

        public RelayCommand GetSortedArrayCommand
        {
            get
            {
                return getSortedArrayCommand ?? new RelayCommand(obj => 
                {
                    Action act = () => { SortedArrayString = ""; };

                    act();
                });
            }
        }

        private void AddNode(object obj)
        {
            if (obj is Canvas canvas)
            {
                binnaryTree.Add(AddValue);
                AddValue = 0;
                binnaryTree.DrawTree(canvas);
            }
        }

        private RelayCommand addNodeCommand;

        public RelayCommand AddNodeCommand
        {
            get
            {
                return addNodeCommand ?? new RelayCommand(obj => 
                {
                    AddNode(obj);
                });
            }
        }

        #region WindowCommand

        private void MaximizeApp()
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

        private RelayCommand maximizeAppCommand;

        public RelayCommand MaximizeAppCommand
        {
            get
            {
                return maximizeAppCommand ?? new RelayCommand(obj => 
                { 
                    MaximizeApp();
                });
            }
        }

        private void MinimizeApp()
        {
            if(Application.Current.MainWindow.WindowState == WindowState.Normal 
            || Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private RelayCommand minimizeAppCommand;

        public RelayCommand MinimizeAppCommand
        {
            get
            {
                return minimizeAppCommand ?? new RelayCommand(obj => 
                {
                    MinimizeApp();
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
