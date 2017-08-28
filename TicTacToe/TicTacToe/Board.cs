using System;
using System.Text;

namespace TicTacToe
{
    public class Board
    {
        public int BoardSize { get; }
        private readonly char[,] _gameBoard;

        public Board(int boardSize)
        {

            BoardSize = boardSize;
            _gameBoard = new char[boardSize,boardSize];
        }

        public char GetCurrentSymbol(int row, int col)
        {
            return _gameBoard[row, col];
        }

        public void UpdateBoard(int row, int col, char symbol)
        {

            _gameBoard[row, col] = symbol;
        }

        public void DrawBoard()
        {
            var firstLine = new StringBuilder();
            firstLine.Append(" ");
            for (var i = 1; i <= BoardSize; i++)
                firstLine.Append(" " + i);

            Console.WriteLine(firstLine);
            Console.WriteLine();

            var boardLineTop = new StringBuilder();
            var boardLineBottom = new StringBuilder();
            for (var i = 0; i < BoardSize; i++)
            {
                boardLineTop.Clear();
                boardLineBottom.Clear();
                boardLineTop.Append((i + 1) + " ");
                boardLineBottom.Append(" ");

                for (var j = 0; j < BoardSize; j++)
                {
                    boardLineTop.Append(_gameBoard[i, j]);
                    if (j != BoardSize - 1)
                        boardLineTop.Append("\u2502");

                    boardLineBottom.Append(" \u2500");
                }

                Console.WriteLine(boardLineTop);
                if (i != BoardSize - 1)
                    Console.WriteLine(boardLineBottom);
            }

            Console.WriteLine();
        }

        public void ClearBoard()
        {
            for (var row = 0; row < BoardSize; row++)
            {
                for (var col = 0; col < BoardSize; col++)
                {
                    _gameBoard[row, col] = '\0';
                    
                }
            }
        }

    }
}