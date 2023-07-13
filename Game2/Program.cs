using System;

namespace Game2
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playAgain = true;

            while (playAgain)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Game Collection!");
                Console.WriteLine("Please choose a game:");
                Console.WriteLine("1. Tic Tac Toe");
                Console.WriteLine("2. Rock Paper Scissors");
                Console.WriteLine("3. Guess the Number");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice (1-4): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            TicTacToe();
                            break;
                        case 2:
                            RockPaperScissors();
                            break;
                        case 3:
                            GuessTheNumberGame();
                            break;
                        case 4:
                            playAgain = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }

                if (playAgain)
                {
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }
        }

        static void TicTacToe()
        {
            char[] board = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
            char currentPlayer = 'X';
            bool gameEnded = false;

            while (!gameEnded)
            {
                Console.Clear();
                DrawBoard(board);

                int move = GetPlayerMove();

                if (IsValidMove(move, board))
                {
                    board[move] = currentPlayer;

                    if (IsWinningMove(currentPlayer, board))
                    {
                        Console.Clear();
                        DrawBoard(board);
                        Console.WriteLine("Player {0} wins!", currentPlayer);
                        gameEnded = true;
                    }
                    else if (IsBoardFull(board))
                    {
                        Console.Clear();
                        DrawBoard(board);
                        Console.WriteLine("It's a draw!");
                        gameEnded = true;
                    }
                    else
                    {
                        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move. Press any key to try again.");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void DrawBoard(char[] board)
        {
            Console.WriteLine(" {0} | {1} | {2} ", board[0], board[1], board[2]);
            Console.WriteLine("---+---+---");
            Console.WriteLine(" {0} | {1} | {2} ", board[3], board[4], board[5]);
            Console.WriteLine("---+---+---");
            Console.WriteLine(" {0} | {1} | {2} ", board[6], board[7], board[8]);
        }

        static int GetPlayerMove()
        {
            Console.Write("Player 1, enter your move (0-8): ");
            return int.Parse(Console.ReadLine());

        }

        static bool IsValidMove(int move, char[] board)
        {
            return move >= 0 && move < 9 && board[move] == ' ';
        }

        static bool IsWinningMove(char currentPlayer, char[] board)
        {
            // Check rows
            for (int i = 0; i < 9; i += 3)
            {
                if (board[i] == currentPlayer && board[i + 1] == currentPlayer && board[i + 2] == currentPlayer)
                {
                    return true;
                }
            }

            // Check columns
            for (int i = 0; i < 3; i++)
            {
                if (board[i] == currentPlayer && board[i + 3] == currentPlayer && board[i + 6] == currentPlayer)
                {
                    return true;
                }
            }

            // Check diagonals
            if ((board[0] == currentPlayer && board[4] == currentPlayer && board[8] == currentPlayer) ||
                (board[2] == currentPlayer && board[4] == currentPlayer && board[6] == currentPlayer))
            {
                return true;
            }

            return false;
        }

        static bool IsBoardFull(char[] board)
        {
            foreach (char cell in board)
            {
                if (cell == ' ')
                {
                    return false;
                }
            }

            return true;
        }

        static void RockPaperScissors()
        {
            bool playAgain = true;

            while (playAgain)
            {
                Console.WriteLine("Welcome to Rock, Paper, Scissors!");
                Console.WriteLine("Choose: 1 - Rock, 2 - Paper, 3 - Scissors");
                int playerChoice = GetPlayerChoice();

                if (playerChoice != -1)
                {
                    int computerChoice = GetComputerChoice();
                    Console.WriteLine("Computer chose: " + GetChoiceName(computerChoice));

                    int result = DetermineWinner(playerChoice, computerChoice);

                    if (result == 0)
                    {
                        Console.WriteLine("It's a tie!");
                    }
                    else if (result == 1)
                    {
                        Console.WriteLine("You win!");
                    }
                    else
                    {
                        Console.WriteLine("Computer wins!");
                    }
                }

                playAgain = PlayAgain();
                Console.Clear();
            }
        }

        static int GetPlayerChoice()
        {
            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice >= 1 && choice <= 3)
                {
                    return choice;
                }
            }

            Console.WriteLine("Invalid choice. Please try again.");
            return -1;
        }

        static int GetComputerChoice()
        {
            Random random = new Random();
            return random.Next(1, 4);
        }

        static string GetChoiceName(int choice)
        {
            switch (choice)
            {
                case 1:
                    return "Rock";
                case 2:
                    return "Paper";
                case 3:
                    return "Scissors";
                default:
                    return string.Empty;
            }
        }

        static int DetermineWinner(int playerChoice, int computerChoice)
        {
            // 0 - Tie, 1 - Player wins, 2 - Computer wins
            if (playerChoice == computerChoice)
            {
                return 0;
            }
            else if ((playerChoice == 1 && computerChoice == 3) ||
                     (playerChoice == 2 && computerChoice == 1) ||
                     (playerChoice == 3 && computerChoice == 2))
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        static bool PlayAgain()
        {
            Console.Write("Do you want to play again? (y/n): ");
            string choice = Console.ReadLine();
            return choice.Equals("y", StringComparison.OrdinalIgnoreCase);
        }

        static void GuessTheNumberGame()
        {
            bool playAgain = true;

            while (playAgain)
            {
                Console.Clear();
                Console.WriteLine("=== Guess the Number ==="); Random random = new Random();
                int randomNumber = random.Next(1, 101);

                Console.WriteLine("Guess the number between 1 and 100.");

                int attempts = 0;
                while (true)
                {
                    Console.Write("Enter your guess: ");
                    int guess = Convert.ToInt32(Console.ReadLine());

                    attempts++;

                    if (guess == randomNumber)
                    {
                        Console.WriteLine($"Congratulations! You guessed the number in {attempts} attempts.");
                        playAgain = PlayAgain();
                        Console.Clear();
                        break;
                    }
                    else if (guess < randomNumber)
                    {
                        Console.WriteLine("Too low. Try again.");
                    }
                    else
                    {
                        Console.WriteLine("Too high. Try again.");
                    }
                }
                Console.WriteLine("This is the Guess the Number game.");


                static bool PlayAgain()
                {
                    Console.Write("Do you want to play again? (y/n): ");
                    string choice = Console.ReadLine();
                    return choice.Equals("y", StringComparison.OrdinalIgnoreCase);
                }

            }
        }
    }
}