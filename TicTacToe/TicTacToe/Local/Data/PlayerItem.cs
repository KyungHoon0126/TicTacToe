using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Local.Mvvm;

namespace TicTacToe.Local.Data
{
    public class PlayerItem : ObservableObject
    {
        private string _playerName;
        private bool _isTurn;
        private int _wintCount;
        private ObservableCollection<string> _result;

        public string PlayerName
        {
            get => _playerName;
            set
            {
                _playerName = value;
                OnPropertyChanged();
            }
        }

        public int Wincount
        {
            get => _wintCount;
            set
            {
                _wintCount = value;
                OnPropertyChanged();
            }
        }

        public bool IsTurn
        {
            get => _isTurn;
            set
            {
                _isTurn = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        public PlayerItem(string name)
        {
            PlayerName = name;
            Result = new();
            Result.CollectionChanged += Result_CollectionChanged;
        }

        private void Result_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            Wincount = Result.Where(x => x.Equals("W")).Count();
        }

        internal void SwitchTurn()
        {
            IsTurn = !IsTurn;
        }
    }
}
