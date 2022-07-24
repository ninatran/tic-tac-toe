namespace TicTacToe
{
    class Program
    {
        static void PrintBoard(String[][] board)
        {
            Console.Clear();
            Console.WriteLine("\n");
            for(int row = 0; row < 3; row++){
                Console.Write("| ");
                for( int col = 0; col < 3; col++){
                    Console.Write(board[row][col] + " | ");
                }
                Console.WriteLine("\n");
            }
        }

        static void NextMove(String[][] board, String token)
        {
            // Prompt current player to move
            Console.WriteLine($"\nPlayer {token}, Make your move by entering a number");
            var input = Console.ReadLine();

            // Place token if valid move selected and print board
            bool validMove = false;
            if(input is not null)
            {
                validMove = PlaceToken(board, input, token);
                PrintBoard(board);
            }
            while(!validMove)
            {
                Console.WriteLine("Invalid move. Try again.");
                input = Console.ReadLine();
                if(input is not null)
                {
                    validMove = PlaceToken(board, input, token);
                    PrintBoard(board);
                }
            }
        }

        static bool PlaceToken(String[][] board, String input, String token)
        {
            // Validate move and place token. Return false if invalid move.
            for(int row = 0; row < 3; row++)
            {
                for(int col = 0; col < 3; col++){
                    if(board[row][col] == input)
                    {
                        board[row][col] = token;
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

        static String SwitchCurrentPlayer(String token)
        {
            return String.Equals(token, "X") ? "O" : "X";
        }

        static bool PlayAgain()
        {
            Console.WriteLine("\nWould you like to play again? Y/N");
            var input = Console.ReadLine();
            if(input is not null)
            {
                bool validInput = input is not null && (String.Equals(input.ToUpper(),"Y") || String.Equals(input.ToUpper(),"N")) ? true : false;
                while(!validInput)
                {
                    Console.WriteLine("Invalid choice. Enter Y to play again or N to exit the program.");
                    input = Console.ReadLine();
                    validInput = input is not null && (String.Equals(input.ToUpper(),"Y") || String.Equals(input.ToUpper(),"N")) ? true : false;
                }
                if(input is not null && String.Equals(input.ToUpper(),"Y")) return true;
                else return false;
            }
            else 
            {   
                Console.WriteLine("Null input Error. The program will now exit.");
                Environment.Exit(0);
                return false; 
            }
        }
       
        static void IncrementScore(Dictionary<String, int> scores, String result){
            int count;
            scores.TryGetValue(result, out count);
            scores[result] = count + 1;
        }
       
       static bool NewGame(Dictionary<String, int> scores)
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
            
            // Let users take turns until board is filled or someone wins
            while(turnsElapsed < 9)
            {
                if(winState)
                {
                    currentPlayer = SwitchCurrentPlayer(currentPlayer);
                    IncrementScore(scores, currentPlayer);
                    Console.WriteLine($"Player {currentPlayer} wins!");
                    // Ask if the user wants to play again
                    return PlayAgain();
                }
                else
                {
                    NextMove(board, currentPlayer);
                    currentPlayer = SwitchCurrentPlayer(currentPlayer);
                    turnsElapsed++;
                    winState = CheckForWin(board);
                }

            }

            Console.WriteLine("It's a Draw!");
            IncrementScore(scores, "Draw");

            // Ask if the user wants to play again
            return PlayAgain();

       }
        
        static void Main(string[] args)
        {  
            bool startNewGame = true;

            Dictionary<string, int> scores = new Dictionary<string, int>();
            scores.Add("X", 0);
            scores.Add("O", 0);
            scores.Add("Draw", 0);

            while( startNewGame )
            {
                startNewGame = NewGame(scores);
            }

            Console.Clear();
            Console.WriteLine("\nX wins: {0} | O wins: {1} | Draw: {2}", scores["X"], scores["O"], scores["Draw"]);
            Console.WriteLine("\nThanks for playing!");
            Environment.Exit(0);
        }
    }
}
