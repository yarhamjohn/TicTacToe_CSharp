using System;

namespace TicTacToe
{
    public class Game
    {
        public bool ActiveGame { get; private set; }
        private char _winningPlayer;
        public Player CurrentPlayer { get; set; }

        public Game(Player currentPlayer)
        {
            ActiveGame = true;
            CurrentPlayer = currentPlayer;
        }

        public void SwitchPlayer(Player[] playerList)
        {
            CurrentPlayer = playerList[0] == CurrentPlayer ? playerList[1] : playerList[0];
        }

        public char ReturnWinner()
        {
            return _winningPlayer;
        }

        public bool CheckWinner(Board gameBoard)
        {
            char playerSymbol;
            int playerCounter;

            // Checks rows
            for (var i = 0; i < gameBoard.BoardSize; i++)
            {
                playerCounter = 0;
                playerSymbol = gameBoard.GetCurrentSymbol(i, 0);

                for (var j = 0; j < gameBoard.BoardSize; j++)
                {
                    if (gameBoard.GetCurrentSymbol(i, j) == '\0')
                        break;
                    
                    if (playerSymbol != gameBoard.GetCurrentSymbol(i, j))
                        break;

                    playerCounter++;
                }

                if (playerCounter == gameBoard.BoardSize)
                {
                    ActiveGame = false;
                    _winningPlayer = playerSymbol;
                    return true;
                }
            }

            // Checks cols
            for (var i = 0; i < gameBoard.BoardSize; i++)
            {
                playerCounter = 0;
                playerSymbol = gameBoard.GetCurrentSymbol(0, i);

                for (var j = 0; j < gameBoard.BoardSize; j++)
                {
                    if (gameBoard.GetCurrentSymbol(j, i) == '\0')
                        break;

                    if (playerSymbol != gameBoard.GetCurrentSymbol(j, i))
                        break;

                    playerCounter++;
                }

                if (playerCounter == gameBoard.BoardSize)
                {
                    ActiveGame = false;
                    _winningPlayer = playerSymbol;
                    return true;
                }
            }

            // Checks downward diagonal
            playerCounter = 0;
            playerSymbol = gameBoard.GetCurrentSymbol(0, 0);

            for (var i = 0; i < gameBoard.BoardSize; i++)
            {
                if (gameBoard.GetCurrentSymbol(i, i) == '\0')
                    break;

                if (playerSymbol != gameBoard.GetCurrentSymbol(i, i))
                    break;

                playerCounter++;
            }

            if (playerCounter == gameBoard.BoardSize)
            {
                ActiveGame = false;
                _winningPlayer = playerSymbol;
                return true;
            }

            // Checks upward diagonal
            playerCounter = 0;
            playerSymbol = gameBoard.GetCurrentSymbol(0, gameBoard.BoardSize - 1);

            for (var i = 0; i < gameBoard.BoardSize; i++)
            {
                if (gameBoard.GetCurrentSymbol(i, gameBoard.BoardSize - 1 - i) == '\0')
                    break;

                if (playerSymbol != gameBoard.GetCurrentSymbol(i, gameBoard.BoardSize - 1 - i))
                    break;

                playerCounter++;
            }

            if (playerCounter == gameBoard.BoardSize)
            {
                ActiveGame = false;
                _winningPlayer = playerSymbol;
                return true;
            }

            return false;
        }

        public bool CheckDraw(Board gameBoard)
        {
            var counter = 0;
            for (var i = 0; i < gameBoard.BoardSize; i++)
            {
                for (var j = 0; j < gameBoard.BoardSize; j++)
                {
                    if (gameBoard.GetCurrentSymbol(i, j) != '\0')
                        counter++;
                }
            }

            if (counter == (gameBoard.BoardSize * gameBoard.BoardSize))
            {
                ActiveGame = false;
                return true;
            }

            return false;
        }
        
    }
}