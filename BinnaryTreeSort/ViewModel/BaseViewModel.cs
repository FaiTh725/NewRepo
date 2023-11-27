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
using System.Security.Permissions;
using BinnaryTreeSort.Resourses;
using System.Windows.Media;

namespace BinnaryTreeSort.ViewModel
{
    internal class BaseViewModel : INotifyPropertyChanged
    {

        public BaseViewModel()
        { 
            binnaryTree = new DrawingTree();
        }

        #region Propertyes



        private string errorMessage;

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        private double? searchValue;

        public double? SearchValue
        {
            get=> searchValue;
            set
            {
                searchValue = value;
                OnPropertyChanged(nameof(SearchValue));
            }
        }

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

        private double? removeValue;

        public double? RemoveValue
        {
            get => removeValue;

            set
            {
                removeValue = value;
                OnPropertyChanged(nameof(RemoveValue));
            }
        }

        private double? addValue;

        public double? AddValue
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

        private async void SorteArray(object obj)
        {
            if(obj is Canvas canvas)
            {
                
                SortedArrayString = binnaryTree.SorteArray(canvas);
              
            }

            await Task.Delay(600 * binnaryTree.GetSortedList().Count);
            ClearTree(obj as Canvas);
        }

        private RelayCommand sorteArrayCommand;

        public RelayCommand SorteArrayCommand
        {
            get
            {
                return sorteArrayCommand ?? new RelayCommand(obj => 
                {
                    SorteArray(obj);
                });
            }
        }

        private void SearchNode(object obj)
        {
            if(SearchValue == null)
            {
                ErrorMessage = "Поле поиска пустое";
                return;
            }

            if(obj is Canvas canvas)
            {
                foreach(var item in canvas.Children)
                {
                    if (item is DrawingNode node)
                    {
                        node.BackGroundEllipse = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#09521c"));
                    }
                }

                binnaryTree.SearchNode(canvas, SearchValue);
                SearchValue = null;
            }
        }

        private RelayCommand searchNodeCommand;

        public RelayCommand SearchNodeCommand
        {
            get
            {
                return searchNodeCommand ?? new RelayCommand(obj => 
                {
                    SearchNode(obj);
                });
            }
        }

        private void ClearTree(object obj)
        {
            if(obj is Canvas canvas)
            {
                binnaryTree.Clear();
                SortedArrayString = "";
                ErrorMessage = string.Empty;
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


        private void RemoveNode(object obj)
        {
            if (RemoveValue == null)
            {
                ErrorMessage = "В поле удаления пусто";
                return;
            }
            if(obj is Canvas canvas)
            {

                binnaryTree.Remove(RemoveValue);
                canvas.Children.Clear();
                binnaryTree.DrawTree(canvas);
                
                RemoveValue = null;
                ErrorMessage = null;
            }
        }

        private RelayCommand removeNodeCommand;

        public RelayCommand RemoveNodeCommand
        {
            get
            {
                return removeNodeCommand ?? new RelayCommand(obj => 
                {
                    RemoveNode(obj);        
                });
            }
        }

        private void AddNode(object obj)
        {
            if (obj is Canvas canvas)
            {
                if (AddValue == null)
                {
                    ErrorMessage = "Поле ввода пустое";
                    return;
                }
                try
                {
                    binnaryTree.Add(AddValue);
                    AddValue = null;
                    binnaryTree.DrawTree(canvas);
                    ErrorMessage = string.Empty;
                }
                catch(ArgumentException e)
                {
                    ErrorMessage = e.Message;
                }
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
