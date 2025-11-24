using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPGBattleGame
{
    internal class Warrior : Character
    {
        private Random random;
    

       

        public Warrior()
        {
           
        }

        public override string ToString()
        {        
            return $"{ HeroInfo() }" +
                   $"{Name} is a legendary warrior with the highest health in the vale of the heroes.\n" +
                   $"{ DemonstrateHeroAbilities() }" +
                   $"Therefore {Name} is the ideal hero for a close combat.\n";
        }

        public override void Attack(Character target)
        {
            random = new Random();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write($"{Name} ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($"is attacking ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{target.Name.ToLower()} ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"with {Arms.ToLower()}.");
            Console.ResetColor();

            target.TakeDamage(random.Next(10, 40));


        }


        public override void SpecialAttack(Character target)
        {
            random = new Random();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write($"{Name} ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($"is breaking the armour of ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{target.Name} ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"with dragon blade {Arms.ToLower()}.");
            Console.ResetColor();

            target.TakeDamage(random.Next(30, 60));


        }





    }
}
