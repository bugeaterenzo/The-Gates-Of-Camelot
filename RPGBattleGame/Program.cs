using System.Diagnostics.Metrics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;

namespace RPGBattleGame;

internal class Program
{

    public static Random random;

    static int luckyPotionCount = 2;
    static int luckyPotionMaxCount = 2;

    static int powerAttackCount = 1;





    /// <summary>
    /// This List contains the default enemy characters which will be picked randomly by the computer.
    /// </summary>
    static List<Character> enemyCharacters = new List<Character>()
    {

        new Warrior
        {
            Name = "Arthur",
            Health = 280,
            MaxHealth = 280,
            Arms = "Sword",
            Gender = "male"
        },
        new Mage {
            Name = "Merlin",
            Health = 280,
            MaxHealth = 280,
            Arms = "Spill",
            Gender = "male"
        },
        new Archer
        {
            Name = "Morgana",
            Health = 280,
            MaxHealth = 280,
            Arms = "Bow",
            Gender = "female"
        }

    };




    static Character player;
    static Character computer;


    public static void Main(string[] args)
    {   


        WelcomeMessage();

        CreateUserCharacter();

        computer = createComputerPlayer();
      
        computer.AssignPronouns();

        Console.WriteLine($"\n____________________________________");
                                    Console.Write(player);
        Console.WriteLine($"____________________________________");
        Console.WriteLine($"\n____________________________________");
                                    Console.Write(computer);
        Console.WriteLine($"____________________________________\n");


        while ( player.IsAlive() && computer.IsAlive() )
        {
            Console.WriteLine($"\n____________________________________");
            Console.Write($"       Your Health: {player.Health}/{player.MaxHealth}");
            Console.WriteLine($"\n____________________________________");
            Console.Write($"       {computer.Name}´s Health: {computer.Health}/{computer.MaxHealth}");
            Console.WriteLine($"\n____________________________________");
            Console.Write($"       Potion Count: {luckyPotionCount}/{luckyPotionMaxCount}");
            Console.WriteLine($"\n____________________________________");



            if (player.IsAlive())
            {
                
                    if (GetPlayerAnswer("=> Enter 1 to Attack or 2 to use your Lucky potion:").Equals("attack"))
                    {
                        player.Attack(computer);
                        Console.WriteLine($"\n____________________________________");
                    }
                    else 
                    {
                        if (player.Health != player.MaxHealth && luckyPotionCount != 0)
                        {
                            Console.WriteLine($"Potion: {luckyPotionCount} / {luckyPotionMaxCount}");

                            IncreaseHealth();
                            player.Attack(computer);
                        }
                        else
                        {
                            Console.WriteLine("You Dont Have Any Potion Left.");
                            Console.WriteLine($"Potion: {luckyPotionCount} / {luckyPotionMaxCount}");

                            player.Attack(computer);
                            Console.WriteLine($"\n____________________________________");
                    
                        }

                    }


                if ( player.Health < 50 && powerAttackCount != 0 && GetPlayerAnswer("=> Do you want to use your special attack power?").Equals("attack"))
                {
                    player.SpecialAttack(computer);
                    powerAttackCount--;
                } 

                

            }



            if (computer.IsAlive())
            {
                Console.WriteLine($"");
                computer.Attack(player);
                Console.WriteLine($"\n____________________________________");
            }

            Console.WriteLine("Press A Key To continue.");
            Console.ReadKey();
            Console.Clear();
            

        }

        Console.WriteLine($"\n____________________________________");
        Console.Write($"       Your Health: {player.Health}/{player.MaxHealth}");
        Console.WriteLine($"\n____________________________________");
        Console.Write($"       {computer.Name}´s Health: {computer.Health}/{computer.MaxHealth}");
        Console.WriteLine($"\n____________________________________");

        Console.WriteLine($"\n____________________________________");
        if (player.IsAlive()) Console.Write($"       {player.Name.ToUpper()} WON THE BATTLE.");
        else Console.Write($"        {computer.Name.ToUpper()} WON THE BATTLE.");
        Console.WriteLine($"\n------------------------------------");


    }



