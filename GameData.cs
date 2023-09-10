using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace my_first_c_app
{
    internal class GameData
    {
        public string selectedTheme, wordToGuess, displayValue, alreadyGuessed;
        public int guessCount, incorrectGuessCount, incorrectGuessLimit;
        public IDictionary<string, string> themes = new Dictionary<string, string>();
        public char userGuess;
        [JsonPropertyName("data")]
        public List<string> wordOpts { get; set; }


        public GameData()
        {
            themes["presidents"] = "C:\\Users\\RakeemGordon\\repos\\my_first_c_app\\data\\presidents.json";
            themes["anime"] = "C:\\Users\\RakeemGordon\\repos\\my_first_c_app\\data\\anime.json";
            themes["cars"] = "C:\\Users\\RakeemGordon\\repos\\my_first_c_app\\data\\cars.json";
            selectedTheme = "";
            wordToGuess = "";
            displayValue = "";
            alreadyGuessed = "";
            incorrectGuessLimit = 8;
            incorrectGuessCount = 0;
            wordOpts = [];
        }

        public Dictionary<string, string> GetThemes()
        {
            return (Dictionary<string, string>)themes;
        }

        public int GenerateRandomNumber(int maxValue)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, maxValue);
            return randomNumber;
        }

        public bool EndGameCondition()
        {

            if (incorrectGuessCount > incorrectGuessLimit)
            {
                Console.WriteLine("You Lose! Too many incorrect attempts =[");
                return false;
            }
            if (!displayValue.Contains("_") && displayValue != "")
            {
                Console.WriteLine("Congrats on correctly guessing " + wordToGuess + "!");
                return false;
            }
            return true;
        }
    }
}
