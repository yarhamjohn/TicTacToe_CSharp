namespace TicTacToe
{
    public class Player
    {
        public string PlayerName { get; set; }
        public char PlayerSymbol { get; }
        public bool CurrentPlayer { get; set; }

        public Player(char playerSymbol)
        {
            PlayerSymbol = playerSymbol;
        }
        
    }
}