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
        private const int default_minimum_players = 3;
        private const int default_maximum_players = 7;

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
        }

        private ObservableCollection<LCRSimulationResult> _output;
        public ObservableCollection<LCRSimulationResult> Output
        {
            get => _output;
        }

        private DelegateCommand _addPlayerCommand;
        public DelegateCommand AddPlayerCommand
        {
            get => _addPlayerCommand;
            private set => _addPlayerCommand = value;
        }

        private DelegateCommand _removePlayerCommand;
        public DelegateCommand RemovePlayerCommand
        {
            get => _removePlayerCommand;
            private set => _removePlayerCommand = value;
        }

        private DelegateCommand<int?> _startSimulationCommand;
        public DelegateCommand<int?> StartSimulationCommand
        {
            get => _startSimulationCommand;
            private set => _startSimulationCommand = value;
        }

        public LCRGameViewModel(IDiceService diceService, ISimulationService simulationService) {
            _diceService = diceService;
            _simulationService = simulationService;

            _players = new ObservableCollection<Player>();
            _output = new ObservableCollection<LCRSimulationResult>();

            _addPlayerCommand = new DelegateCommand(OnAddPlayer, CanAdd);
            _removePlayerCommand = new DelegateCommand(OnRemovePlayer, CanRemove);
            _startSimulationCommand = new DelegateCommand<int?>(OnStartSimulation, CanSimulate);

            // Add default players
            _players.Add(new Player("Andrea"));
            _players.Add(new Player("Abigail"));
            _players.Add(new Player("Omar"));
        }

        public void OnAddPlayer() {
            if (!string.IsNullOrEmpty(_selectedPlayerName))
            {
                Players.Add(new Player(_selectedPlayerName));
                SelectedPlayerName = string.Empty;

                // NOTE: Added manual invocation of RaiseCanExecuteChanged due to time constraints.
                StartSimulationCommand.RaiseCanExecuteChanged();
                AddPlayerCommand.RaiseCanExecuteChanged();
                RemovePlayerCommand.RaiseCanExecuteChanged();
            }
        }
        public void OnRemovePlayer() {
            if (_players.Contains(_selectedPlayer) && _players.Count > default_minimum_players)
            {
                Players.Remove(_selectedPlayer);
                SelectedPlayerName = string.Empty;

                // NOTE: Added manual invocation of RaiseCanExecuteChanged due to time constraints.
                StartSimulationCommand.RaiseCanExecuteChanged();
                AddPlayerCommand.RaiseCanExecuteChanged();
                RemovePlayerCommand.RaiseCanExecuteChanged();
            }
        }

        public void OnStartSimulation(int? matches) {

            if (matches == null || _players.Count < default_minimum_players)
                return;

            LCRSimulationResult result =_simulationService.Simulate(
                _players.ToList<Player>().ToArray(), default_player_chips, matches.Value, _diceService);
            Output.Add(result);
        }

        public bool CanSimulate(int? matches)
        {
            return Players.Count >= default_minimum_players;
        }

        public bool CanRemove()
        {
            return Players.Count > default_minimum_players;
        }

        public bool CanAdd()
        {
            return Players.Count < default_maximum_players;
        }
    }
}
