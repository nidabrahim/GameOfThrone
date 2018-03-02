using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer;
using BusinessLayer;

namespace ThronesTournamentConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instanciate the Business Manager
            ThronesTournamentManager bm = new ThronesTournamentManager();

            //choice : the variable containing the user's choice
            int choice;

            //Menu (5 to quit)
            do
            {

                //Print the menu
                Console.Clear();
                Console.Write(mainMenu());

                //Let the user make their choice
                choice = Convert.ToInt32(Console.ReadLine());
                string result = mainMenuChoice(bm, choice);

                //Menu output
                Console.Clear();
                Console.WriteLine(result);
                Console.Write("\n*****Press Enter*****");
                Console.ReadLine();
            } while (choice != 8);
        }

        public static string mainMenu()
        {

            return " ****** Menu ******\n "
            + " 1 – List all Houses\n "
            + " 2 – List all Characters\n "
            + " 3 – List all Territories\n "
            + " 4 – List all Fights\n "
            + " 5 – Make 2 Houses fight each other\n "
            + " 6 – Add a House\n "
            + " 7 – Add a Character\n "
            + " 8 – Quit\n "
            + " Choice ? : ";
        }

        public static string mainMenuChoice(ThronesTournamentManager business, int choice)
        {
            string result = "";

            switch (choice)
            {
                case 1:
                    foreach (House s in business.ListHouses())
                    {
                        Console.Write("\n*****HOUSES*****\n");
                        result += s + "\n";
                    }
                    break;
                case 2:
                    foreach (Character s in business.ListCharacters())
                    {
                        Console.Write("\n*****CHARACTERS*****\n");
                        result += s + "\n";
                    }
                    break;
                case 3:
                    foreach (Territory s in business.ListTerritories())
                    {
                        Console.Write("\n*****TERRITORIES*****\n");
                        result += s + "\n";
                    }
                    break;
                case 4:
                    foreach (Fight s in business.ListFights())
                    {
                        Console.Write("\n*****FIGHTS*****\n");
                        result += s + "\n";
                    }
                    break;
                case 5:
                    String winner = combatMenu(business);
                    result += "The winner is : " + winner + "\n";
                    break;
                case 6:
                    addHouseMenu(business);
                    Console.WriteLine("House Created\n");
                    break;
                case 7:
                    addCharacterMenu(business);
                    Console.WriteLine("Character Created\n");
                    break;
                case 8:
                    result += "Goodbye";
                    break;
                default:
                    result += "Bad choice, try again ";
                    break;
            }
            return result;
        }

        public static String combatMenu(ThronesTournamentManager business)
        {
            //Initialize local variables
            int h1, h2, t, i = 0;
            String list = "";

            //Print the first house menu
            Console.Clear();
            Console.WriteLine("*****COMBAT*****\n");
            Console.WriteLine("Select the first house\n");
            foreach (String s in business.ListHousesNames())
            {
                list += i + ":" + s + "\n";
                i++;
            }
            Console.WriteLine(list);

            //The user chooses the first house
            Console.Write("\n*****Choose the first house:");
            h1 = Convert.ToInt32(Console.ReadLine());

            //Print the second house menu
            i = 0; list = "";
            Console.Clear();
            Console.WriteLine("Select the second house\n");
            foreach (String s in business.ListHousesNames())
            {
                if (i != h1)
                {
                    list += i + ":" + s + "\n";
                }
                i++;
            }
            Console.WriteLine(list);

            //The user chooses the second house
            Console.Write("\n*****Choose the second house:");
            h2 = Convert.ToInt32(Console.ReadLine());

            //Print the territory menu
            i = 0; list = "";
            Console.Clear();
            Console.WriteLine("Select the territory\n");
            foreach (Territory s in business.ListTerritories())
            {
                list += i + ":" + s + "\n";
                i++;
            }
            Console.WriteLine(list);

            //The user chooses the territory
            Console.Write("\n*****Choose the territory:");
            t = Convert.ToInt32(Console.ReadLine());

            //Make the houses fight and display the winner
            House winningHouse = business.Combat(h1, h2, t);
            return winningHouse.Name;
        }

        public static void addHouseMenu(ThronesTournamentManager business)
        {
            //Initialize local variables
            String name;
            int units;

            //House's name
            Console.Clear();
            Console.WriteLine("*****ADD A HOUSE*****\n");
            Console.WriteLine("Enter the house's name:");
            name = Console.ReadLine();
            Console.WriteLine("\n");

            //Number of units
            Console.WriteLine("Enter the number of units:");
            units = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n");

            //Add the house
            business.AddHouse(name, units);
        }

        public static void addCharacterMenu(ThronesTournamentManager business)
        {
            //Initialize local variables
            String firstname, lastname, list = "";
            int craziness, bravery, pv, type, i = 0, h;

            //Firstname
            Console.Clear();
            Console.WriteLine("Enter the house's name:");
            firstname = Console.ReadLine();
            Console.WriteLine("\n");

            //Lastname
            Console.WriteLine("Enter the house's name:");
            lastname = Console.ReadLine();
            Console.WriteLine("\n");

            //Bravery
            Console.WriteLine("Enter the bravery level:");
            bravery = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n");

            //Craziness
            Console.WriteLine("Enter the craziness level:");
            craziness = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n");

            //PV
            Console.WriteLine("Enter the number of health points:");
            pv = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n");

            //Type
            Console.WriteLine("Select the character's type\n0) WARRIOR\n1) WITCH \n2) TACTICIAN\n 3) LEADER\n4) LOSER\nYour choice:");
            type = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(list);

            //House
            Console.WriteLine("Select house\n");
            foreach (String s in business.ListHousesNames())
            {
                list += i + ":" + s + "\n";
                i++;
            }
            Console.WriteLine(list);

            //The user chooses the first house
            Console.Write("\n*****Choose the first house:");
            h = Convert.ToInt32(Console.ReadLine());

            //Create the character
            business.AddCharacter(firstname, lastname, bravery, craziness, pv, type,h);

        }
    }
}
