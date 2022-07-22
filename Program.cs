namespace TicTacToe
{
    class Program
    {
        static void PrintBoard(String[][] board)
        {
            Console.WriteLine("\n");
            for(int row = 0; row < 3; row++){
                Console.Write("| ");
                for( int col = 0; col < 3; col++){
                    Console.Write(board[row][col] + " | ");
                }
                Console.WriteLine("\n");
            }
        }

        static void NextMove(String[][] board, String token){
            // Prompt Player 
            Console.WriteLine($"Player {token}, Make your move by entering a number");
            var input = Console.ReadLine();

            // Place token once valid move is made
            bool validMove = false;
            if(input is not null)
                validMove = PlaceToken(board, input, token);

            while(!validMove)
            {
                Console.WriteLine("Invalid move. Try again.");
                input = Console.ReadLine();
                if(input is not null)
                    validMove = PlaceToken(board, input, token);
            }
        }

        static bool PlaceToken(String[][] board, String input, String token){
            for(int row = 0; row < 3; row++)
            {
                for(int col = 0; col < 3; col++){
                    if(board[row][col] == input)
                    {
                        board[row][col] = token;
                        PrintBoard(board);
                        return true;
                    }
                }
            }
            return false;
        }

        static bool CheckForWin(String[][] board)
        {
            //Check rows
            for(int i = 0; i < 3; i++)
                if (String.Equals(board[i][0], board[i][1]) && String.Equals(board[i][1], board[i][2]))
                    return true;
            
            // Check columns
            for(int i = 0; i < 3; i++)
                if (String.Equals(board[0][i], board[1][i]) && String.Equals(board[1][i], board[2][i]))
                    return true;

            // Check Diagonals
            if (String.Equals(board[0][0], board[1][1]) && String.Equals(board[1][1], board[2][2]))
                return true;
            if (String.Equals(board[0][2], board[1][1]) && String.Equals(board[1][1], board[2][0]))
                return true;
            else
                return false;
        }

        static String SwitchCurrentPlayer(String token){
            return String.Equals(token, "X") ? "O" : "X";
        }
       
        static void Main(string[] args)
        {   
            // Create new Board
            String[][] board = 
            {
                new String[] {"1", "2", "3"},
                new String[] {"4", "5", "6"},
                new String[] {"7", "8", "9"}
            };

            int turnsElapsed = 0;
            var currentPlayer = "X";

            // Print board
            PrintBoard(board);

            // Let players make five turns before checking for possible win
            for(int turn = 0; turn < 5; turn++)
            {
                NextMove(board, currentPlayer);
                currentPlayer = SwitchCurrentPlayer(currentPlayer);
                turnsElapsed++;
            }

            // Check for win
            bool winState = CheckForWin(board);
            while(turnsElapsed < 9)
            {
                if(winState)
                {
                    currentPlayer = SwitchCurrentPlayer(currentPlayer);
                    Console.WriteLine($"Player {currentPlayer} wins!");
                    Environment.Exit(0);
                }
                else
                {
                    NextMove(board, currentPlayer);
                    currentPlayer = SwitchCurrentPlayer(currentPlayer);
                    turnsElapsed++;
                    winState = CheckForWin(board);
                }

            }

            Console.WriteLine("It's a Tie");
            Environment.Exit(0);
        }
    }
}
