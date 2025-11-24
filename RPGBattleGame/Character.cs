using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGBattleGame
{
    internal abstract class Character
    {
        private string _Name = "";
        private int _Health;
        private int _MaxHealth;

        private string _Gender;
        private string _pronoun;
        private string _personalPronoun;


        private string point;
        private string _arms;




        public string Arms
        {
            get { return _arms; }
            set { _arms = value; }
        }

        public string Name
        {
            get { return _Name[0].ToString().ToUpper() + _Name.Substring(1).ToLower(); }
            set { _Name = value; }
        }

        public int Health
        {
            get { return _Health; }
            set { _Health = value; }
        }

        public int MaxHealth
        {
            get { return _MaxHealth; }
            set { _MaxHealth = value; }
        }


        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }


        public string Pronoun
        {
            get { return _pronoun; }
            set { _pronoun = value; }
        }

        public string PersonalPronoun
        {
            get { return _personalPronoun; }
            set { _personalPronoun = value; }
        }



     


        public Character()
        {
           
        }

        public string HeroInfo()
        {
            return $"Name: {Name}  Class: {this.GetType().Name}  Arms: {Arms}  MaxHealth: {MaxHealth}  .\n";
        }
        
        public string DemonstrateHeroAbilities()
        {
            return $"{Pronoun} will use {PersonalPronoun} {Arms} to attack the enemy.\n" ;
        }

        public abstract void Attack(Character target);


        public abstract void SpecialAttack(Character target);


        public void TakeDamage(int amount)
        {
            Health = Health - amount;
            if (Health < 0) Health = 0;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write($"{Name} ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($"took ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{amount} ");
            Console.ForegroundColor = ConsoleColor.Black;
            point = amount > 1 ? "points" : "point";
            Console.WriteLine($"damage {point} and {PersonalPronoun} remaining health is {Health}/{MaxHealth}.");
            Console.ResetColor();

        }

        public void AssignPronouns()
        {
            bool checkIFMale = Gender.Equals("male");
            Pronoun = checkIFMale ? "He" : "She";
            PersonalPronoun = checkIFMale ? "his" : "her";
        }

        public bool IsAlive()
        {
            return Health > 0;
        }
    }
}