     /// <summary>
     /// This Method displays the welcome message.
     /// </summary>
     static void WelcomeMessage()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n|________Welcome__To__The__Gates__Of__Camelot________|\n");
        Console.ResetColor();
    }



    /// <summary>
    /// This Method creates a user character and assigns his/her gender.
    /// </summary>
    static void CreateUserCharacter()
    {

        string name = GetPlayerName();
        string gender = GetGender();
        string theChosenCharacter = GetCharacterClass();

        player = CreateCharacterObject(theChosenCharacter, name, gender);
        
        player.AssignPronouns();          

    }



    /// <summary>
    /// This Method gets the player name and makes sure no empty value is passed.
    /// </summary>
    /// <returns>Players Name.</returns>
    public static string GetPlayerName()
    {
        Console.Write($"Enter your hero's name :");
        string playerName = Console.ReadLine();

        while (string.IsNullOrEmpty(playerName))
        {

            Console.Write($"Wrong input, please enter your hero's name :");
            playerName = Console.ReadLine();

        }
        return playerName;
    }



    /// <summary>
    /// This Method gets the gender of the player.
    /// </summary>
    /// <returns>Players Gender.</returns>
    static string GetGender()
    {
        Console.Write($"Enter either male or female for your hero's gender :");
        string playerGender = Console.ReadLine();

        while (true)
        {
            if (playerGender.Equals("male"))break;
            if (playerGender.Equals("female")) break;


            Console.Write($"Wrong input, please enter either male or female :");
            playerGender = Console.ReadLine();

        }
        return playerGender;
    }



    /// <summary>
    /// This Method ask the player whether he/she wants to be a Warrior , Mage or Archer.
    /// </summary>
    /// <returns>Players Character Class.</returns>
    static string GetCharacterClass()
    {
        Console.WriteLine($"1: Warrior  2:Mage  3:Archer  ");
        Console.Write($"Enter the ID of your hero's class :");

        string classID = Console.ReadLine();
        int id;
        while (string.IsNullOrEmpty(classID) || !int.TryParse(classID, out id) || ( id < 1 || id > 3) ) 
        {

            Console.Write($"Wrong input, please enter an number between 1 and 3.");
            classID = Console.ReadLine();
        }


        switch (id)
        {
            case 1:
                return "warrior";
            case 2:
                return "mage";
            case 3:
                return "archer";
            default:
                return String.Empty;
        }

    }




    /// <summary>
    /// This Method selects the desired weapon for the warrior character.
    /// </summary>
    /// <returns>Warriors Weapon string : sword, axe or lance.</returns>
    static string GetWarriorWeapon()
    {
        Console.WriteLine($"1:Sword  2:Axe  3:Lance ");
        Console.Write($"Enter the ID of your favorite weapon :");
        string ID = Console.ReadLine();
        int id;
        while (string.IsNullOrEmpty(ID) || !int.TryParse(ID, out id) || (id < 1 || id > 3) )
        {

            Console.Write($"Wrong input, please enter an number between 1 and 3.");
            ID = Console.ReadLine();

        }

        switch (id)
        {
            case 1:
                return "Sword";
            case 2:
                return "Axe";
            case 3:
                return "Lance";
            default:
                return String.Empty;
        }
    }



    /// <summary>
    /// This Method creates the player character object based on the name,
    /// gender and character class that was passed by the player.
    /// </summary>
    /// <param name="playerClass"> The players character class.</param>
    /// <param name="name"> The players character name.</param>
    /// <param name="gender"> The players character gender.</param>
    /// <returns>Player Character object of one of these types : Warrior, Mage, Archer.</returns>
    static Character CreateCharacterObject(string playerClass, string name, string gender)
    {

        switch (playerClass)
        {
            case "warrior":
                return new Warrior
                {
                    Name = name,
                    Gender = gender,
                    Arms = GetWarriorWeapon(),
                    Health = 250,
                    MaxHealth = 250,
                };
            case "mage":
                return new Mage
                {                   
                    Name = name,
                    Gender = gender,
                    Arms = "Spill",
                    Health = 220,
                    MaxHealth = 220,
                };
            case "archer":
                return new Archer
                {
                    Name = name,
                    Gender = gender,
                    Arms = "Bow",
                    Health = 220,
                    MaxHealth = 220,
                };
        }

        throw new Exception("Error on playerClass string.");

    }



    /// <summary>
    /// This Method creates an enemy character.
    /// </summary>
    /// <returns>Default Enemy Character object of one of these types : Warrior, Mage, Archer.</returns>
    static Character createComputerPlayer()
    {

        random = new Random();

        return enemyCharacters[random.Next(0, 3)];

    }



    /// <summary>
    /// This Method asks a yes/no question from the user.
    /// And returns the answer as a string.
    /// </summary>
    /// <returns>A string either yes or no.</returns>
    static string GetPlayerAnswer(string question)
    {
        Console.Write($"{question}");
        if(question.Contains("special attack"))
        {
            Console.Write($"\nEnter 1 for yes and 2 for no:");
        }
        


        string answer = Console.ReadLine();

        int id;
        while ( string.IsNullOrEmpty(answer) || !int.TryParse(answer, out id) || (id < 1 || id > 2) )
        {

            Console.Write($"Wrong input, please enter either 1 or 2 :");
            answer = Console.ReadLine();

        }

        switch (id)
        {
            case 1:
                return "attack";               
            case 2:
                return "heal";

            default:
                return String.Empty;

        }



    }



    /// <summary>
    /// This Method increases the health of the player 
    /// with a random value between 10 and 30.
    /// </summary>
    static void IncreaseHealth()
    {
        random = new Random();
        player.Health += random.Next(10, 41);
        //
        if ( player.Health > player.MaxHealth ) player.Health = player.MaxHealth;

        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Green;
        Console.WriteLine($"\t\t\t\t\t\t\t\t");
        Console.WriteLine($"Your health was increased to {player.Health}\t\t\t\t");
        Console.ResetColor();
        Console.WriteLine($"\t\t\t\t\t\t\t\t");
        luckyPotionCount--;
    }



}