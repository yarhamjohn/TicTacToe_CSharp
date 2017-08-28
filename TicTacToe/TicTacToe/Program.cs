using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            //*************************
            // Currently can only be 2 player game
            // ************************


            // Welcome to the game
            Console.WriteLine("Welcome to TicTacToe!");
            Console.WriteLine();


            GameSelection:
            // Ask if its a 1 or 2 player game
            Console.Write("Do you want to play a one-player game or two-player game (enter 1 or 2)? ");
            var numPlayers = Console.ReadLine();

            if (numPlayers != "1" && numPlayers != "2")
            {
                Console.WriteLine("You did not enter a valid number (1 or 2). Please try again.");
                goto GameSelection;
            }

            var playerList = new Player[2];
            Console.WriteLine();


            // Set up each player, getting player names and symbols
            for (var i = 0; i < Convert.ToInt32(numPlayers); i++)
            {
                PlayerNameInit:
                Console.Write("Player {0} - Please enter your name: ", i + 1);
                var playerName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(playerName))
                {
                    Console.WriteLine("You did not enter a name. Please try again.");
                    goto PlayerNameInit;
                }

                Console.WriteLine();

                PlayerSymbolInit:
                Console.Write("Player {0} - Please choose your symbol (A-Z): ", i + 1);
                var playerSymbol = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(playerSymbol) || playerSymbol.Length != 1 ||
                    (Convert.ToChar(playerSymbol.ToUpper()) < 'A' || Convert.ToChar(playerSymbol.ToUpper()) > 'Z'))
                {
                    Console.WriteLine("You did not enter a valid symbol. Please try again");
                    goto PlayerSymbolInit;
                }
                
                playerList[i] = new Player(Convert.ToChar(playerSymbol.ToUpper()))
                {
                    PlayerName = playerName
                };

                Console.WriteLine();

            }

            // Add computer player if 1 player game chosen
            if (Convert.ToInt32(numPlayers) == 1)
            {
                if (playerList[0].PlayerSymbol == 'X')
                    playerList[1] = new Player('O');
                else playerList[1] = new Player('X');

                playerList[1].PlayerName = "Computer";
            }


            // Ask how big a board to play (3 - 10) and set up board
            BoardSizeInit:
            Console.Write("How big a board would you like to play (3-10)? ");
            var boardSize = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(boardSize) || !(int.TryParse(boardSize, out int o)) || (Convert.ToInt32(boardSize) < 3 || Convert.ToInt32(boardSize) > 10))
            {
                Console.WriteLine("You did not choose a valid board size. Please try again.");
                goto BoardSizeInit;
            }
            var gameBoard = new Board(Convert.ToInt32(boardSize));
            
            
            // Loop until game ends:
            var game = new Game(playerList[0]);
                
            while (game.ActiveGame)
            {
                // Draw the board
                gameBoard.DrawBoard();

                if (game.CurrentPlayer.PlayerName == "Computer")
                {
                    Console.WriteLine("It's the computer's turn!");
                    var valid = false;

                    //Randomly pick an empty square
                    while (!valid)
                    {
                        var randRow = new Random();
                        int row = randRow.Next(0, gameBoard.BoardSize);

                        var randCol = new Random();
                        int col = randCol.Next(0, gameBoard.BoardSize);

                        if (gameBoard.GetCurrentSymbol(row, col) ==
                            '\0')
                        {
                            gameBoard.UpdateBoard(row, col, game.CurrentPlayer.PlayerSymbol);
                            valid = true;
                        }
                    }
                }

                else
                {
                    MoveSelection:
                    // Get player selection and play move
                    Console.WriteLine("{0}: Please choose where to make your move.", game.CurrentPlayer.PlayerName);

                    RowSelection:
                    Console.Write("Enter your row number: ");
                    var row = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(row) || !(int.TryParse(row, out int m)) || (Convert.ToInt32(row) < 1 || Convert.ToInt32(row) > gameBoard.BoardSize))
                    {
                        Console.WriteLine("You did not enter a valid row number. Please try again.");
                        goto RowSelection;
                    }
                    
                    ColSelection:
                    Console.Write("Enter your column number: ");
                    var col = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(col) || !(int.TryParse(col, out int n)) || (Convert.ToInt32(col) < 1 || Convert.ToInt32(col) > gameBoard.BoardSize))
                    {
                        Console.WriteLine("You did not enter a valid column number. Please try again.");
                        goto ColSelection;
                    }

                    if (gameBoard.GetCurrentSymbol(Convert.ToInt32(row) - 1, Convert.ToInt32(col) - 1) != '\0')
                    {
                        Console.WriteLine("This position has already been filled! Please make another selection.");
                        goto MoveSelection;
                    }

                    gameBoard.UpdateBoard(Convert.ToInt32(row) - 1, Convert.ToInt32(col) - 1, game.CurrentPlayer.PlayerSymbol);
                }


                // Check if game is won
                if (game.CheckWinner(gameBoard))
                {
                    Console.WriteLine("The game was won!");
                    Console.WriteLine();

                    gameBoard.DrawBoard();

                    // Get winner
                    foreach (Player player in playerList)
                    {
                        if (player.PlayerSymbol == game.ReturnWinner())
                            Console.WriteLine("The winner is: " + player.PlayerName);
                    }

                    Console.WriteLine();

                    break;
                }

                // Check if game is drawn
                if (game.CheckDraw(gameBoard))
                {
                    Console.WriteLine("The game was drawn!");
                    Console.WriteLine();

                    gameBoard.DrawBoard();

                    Console.WriteLine();

                    break;
                }

                // Switch player
                game.SwitchPlayer(playerList);
            }

            // Ask if a replay is wanted
            RepeatGame:
            Console.Write("Would you like to play another game (Y/N)? ");
            var answer = Console.ReadLine();

            if (answer.ToUpper() != "Y" && answer.ToUpper() != "N")
            {
                Console.WriteLine("You did not make a valid choice. Please try again.");
                goto RepeatGame;
            }

            if (answer.ToUpper() == "Y")
                goto GameSelection;

        }
    }
}
