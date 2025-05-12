namespace PrisonersChess___Caleb_Cattron
{
    internal class Program
    {
        static void Menu()
        {
            ConsoleKey userInput = ConsoleKey.None;
            string[] options = new string[4];
            options[0] = "Solve!";
            options[1] = "Play!";
            options[2] = "Help!";
            options[3] = "Exit!";
            int hoveredOption = 0;

            while (true)
            {
                Console.WriteLine("Welcome to Prisoners Chess! Use arrowkeys and enter to control!");
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == hoveredOption)
                    {
                        Console.WriteLine("> " + options[i]);
                    }
                    else
                    {
                        Console.WriteLine(options[i]);
                    }
                }

                userInput = Console.ReadKey().Key;
                if (userInput == ConsoleKey.UpArrow && hoveredOption > 0)
                {
                    hoveredOption--;
                }
                else if (userInput == ConsoleKey.DownArrow && hoveredOption < 3)
                {
                    hoveredOption++;
                }
                else if (userInput == ConsoleKey.Enter)
                {
                    if (hoveredOption == 0)
                    {
                        Solve();
                    }
                    else if (hoveredOption == 1)
                    {
                        Play();
                    }
                    else if (hoveredOption == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Prisoners Chess is a puzzle that challenges two people to correctly communicate the location of a hidden key on a chess board.");
                        Console.WriteLine("The chess board has a coin on each tile randomly flipped heads or tails, and a secret compartment for the key under each tile.");
                        Console.WriteLine("Player one is allowed to see the starting board, where the key is hidden, and flip a single coin to communicate to player two.");
                        Console.WriteLine("Player two is allowed to see the board after player one flips a coin of their choice, and must find the key!");
                        Console.WriteLine("Players are allowed to communicate a strategy before either person sees the board.");
                        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("Credits to 'The impossible chessboard puzzle' by 3Blue1Brown or 'The almost impossible chessboard puzzle' by Stand-up Maths!");
                        Console.WriteLine("Press any key to go back to menu!");
                        Console.ReadKey();
                    }
                    else
                    {
                        break;  //Exit
                    }

                    //Enter hovered menu!
                }
                
                Console.Clear();

            }
        }

        static void Solve()
        {
            bool[,] currentBoard = CreateBoard();
            int[] keyLocation = hideKey(currentBoard); //x and y

            //Boards have now been generated and key is hidden!

            int[] boardKeyAndCoinValues = CalculateCoinToFlip(currentBoard, keyLocation);
            //The correct coin to flip has been calculated

            string[] boardDisplay = GenerateDisplayBoard(currentBoard);
            int[] coinPosition = new int[2];    //x and y


            //Yes I know this is ugly... but I'm tired D:
            if (boardKeyAndCoinValues[2] < 56)
            {
                if (boardKeyAndCoinValues[2] < 48)
                {
                    if (boardKeyAndCoinValues[2] < 40)
                    {
                        if (boardKeyAndCoinValues[2] < 32)
                        {
                            if (boardKeyAndCoinValues[2] < 24)
                            {
                                if (boardKeyAndCoinValues[2] < 16)
                                {
                                    if (boardKeyAndCoinValues[2] < 8)
                                    {
                                        coinPosition[1] = 0;
                                        coinPosition[0] = boardKeyAndCoinValues[2];
                                    }
                                    else
                                    {
                                        coinPosition[1] = 1;
                                        coinPosition[0] = boardKeyAndCoinValues[2] - 8;
                                    }
                                }
                                else
                                {
                                    coinPosition[1] = 2;
                                    coinPosition[0] = boardKeyAndCoinValues[2] - 16;
                                }
                            }
                            else
                            {
                                coinPosition[1] = 3;
                                coinPosition[0] = boardKeyAndCoinValues[2] - 24;
                            }
                        }
                        else
                        {
                            coinPosition[1] = 4;
                            coinPosition[0] = boardKeyAndCoinValues[2] - 32;
                        }
                    }
                    else
                    {
                        coinPosition[1] = 5;
                        coinPosition[0] = boardKeyAndCoinValues[2] - 40;
                    }
                }
                else
                {
                    coinPosition[1] = 6;
                    coinPosition[0] = boardKeyAndCoinValues[2] - 48;
                }
            }
            else
            {
                coinPosition[1] = 7;
                coinPosition[0] = boardKeyAndCoinValues[2] - 56;
            }
            //Big Block Of Code


            Console.Clear();
            Console.WriteLine("The boards current state is " + boardKeyAndCoinValues[0]);
            Console.WriteLine("The key was hidden under " + boardKeyAndCoinValues[1]);
            Console.WriteLine("The correct coin to flip is " + boardKeyAndCoinValues[2]);
            Console.WriteLine("The absolute value of |board - key| = coin!");
            Console.WriteLine("Flip This Coin!");
            Console.WriteLine("\n");

            for (int y = 0; y < 8; y++)
            {
                if (coinPosition[1] == y)
                {
                    Console.WriteLine(boardDisplay[y] + "<");
                }
                else
                {
                    Console.WriteLine(boardDisplay[y]);
                }
            }
            string bottomSelector = string.Empty;
            for (int x = 0; x < coinPosition[0]; x++)
            {
                bottomSelector += "  ";
            }
            Console.WriteLine(bottomSelector + "^");

            Console.WriteLine("\n");
            Console.WriteLine("Press enter to go back to menu!");

            while (true)
            {
                ConsoleKey userInput = Console.ReadKey().Key;
                if (userInput == ConsoleKey.Enter)
                {
                    break;
                }
            }
        }
        static void Play()
        {
            bool[,] currentBoard = CreateBoard();
            int[] keyLocation = hideKey(currentBoard);
            int[] boardKeyAndCoinValues = CalculateCoinToFlip(currentBoard, keyLocation);
            
            string[] boardDisplay = GenerateDisplayBoard(currentBoard);
            bool[,] alteredBoard = currentBoard;
            bool player1NotDone = true;
            int[] player1Position = new int[2];
            player1Position[0] = 0;
            player1Position[1] = 0;

            while (player1NotDone)
            {
                Console.WriteLine("Select what coin you want to flip Player 1!");

                for (int y = 0; y < 8; y++)
                {
                    if (player1Position[1] == y)
                    {
                        Console.WriteLine(boardDisplay[y] + "<");
                    }
                    else
                    {
                        Console.WriteLine(boardDisplay[y]);
                    }
                }
                string bottomSelector = string.Empty;
                for (int x = 0; x < player1Position[0]; x++)
                {
                    bottomSelector += "  ";
                }
                Console.WriteLine(bottomSelector + "^");

                ConsoleKey userInput = Console.ReadKey().Key;
                if (userInput == ConsoleKey.UpArrow && player1Position[1] > 0)
                {
                    player1Position[1]--;
                }
                else if (userInput == ConsoleKey.DownArrow && player1Position[1] < 7)
                {
                    player1Position[1]++;
                }
                else if (userInput == ConsoleKey.LeftArrow &&   player1Position[0] > 0)
                {
                    player1Position[0]--;
                }
                else if (userInput == ConsoleKey.RightArrow && player1Position[0] < 7)
                {
                    player1Position[0]++;
                }
                else if (userInput == ConsoleKey.Enter)
                {
                    player1NotDone = false;
                    alteredBoard[player1Position[0], player1Position[1]] = !alteredBoard[player1Position[0], player1Position[1]];
                }
                Console.Clear();
            }


            //Have player guess, then compare to boardKeyAndCoinValues[2]

            string[] alteredBoardDisplay = GenerateDisplayBoard(alteredBoard);
            bool player2NotDone = true;
            int[] player2Position = new int[2];
            player2Position[0] = 0;
            player2Position[1] = 0;

            while (player2NotDone)
            {
                Console.WriteLine("Select where you think the key is Player two!");

                for (int y = 0; y < 8; y++)
                {
                    if (player2Position[1] == y)
                    {
                        Console.WriteLine(alteredBoardDisplay[y] + "<");
                    }
                    else
                    {
                        Console.WriteLine(alteredBoardDisplay[y]);
                    }
                }
                string bottomSelector = string.Empty;
                for (int x = 0; x < player2Position[0]; x++)
                {
                    bottomSelector += "  ";
                }
                Console.WriteLine(bottomSelector + "^");

                ConsoleKey userInput = Console.ReadKey().Key;
                if (userInput == ConsoleKey.UpArrow && player2Position[1] > 0)
                {
                    player2Position[1]--;
                }
                else if (userInput == ConsoleKey.DownArrow && player2Position[1] < 7)
                {
                    player2Position[1]++;
                }
                else if (userInput == ConsoleKey.LeftArrow && player2Position[0] > 0)
                {
                    player2Position[0]--;
                }
                else if (userInput == ConsoleKey.RightArrow && player2Position[0] < 7)
                {
                    player2Position[0]++;
                }
                else if (userInput == ConsoleKey.Enter)
                {
                    player2NotDone = false;
                }
                Console.Clear();
            }

            int player1Total = 0;
            player1Total = player1Position[1] * 8;
            player1Total += player1Position[0];

            int player2Total = 0;
            player2Total = player2Position[1] * 8;
            player2Total += player2Position[0];

            if (player1Total == boardKeyAndCoinValues[2] && player2Total == boardKeyAndCoinValues[1])
            {
                Console.WriteLine("YOU TWO DID IT!");
            }
            else if (player1Total == boardKeyAndCoinValues[2])
            {
                Console.WriteLine("Player 1 was correct! Player 2 might have to brush up on the rules");
                Console.WriteLine("Player 1 guess: " + player1Total + " Player 2 guess:" + player2Total);
                Console.WriteLine("Coin to flip: " + boardKeyAndCoinValues[2] + " Key location: " + boardKeyAndCoinValues[1]);
            }
            else if (player2Total == boardKeyAndCoinValues[1])
            {
                Console.WriteLine("Player 2 is correct? They somehow found the key when Player 1 was incorrect!");
                Console.WriteLine("Player 1 guess: " + player1Total + " Player 2 guess:" + player2Total);
                Console.WriteLine("Coin to flip: " + boardKeyAndCoinValues[2] + " Key location: " + boardKeyAndCoinValues[1]);
            }
            else
            {
                Console.WriteLine("Both players are wrong, try again after brushing up on the rules!");
                Console.WriteLine("Player 1 guess: " + player1Total + " Player 2 guess:" + player2Total);
                Console.WriteLine("Coin to flip: " + boardKeyAndCoinValues[2] + " Key location: " + boardKeyAndCoinValues[1]);
            }

            Console.WriteLine("Press enter to go back to menu!");
            while (true)
            {
                ConsoleKey userInput = Console.ReadKey().Key;

                if (userInput == ConsoleKey.Enter)
                {
                    break;
                }
            }
        }

        static bool[,] CreateBoard()
        {
            Random random = new Random();
            bool[,] currentBoard = new bool[8, 8];
            string[] boardDisplay = new string[8];
            bool correctBoard = false;

            while (!correctBoard)
            {
                Console.WriteLine("Would you like to solve a random board? (y / n)");
                ConsoleKey userInput = Console.ReadKey().Key;

                if (userInput == ConsoleKey.Y)
                {
                    Console.WriteLine("\n");
                    //Randomizes Board
                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            if (random.Next() % 2 == 0)
                            {
                                currentBoard[x, y] = false;
                            }
                            else
                            {
                                currentBoard[x, y] = true;
                            }
                        }
                    }
                    //Randomizes Board
                    break;
                }
                else if (userInput == ConsoleKey.N)
                {
                    Console.WriteLine("\n");
                    //Sets entire board to Tails
                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            currentBoard[x, y] = false;
                        }
                    }
                    //Sets entire board to Tails

                    int[] position = new int[2];    //x and y position
                    while (!correctBoard)
                    {
                        Console.WriteLine("Press space to swap between heads (1) and tails (0)! Press enter when finished!");
                        Console.WriteLine("\n");

                        //Generates and shows current position pointed at
                        boardDisplay = GenerateDisplayBoard(currentBoard);

                        for (int y = 0; y < 8; y++)
                        {
                            if (position[1] == y)
                            {
                                Console.WriteLine(boardDisplay[y] + "<");
                            }
                            else
                            {
                                Console.WriteLine(boardDisplay[y]);
                            }
                        }
                        string bottomSelector = string.Empty;
                        for (int x = 0; x < position[0]; x++)
                        {
                            bottomSelector += "  ";
                        }
                        Console.WriteLine(bottomSelector + "^");
                        //Generates and shows current position pointed at

                        userInput = Console.ReadKey().Key;
                        if (userInput == ConsoleKey.UpArrow && position[1] > 0)
                        {
                            position[1]--;
                        }
                        else if (userInput == ConsoleKey.DownArrow && position[1] < 7)
                        {
                            position[1]++;
                        }
                        else if (userInput == ConsoleKey.LeftArrow && position[0] > 0)
                        {
                            position[0]--;
                        }
                        else if (userInput == ConsoleKey.RightArrow && position[0] < 7)
                        {
                            position[0]++;
                        }
                        else if (userInput == ConsoleKey.Spacebar)
                        {
                            currentBoard[position[0], position[1]] = !currentBoard[position[0], position[1]];
                        }
                        else if (userInput == ConsoleKey.Enter)
                        {
                            while (true)
                            {
                                Console.WriteLine("Is this the board you want to solve? (y / n)");
                                userInput = Console.ReadKey().Key;
                                if (userInput == ConsoleKey.Y)
                                {
                                    correctBoard = true;
                                    break;
                                }
                                else if (userInput == ConsoleKey.N)
                                {
                                    break;
                                }
                            }
                        }
                        Console.Clear();
                    }
                }
            }

            return currentBoard;
        }

        static int[] hideKey(bool[,] board)
        {
            int[] position = new int[2];    //x and y position
            string[] boardDisplay = new string[8];

            while (true)
            {
                Console.WriteLine("Press enter to hide the key!");
                Console.WriteLine("\n");

                //Generates and shows current position pointed at
                boardDisplay = GenerateDisplayBoard(board);

                for (int y = 0; y < 8; y++)
                {
                    if (position[1] == y)
                    {
                        Console.WriteLine(boardDisplay[y] + "<");
                    }
                    else
                    {
                        Console.WriteLine(boardDisplay[y]);
                    }
                }
                string bottomSelector = string.Empty;
                for (int x = 0; x < position[0]; x++)
                {
                    bottomSelector += "  ";
                }
                Console.WriteLine(bottomSelector + "^");
                //Generates and shows current position pointed at

                ConsoleKey userInput = Console.ReadKey().Key;
                if (userInput == ConsoleKey.UpArrow && position[1] > 0)
                {
                    position[1]--;
                }
                else if (userInput == ConsoleKey.DownArrow && position[1] < 7)
                {
                    position[1]++;
                }
                else if (userInput == ConsoleKey.LeftArrow && position[0] > 0)
                {
                    position[0]--;
                }
                else if (userInput == ConsoleKey.RightArrow && position[0] < 7)
                {
                    position[0]++;
                }
                else if (userInput == ConsoleKey.Enter)
                {
                    bool correctKeyLocation = false;
                    while (true)
                    {
                        Console.WriteLine("Is this where you want to hide the key? (y / n)");
                        userInput = Console.ReadKey().Key;
                        if (userInput == ConsoleKey.Y)
                        {
                            correctKeyLocation = true;
                            Console.WriteLine("\n");
                            break;
                        }
                        else if (userInput == ConsoleKey.N)
                        {
                            Console.WriteLine("\n");
                            break;
                        }
                    }

                    if (correctKeyLocation)
                    {
                        break;
                    }
                }

                Console.Clear();
            }

            return position;
        }

        static string[] GenerateDisplayBoard(bool[,] board)
        {
            string[] boardDisplay = new string[8];

            for (int y = 0; y < 8; y++)
            {
                string boardDisplayLine = string.Empty;
                for (int x = 0; x < 8; x++)
                {
                    if (board[x, y] == true)
                    {
                        boardDisplayLine = boardDisplayLine + "1 ";
                    }
                    else
                    {
                        boardDisplayLine = boardDisplayLine + "0 ";
                    }
                }
                boardDisplay[y] = boardDisplayLine;
            }
            return boardDisplay;
        }

        static int[] CalculateCoinToFlip(bool[,] board, int[] key)
        {
            int[] boardKeyCoinValues = new int[3];
            int coinToFlip = 0;
            int keyLocation = 0;
            int currentBoardNumber = 0;
            bool isOdd = false;


            keyLocation = key[1] * 8;
            keyLocation += key[0];

            //First Encryption
            for (int y = 0; y < 8; y++)
            {
                for (int x = 1; x < 8; x += 2)
                {
                    if (board[x, y] == true)
                    {
                        isOdd = !isOdd;
                    }
                }
            }
            if (isOdd)
            {
                currentBoardNumber += 1;
            }
            //First Encryption

            isOdd = false;
            //Second Encryption
            for (int y = 0; y < 8; y++)
            {
                if (board[2, y] == true)
                {
                    isOdd = !isOdd;
                }
                if (board[3, y] == true)
                {
                    isOdd = !isOdd;
                }
                if (board[6, y] == true)
                {
                    isOdd = !isOdd;
                }
                if (board[7, y] == true)
                {
                    isOdd = !isOdd;
                }
            }
            if (isOdd)
            {
                currentBoardNumber += 2;
            }
            //Second Encryption

            isOdd = false;
            //Third Encryption
            for (int y = 0; y < 8; y++)
            {
                for (int x = 4; x < 8; x++)
                {
                    if (board[x, y] == true)
                    {
                        isOdd = !isOdd;
                    }
                }
            }
            if (isOdd)
            {
                currentBoardNumber += 4;
            }
            //Third Encryption

            isOdd = false;
            //Fourth Encryption
            for (int y = 1; y < 8; y += 2)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[x, y] == true)
                    {
                        isOdd = !isOdd;
                    }
                }
            }
            if (isOdd)
            {
                currentBoardNumber += 8;
            }
            //Fourth Encryption

            isOdd = false;
            //Fifth Encryption
            for (int x = 0; x < 8; x++)
            {
                if (board[x, 2] == true)
                {
                    isOdd = !isOdd;
                }
                if (board[x, 3] == true)
                {
                    isOdd = !isOdd;
                }
                if (board[x, 6] == true)
                {
                    isOdd = !isOdd;
                }
                if (board[x, 7] == true)
                {
                    isOdd = !isOdd;
                }
            }
            if (isOdd)
            {
                currentBoardNumber += 16;
            }
            //Fifth Encryption

            isOdd = false;
            //Sixth Encryption
            for (int y = 4; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[x, y] == true)
                    {
                        isOdd = !isOdd;
                    }
                }
            }
            if (isOdd)
            {
                currentBoardNumber += 32;
            }
            //Sixth Encryption

            coinToFlip = Math.Abs(currentBoardNumber - keyLocation);

            boardKeyCoinValues[0] = currentBoardNumber;
            boardKeyCoinValues[1] = keyLocation;
            boardKeyCoinValues[2] = coinToFlip;

            return boardKeyCoinValues;
        }

        static void Main(string[] args)
        {
            Menu();
        }
    }
}
