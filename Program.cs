using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        static bool passed;
        static bool passedLetters;
        static int loop = 0;
        static void Main(string[] args)
        {
            Gameplay x = new Gameplay();
            Gamer z = new Gamer();

            x.Count();
            x.Rand(x);
            x.LoadTheCities();
            do
            {
                Console.Clear();
                z.Lives = 5;
                x.Rand(x);
                x.ChooseCity();
                x.ChooseCapital();
                x.Encode();

                do
                {
                    if(z.Lives<=2)
                    {
                        Console.WriteLine("----\nThe capital belongs to {0}\n----", x.ChooseCapital());
                    }
                    x.Dispaly();
                    z.ShowLivesAndName(z);
                    x.ShowCity();
                    x.Choose(z);                    
                    if (z.Decision == "letter")
                    {
                        x.CheckALetter(z, loop);
                        loop++;
                        Program.passedLetters = x.CheckAllLetters();
                    }
                    else if (z.Decision == "sentence")
                    {
                        Program.passed = x.CheckASentence(z);
                    }
                    if (Program.passed == true) break;
                    else if (x.CheckingLetters() == false)
                    {
                        Console.WriteLine("You have guessed all letters!");                        
                        if (Program.passedLetters == true) break;
                    }
                    else continue;
                    
                } while (z.Lives > 0);
                if (z.Lives <= 0)
                {
                    Console.WriteLine("You've lost all of your lives");
                }
                z.Exit();
            } while (z.Decision.ToLower() != "yes");
        }
    }
}
