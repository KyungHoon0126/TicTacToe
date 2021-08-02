﻿using System.Windows.Input;
using TicTacToe.Local.Mvvm;

namespace TicTacToe.Local.Data
{
    public class SquareItem : ObservableObject
    {
        public ICommand ChoiceCommand {  get; set; }
        
        private Player _player;
        public Player Player
        {
            get => _player;
            set
            {
                _player = value;
                OnPropertyChanged();
            }
        }

        public string WinnerMessage => "Win: " + Player;

        public SquareItem(ICommand choice)
        {
            ChoiceCommand = choice;
        }
    }
}
