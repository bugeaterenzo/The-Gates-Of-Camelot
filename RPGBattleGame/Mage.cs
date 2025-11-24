using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGBattleGame
{
    internal class Mage : Character
    {
        private Random random;
        
        public Mage()
        {
            Health = MaxHealth;
        }

        


        public override string ToString()
        {

            return $"{ HeroInfo() }" +
                   $"{Name} is an amazing mage with the most powerfull magic in the vale of the heroes.\n" +
                   $"{ DemonstrateHeroAbilities() }" +
                   $"Therefore {Name.ToLower()} is the ideal hero for both long-range and close combat.\n";
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

            target.TakeDamage( random.Next(10, 40) );

            

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
            Console.WriteLine($"with dragon fire {Arms}.");
            Console.ResetColor();

            target.TakeDamage(random.Next(30, 60));


        }






    }
}
