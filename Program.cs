using my_first_c_app;
using System.Text.Json;

internal class Program
{
 
    private static void Main()
    {
        GameData gameData = new();

        gameData = StartGame(gameData);

        gameData.displayValue = CreateDefaultDisplay(gameData.wordToGuess.Length);

        while(gameData.EndGameCondition())
         {

            HandleGuess(gameData);
            handleDisplay(gameData);
        }

        Console.WriteLine("Game Ended");
    }

    static GameData StartGame(GameData gameData)
    {
        Console.WriteLine("Welcome to Hangman! \nChoose your theme: \n");
        foreach (string key in gameData.themes.Keys)
        {
            Console.WriteLine(key);
        }


        string selectedTheme = Console.ReadLine();

        Console.WriteLine(" \nYou choose " + selectedTheme + "\n");

        if (gameData.themes.ContainsKey(selectedTheme))
        {

            gameData.selectedTheme = selectedTheme;
            string opts = File.ReadAllText(gameData.themes[selectedTheme]);
            try
            {
                gameData = JsonSerializer.Deserialize<GameData>(opts);
                if (gameData != null && gameData.wordOpts != null)
                {

                    int randInt = gameData.GenerateRandomNumber(gameData.wordOpts.Count);
                    gameData.wordToGuess = gameData.wordOpts[randInt].ToUpper();
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while reading the file: " + e.Message);
            }
        }
        else
        {
            Console.WriteLine("This theme is not valid. Please Select a valid theme \n");
            StartGame(gameData);
        }

        return gameData;
    }

    static string CreateDefaultDisplay(int lengthOfWord)
    {
        string defaultDisplay = "";
        for (int i = 0; i < lengthOfWord; i++)
        {
            defaultDisplay += "_";
        }
        return defaultDisplay;
    }

    static void handleDisplay(GameData gameData)
    {

        char[] updatedDisplay = gameData.displayValue.ToCharArray();
        for (int i = 0; i < gameData.wordToGuess.Length; i++)
        {
            char currentLetter = gameData.wordToGuess[i];
            if (currentLetter == gameData.userGuess)
            {
                updatedDisplay[i] = gameData.userGuess;
            }
        }

        gameData.displayValue = new string(updatedDisplay);
    }

    static void HandleGuess(GameData gameData)
    {
        Console.WriteLine(gameData.displayValue + "\n");
        Console.WriteLine("Guess a letter \n");
        string current = Console.ReadLine().ToUpper();

        Console.WriteLine("you choose " + current );

        if (current == "QUIT")
        {
            Console.WriteLine("Sorry to see you go!");
        }

        if (current.Length > 1)
        {
            Console.WriteLine("Please enter only one letter ", gameData.alreadyGuessed);
            HandleGuess(gameData);
        }

        bool alreadyUsed = gameData.alreadyGuessed.Contains(current);
        bool correctGuess = gameData.wordToGuess.Contains(current);

        if (alreadyUsed)
        {
            Console.WriteLine("You have already guessed " + current);
            HandleGuess(gameData);
        }

        if (correctGuess)
        {
            Console.WriteLine("Correct!");
            gameData.userGuess = current.ToCharArray()[0];
            gameData.alreadyGuessed = current;
            return;
        }
        Console.WriteLine("Incorrect, try again.");
        gameData.alreadyGuessed += current;
        gameData.incorrectGuessCount++;
        HandleGuess(gameData);

    }
}