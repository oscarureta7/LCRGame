using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Commands;

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

        private Player _selectedPlayer;
        public Player SelectedPlayer
        {
            get => _selectedPlayer;
            set => _selectedPlayer = value;
        }

        private ObservableCollection<Player> _players;
        public ObservableCollection<Player> Players {
            get => _players;
            set => _players = value;
        }

        private ObservableCollection<LCRSimulationResult> _output;
        public ObservableCollection<LCRSimulationResult> Output
        {
            get => _output;
        }

        private ICommand _addPlayerCommand;
        public ICommand AddPlayerCommand
        {
            get => _addPlayerCommand;
        }

        private ICommand _removePlayerCommand;
        public ICommand RemovePlayerCommand
        {
            get => _removePlayerCommand;
        }

        private DelegateCommand<int?> _startSimulationCommand;
        public DelegateCommand<int?> StartSimulationCommand
        {
            get => _startSimulationCommand;
        }

        public LCRGameViewModel(IDiceService diceService, ISimulationService simulationService) {
            _diceService = diceService;
            _simulationService = simulationService;

            _players = new ObservableCollection<Player>();
            _output = new ObservableCollection<LCRSimulationResult>();

            _addPlayerCommand = new DelegateCommand(OnAddPlayer);
            _removePlayerCommand = new DelegateCommand(OnRemovePlayer);
            _startSimulationCommand = new DelegateCommand<int?>(OnStartSimulation);
        }

        public void OnAddPlayer() {
            if (!string.IsNullOrEmpty(_selectedPlayerName) && !_players.Contains(_selectedPlayer))
            {
                Players.Add(new Player(_selectedPlayerName));
                SelectedPlayerName = string.Empty;
            }
        }
        public void OnRemovePlayer() {
            if (_players.Contains(_selectedPlayer))
            {
                Players.Remove(_selectedPlayer);
                SelectedPlayerName = string.Empty;
            }
        }

        public void OnStartSimulation(int? matches) {

            if (matches == null)
                return;

            LCRSimulationResult result =_simulationService.Simulate(
                _players.ToList<Player>().ToArray(), default_player_chips, matches.Value, _diceService);
            Output.Add(result);
        }
    }
}
