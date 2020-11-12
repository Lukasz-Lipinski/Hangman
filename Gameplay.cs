using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class Gameplay
    {
        private FileStream fs;
        private StreamReader sr;
        private string[] NotInWord; 
        private string[] cities;
        private string[] EnCity;
        private string city;
        private int count = 0;
        private int number;
        private bool passed;
        bool exsist;
        bool TheSame;

        //Operacje na strumieniu 
        public void OpenFile()
        {            
            try
            {
                fs = new FileStream("C:\\Users\\Lipek\\source\\repos\\Hangman\\pliki\\countries_and_capitals.txt.txt", FileMode.Open);
            }
            catch (Exception e)
            {
                Console.WriteLine("The file's path is incorrected ... \n" + e);
            }

            try
            {
                sr = new StreamReader(fs);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void CloseFile()
        {
            fs.Close();
        }

        public void LoadTheCities() //#3
        {
            OpenFile();
            string lines;
            int i = 0;
            cities = new string[this.count];
            while ((lines = sr.ReadLine()) != null)
            {
                cities[i] = lines;
                i++;
            }
            CloseFile();            
        }
        public string ChooseCity() //#4
        {            
            string[] CityAndCapital = cities[this.number].Split(new char[] {'|'});
            for ( int i = 0; i < CityAndCapital.Length; i++)
            {
                CityAndCapital[i] = CityAndCapital[i].Trim();
            }
            return CityAndCapital[1];
        }

        public string ChooseCapital() //#5
        {
            string[] CityAndCapital = cities[this.number].Split(new char[] { '|' });
            for (int i = 0; i < CityAndCapital.Length; i++)
            {
                CityAndCapital[i] = CityAndCapital[i].Trim();
            }
            return CityAndCapital[0];
        }


        public void Encode() // #6
        {
            this.city = ChooseCity();
            EnCity = new string [city.Length];
            NotInWord = new string[city.Length + 5];
            for (int i = 0; i < city.Length; i++)
            {
                EnCity[i] = " _ ";
            }
        }
        public void ShowCity()
        {            
            for ( int i = 0; i <= this.EnCity.Length; i++)
            {
                
                if (i == this.EnCity.Length) Console.Write("\n");
                else Console.Write(EnCity[i]);
            }
        }
        public void CheckALetter(Gamer gamer, int loop)
        {
            int Letters = 0;
            gamer.TakeALetter();
            if (loop == 0)
            {
                NotInWord[loop] = gamer.ShowLetter;
            }
            else
            {
                while (CheckedUsedLetters(gamer) == true)
                {
                    gamer.TakeALetter();
                }
                NotInWord[loop] = gamer.ShowLetter;
            }
            int AmountLetters = EnCity.Length;
            for (int i = 0; i < AmountLetters; i++)
            {
                if(gamer.ShowLetter[0] == city[i]) // Sprawdzanie czy wprowadzona litera jest taka sama jak w nazwie miasta
                {
                    this.EnCity[i] = gamer.ShowLetter;                    
                    Letters++;               
                }
            }
            if (Letters == 0)
            {
                gamer.Lives--;
            }
        }
        public bool CheckASentence(Gamer gamer)
        {
            string sentence = gamer.TakeASentence();            
            for(int i = 0; i < sentence.Length; i++)
            {
                if(sentence[i] != city[i])
                {
                    Console.WriteLine("Wrong city's name");
                    gamer.Lives -= 2;
                    this.passed = false;
                    break;
                }
                else if (i == (sentence.Length-1))
                {
                    Console.WriteLine("Congratulation! You have guessed !");
                    this.passed = true;
                }
            }
            return this.passed;
        }
        public void Dispaly()
        {
            Console.Write("These letters were used: ");
            foreach(string i in NotInWord)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine("");
        }
        public bool CheckedUsedLetters(Gamer gamer)
        {            
            foreach(string i in NotInWord)
            {
                if (i == gamer.ShowLetter)
                {
                    Console.WriteLine("You used this letter ... ");
                    exsist = true;
                    break;
                }
                else exsist = false;
            }
            return exsist;
        }

        //zliczanie ilosci wierszy
        public int Count() //#1
        {
            OpenFile();
            string lines;
            while ((lines = sr.ReadLine()) != null)
            {
                this.count++;
            }
            CloseFile();
            return this.count;
        }        
        // zwracanie numeru wiersza
        public int Rand(Gameplay gameplay) //#2
        {   
            Random r = new Random();            
            this.number = r.Next(0, this.count);
            return this.number;
        }
        public void CheckTheLives(Gamer gamer)
        {
            if (gamer.Lives <= 0)
            {
                Console.WriteLine("you lost all of your lives");
                gamer.Exit();
            }
            else return;
        }
        public void Choose(Gamer gamer) // wybor zgadniecia litery lub calego zdania
        {
            Console.WriteLine("Would you like to guess a letter or a sentence?");
            gamer.Decision = Console.ReadLine().ToLower();
            while(true)
            {
                if (gamer.Decision == "letter") break;
                else if (gamer.Decision == "a letter")
                {
                    gamer.Decision = "letter";
                    break;
                }
                else if (gamer.Decision == "sentence") break;
                else if (gamer.Decision == "a sentence")
                {
                    gamer.Decision = "sentence";
                    break;
                }
                else
                {
                    Console.WriteLine("Taken word is no correct, please use letter or a letter");
                    gamer.Decision = Console.ReadLine().ToLower();
                }
            }
        }
        public bool CheckAllLetters()//ok
        {
            string znak;
            for (int i = 0; i < city.Length; i++)
            {                
                znak = Convert.ToString(city[i]);
                if (znak == EnCity[i])
                {
                    this.TheSame = true;
                    break;
                }
                else this.TheSame = false;
            }
            return this.TheSame;
        }
        public bool CheckingLetters()
        {
            for(int i = 0; i < EnCity.Length; i++)
            {
                if (EnCity[i] == " _ ")
                {
                    this.TheSame = true;
                    break;
                }
                else this.TheSame = false;
            }
            return this.TheSame;
        }
    }
}
