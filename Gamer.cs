using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class Gamer
    {
        private int lives;
        private string name;
        private string letter;
        private string sentence;
        private string quitDecision;

        public Gamer()
        {
            Console.WriteLine("Please take your name ...");
            this.name = Console.ReadLine();
            this.lives = 5;
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public int Lives
        {
            get
            {
                return this.lives;
            }
            set
            {
                this.lives = value;
            }
        }
        public string ShowLetter
        {
            get
            {
                return this.letter;
            }
            set
            {
                letter = value;
            }
        }
        public string Decision
        {
            get
            {
                return this.quitDecision;
            }
            set
            {
                this.quitDecision = value;
            }
        } 
        public void TakeALetter()
        {
            
            Console.WriteLine("Please take a letter ... ");
            do
            {
                this.letter = Console.ReadLine();
                if (this.letter.Length > 1)
                {
                    Console.WriteLine("You should write ONE letter ... ");

                }
            } while (this.letter.Length > 1);
            
           
        }
        public string TakeASentence()
        {
            Console.WriteLine("Please take a sentence ... ");
            this.sentence = Console.ReadLine();
            return this.sentence;
        }
        public void Exit()
        {
            Console.WriteLine("Would you like to quit ?");
            this.quitDecision = Console.ReadLine();
            this.quitDecision.ToLower();
        }
        public void ShowLivesAndName(Gamer gamer)
        {
            Console.WriteLine("Player names : {0}\tLives : {1}",  gamer.Name, gamer.Lives);
             
        }
    }
}
