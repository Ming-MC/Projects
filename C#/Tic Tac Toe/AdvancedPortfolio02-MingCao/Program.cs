/*
Purpose: Create a program for playing a tic-tac-toe game
         - Two players take turns marking an available cell in a 3 x 3 grid
         - Winner: one player has placed three tokens in a a horizontal, vertical, or diagonal row on the grid
         - No winner: all the cells on the grid have been filled with tokens and neither players has achieved 

Input: 1. User's enter: letter from 'A' to 'I'
       2. User's answer: 'y' or 'n'

Process(es): 1. To create game introduction and board
             2. To prompt the user to enter letter
             3. To search the input letter in 2D array
             4. To find out an elment with same value
             5. To set the value to the elemet in the same cell of blank array

output:  Games result: winner and draw

Author: Ming Cao

Last modified: April 16, 2020
*/

using System;

namespace AdvancedPortfolio02_MingCao
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declare variable
            bool exit;
            
            // do-while loop: the player will continue to play the game until the player want to exit the game
            do
            {
                //To clear the screen after each round
                Console.Clear();

                //Declare 2D array with an initializer: 9 letters from A to I
                char[,] arrayLetter = { { 'A', 'B', 'C' }, { 'D', 'E', 'F' }, { 'G', 'H', 'I' } };
                //Declare 2D array with an initializer:  9 blanks
                char[,] arrayBlank = { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
                
                //Declare variables
                char playerInput;
                char result;
                char player = 'O';
                bool gameOver = false;

                //Calling method: display game introduction and board
                DisplayIntroduction();
                //Calling method: display an opening board given values by array letter
                DisplayBoard(arrayLetter);

                //do-while loop: the game will not be end of round until the result proof game over
                do
                {
                    //Player has initialized as 'O'
                    //Calling method and give the value, so the player starts from X
                    player = SetPlayerRole(player);
                    //Calling method: prompt the player to enter letter and get the correct letters 
                    playerInput = CheckPlayerInput($"\nEnter cell ID (A-I) for player {player}: ");

                    //for loop: Check the player input in each element of letter array
                    //three rows
                    for (int row = 0; row < 3; row++)
                    {
                        //three columns
                        for (int column = 0; column < 3; column++)
                        {
                           //Check if the element in array letter is same as player input
                           //---each cell in the game board with letters equal to player input
                           //---A (board) = A (player input)
                           if (arrayLetter[row, column] == playerInput)
                            {
                                //Check each cell is blank
                                if (arrayBlank[row, column] == ' ')
                                {
                                    //The element in array blank equal to player
                                    arrayBlank[row, column] = player;
                                    //Calling method to display blank game board filled with player ID when they play the game
                                    DisplayBoard(arrayBlank);
                                } //End if

                                //When the place of the array blank has already been filled with player idc
                                //Ask the player to try another one
                                else if (arrayBlank[row, column] == 'X' || arrayBlank[row, column] == 'O')
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nThis cell has already token. Please try another one.");
                                    Console.ResetColor();
                                    playerInput = CheckPlayerInput($"\nEnter cell ID (A-I) for player {player}: ");
                                } //End else if
                            } //End if
                        } //End for 
                    } //End for

                    //Call method: to get the game result (win or draw)
                    result = CheckGameResult(arrayBlank, player);
                    if (result == player)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\n☆☆☆☆☆☆☆☆☆☆☆");
                        Console.Write("☆  ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"Player { result} wins!  ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("☆");
                        Console.WriteLine("☆☆☆☆☆☆☆☆☆☆☆");
                        Console.ResetColor();
                        gameOver = true;
                    } //End if
                    else if (result == 'D')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\n☆☆☆☆☆☆☆☆☆☆");
                        Console.Write("☆  ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("It is a draw.");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(" ☆");
                        Console.WriteLine("☆☆☆☆☆☆☆☆☆☆");
                        Console.ResetColor();
                        gameOver = true;
                    } //End else if
                } while (!gameOver); //End do-while

                //Call method: to get the answer from player if they want to continue or exit
                exit = GetPlayerAnswer("\nWould you like to play again (y/n)? ");

                if (exit)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nGood-bye and thanks for playing.");
                    Console.ResetColor();
                } //End if
            } while (!exit); //End do-while

            Console.ReadKey();
        } //End method

        /// <summary>
        /// Tie-Tac-Toe Game Introduction
        /// </summary>
        static void DisplayIntroduction()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("********************\n" +
                          "* Tic-Tac-Toe Game *\n" +
                          "********************\n");
            Console.ResetColor();
            
            Console.WriteLine("\nThe cell IDs for the game are shown below.");
        } //End method

        /// <summary>
        /// Game Board
        /// </summary>
        /// <param name="array"></param>
        static void DisplayBoard(char[,] array)
        {
            //Draw game board: set array in cells
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"\n-------------");
            Console.Write($"\n| {array[0, 0]} | {array[0, 1]} | {array[0, 2]} |");
            Console.Write($"\n-------------");
            Console.Write($"\n| {array[1, 0]} | {array[1, 1]} | {array[1, 2]} |");
            Console.Write($"\n-------------");
            Console.Write($"\n| {array[2, 0]} | {array[2, 1]} | {array[2, 2]} |");
            Console.Write($"\n-------------\n");
            Console.ResetColor();

        } //End method

        /// <summary>
        /// Set one player as "X", another player as "O"
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        static char SetPlayerRole(char player)
        {
            if (player == 'O')
            {
                player = 'X';
            } //End if
            else
            {
                player = 'O';
            } //End else

            return player;

        } //End method

        /// <summary>
        /// Check Two result:
        /// 1. win
        /// 2. draw
        /// </summary>
        /// <param name="array"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        static char CheckGameResult(char[,] array, char player)
        {
            //Declare variable
            char result = ' ';

            //Check win:
            //--- making sure every cell in row, column, diagonall which has the same player
            if (player == array[0, 0] && player == array[0, 1] && player == array[0, 2] || //First row
                player == array[1, 0] && player == array[1, 1] && player == array[1, 2] || //Second row
                player == array[2, 0] && player == array[2, 1] && player == array[2, 2] || //Third row
                player == array[0, 0] && player == array[1, 0] && player == array[2, 0] || //First column
                player == array[0, 1] && player == array[1, 1] && player == array[2, 1] || //Second column
                player == array[0, 2] && player == array[1, 2] && player == array[2, 2] || //Third column
                player == array[0, 0] && player == array[1, 1] && player == array[2, 2] || //First diagonal: from top left to bottom right
                player == array[0, 2] && player == array[1, 1] && player == array[2, 0])   //Second diagonal: from top right to bottom left
            {
                //The result here is that one of the player is the winner
                result = player;
            } //End if
            
            //Check draw:
            //---making sure all cells are token and not the same 
            else if (array[0, 0] != ' ' && array[0, 1] != ' ' && array[0, 2] != ' ' &&
                     array[1, 0] != ' ' && array[1, 1] != ' ' && array[1, 2] != ' ' &&
                     array[2, 0] != ' ' && array[2, 1] != ' ' && array[2, 2] != ' ')
            {
                //The result here is draw
                result = 'D';
            } //End else if

            return result;

        } //End method

        /// <summary>
        /// Check if player's input from A to I correctly
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        static char CheckPlayerInput(string prompt)
        {
            //Declare variables
            bool validInput = false;
            char playerInput = ' ';

            //do-while loop: check player's input until meets the requirement
            do
            {
                //Prompt player to input letter to play game
                Console.Write(prompt);

                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    playerInput = char.Parse(Console.ReadLine().ToUpper());
                    Console.ResetColor();

                    //Check player's input is from A to I
                    //Char.IsLetter is a method to void numbers
                    if (Char.IsLetter(playerInput) && playerInput <= 'I')
                    {
                        //This means input is correct
                        validInput = true;
                    } //End if
                    
                    //Player's input is not from A to I
                    else
                    {
                        //Error prompt: ask player to enter a correct letter
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid input. Please enter a letter from <A> to <I>." );
                        Console.ResetColor();
                    } //End else
                } //End try
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input. Please enter a letter from <A> to <I>.");
                    Console.ResetColor();
                } //End catch
            } while (!validInput); //End do-while

            return playerInput;

        } //End method

        /// <summary>
        /// Check if player's answer is y/n correctly
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        static bool GetPlayerAnswer(string prompt)
        {
            //Declare variables
            bool validInput = false;
            bool exit = false;
            char playerAnswer;

            //do-while loop: check player's answer until meets the requirement
            do
            {
                Console.Write(prompt);

                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    playerAnswer = char.Parse(Console.ReadLine().ToLower());
                    Console.ResetColor();
          
                    //Check players answer is y
                    if (Char.IsLetter(playerAnswer) && playerAnswer == 'y')
                    {
                        //This mean input is correct
                        validInput = true;
                        //y means to play game again, so the game will not exit
                        exit = false;
                    } //End if
                    //Check players answer is n
                    else if (Char.IsLetter(playerAnswer) && playerAnswer == 'n')
                    {
                        validInput = true;
                        //n means not to play game again, so the game will exit
                        exit = true;
                    } //End else if

                    //Show error prompt, when enter the input is not 'y' or 'n'
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid input. Please enter a letter from <y> to <n>.");
                        Console.ResetColor();
                    } //End else
                } //End try
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n" + ex.Message + "Invalid input. Please enter a letter from <y> to <n>.");
                    Console.ResetColor();
                } //End catch
            } while (!validInput); //End do-while

            return exit;

        } //End method

    } //End class
} //End namespace
