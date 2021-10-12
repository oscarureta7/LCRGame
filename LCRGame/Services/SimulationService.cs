using System;
using System.Collections.Generic;
using System.Text;

using LCRGame.Models;

namespace LCRGame.Services
{
    public class SimulationService : ISimulationService
    {
        public LCRSimulationResult Simulate(Player[] players, int initialChips, int totalRounds, IDiceService diceService)
        {
            LCRSimulationResult simulationResult = new LCRSimulationResult();
            Player currentPlayer;
            Player targetPlayer;

            int currentMatchTurns = 0;
            int totalSimulationTurns = 0;
            int remainingPlayers = 0;
            int currentPlayerIndex = 0;
            int centerPoolChips = 0;
            LCRDiceSides[] rollResult;

            // Simulationloop
            for(int round = 0; round < totalRounds; round++)
            {
                // Setup game
                for(int playerIndex = 0; playerIndex < players.Length; playerIndex++)
                {
                    players[playerIndex].Chips = initialChips;
                    players[playerIndex].Active = true;
                }

                remainingPlayers = players.Length;
                currentPlayerIndex = 0;
                currentMatchTurns = 0;

                // Game loop
                while(remainingPlayers > 1)
                {
                    currentPlayer = players[currentPlayerIndex];

                    if (currentPlayer.Active)
                    {
                        if (currentPlayer.Chips > 3)
                            rollResult = diceService.RollDice(3);
                        else
                            rollResult = diceService.RollDice(currentPlayer.Chips);

                        for(int dieIndex = 0; dieIndex < rollResult.Length; dieIndex++)
                        {
                            switch (rollResult[dieIndex])
                            {
                                
                                case LCRDiceSides.R:
                                    targetPlayer = GetPlayerAtRight(currentPlayerIndex, players);
                                    --currentPlayer.Chips;
                                    ++targetPlayer.Chips;
                                    if (!targetPlayer.Active)
                                    {
                                        targetPlayer.Active = true;
                                        ++remainingPlayers;
                                    }
                                    break;
                                case LCRDiceSides.L:
                                    targetPlayer = GetPlayerAtLeft(currentPlayerIndex, players);
                                    --currentPlayer.Chips;
                                    ++targetPlayer.Chips;
                                    targetPlayer.Active = true;
                                    if (!targetPlayer.Active)
                                    {
                                        targetPlayer.Active = true;
                                        ++remainingPlayers;
                                    }
                                    break;
                                case LCRDiceSides.C:
                                    --currentPlayer.Chips;
                                    ++centerPoolChips;
                                    break;
                                case LCRDiceSides.Dot:
                                default:
                                    break;
                            }
                        }

                        if (currentPlayer.Chips <= 0 && currentPlayer.Active)
                        {
                            currentPlayer.Active = false;
                            --remainingPlayers;
                        }
                        ++currentMatchTurns;

                        ++currentPlayerIndex;
                        if (currentPlayerIndex >= players.Length - 1)
                            currentPlayerIndex = 0;
                    } else
                    {
                        currentPlayerIndex++;
                        if (currentPlayerIndex >= players.Length - 1)
                            currentPlayerIndex = 0;
                        continue;
                    }
                }

                if (simulationResult.ShortestGame > currentMatchTurns || simulationResult.ShortestGame == 0)
                    simulationResult.ShortestGame = currentMatchTurns;

                if (simulationResult.LongestGame < currentMatchTurns || simulationResult.LongestGame == 0)
                    simulationResult.LongestGame = currentMatchTurns;

                totalSimulationTurns += currentMatchTurns;
            }

            simulationResult.AverageLength = totalSimulationTurns / totalRounds;
            simulationResult.TimeStamp = DateTime.Now;

            return simulationResult;
        }

        private Player GetPlayerAtLeft(int currentPlayer, Player[] players)
        {
            if (currentPlayer == 0)
                return players[players.Length - 1];
            else
                return players[currentPlayer - 1];
        }

        private Player GetPlayerAtRight(int currentPlayer, Player[] players)
        {
            if (currentPlayer + 1 >= players.Length)
                return players[0];
            else
                return players[currentPlayer + 1];
        }
    }
}
