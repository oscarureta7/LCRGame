using System;
using System.Collections.Generic;
using System.Text;

using Prism.Mvvm;

using LCRGame.Models;
using LCRGame.Services;

namespace LCRGame.ViewModels
{
    // NOTE: INotifyPropertyChanged is already implemented by BindableBase
    public class LCRGameViewModel : BindableBase
    {
        private const int default_player_chips = 3;

        private IDiceService _diceService;
        private ISimulationService _simulationService;


        private string _selectedPlayerName;
        public string SelectedPlayerName
        {
            get => _selectedPlayerName;
            set => _selectedPlayerName = value;
        }

        private List<Player> _players;
        public List<Player> Players {
            get => _players;
            set => _players = value;
        }

        private int _matches;
        public int Matches
        {
            get => _matches;
            set => _matches = value;
        }

        public LCRGameViewModel(IDiceService diceService, ISimulationService simulationService) {
            _diceService = diceService;
            _simulationService = simulationService;

            Players = new List<Player>();

            Players.Add(new Player(default_player_chips,"Player 1"));
            Players.Add(new Player(default_player_chips, "Player 2"));
            Players.Add(new Player(default_player_chips, "Player 3"));
        }
    }
}
