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
        
         public static void Main(string[] args)
         {
            Program prog = new Program();
            ThronesTournamentManager mng = new ThronesTournamentManager(); 
            int choix;
            char revenir = 'o';
            
            do {
                Console.Clear();
                prog.Menu();
                Console.WriteLine("\n\n >> Choix : ");
                choix = Int32.Parse(Console.ReadLine());

                switch (choix)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("HOUSES\n");
                        List<House> houses = mng.ListHouses();
                        foreach(House house in houses)
                        {
                            Console.WriteLine(house);
                        }

                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("CHARACTER\n");
                        List<Character> characters = mng.ListCharacters();
                        foreach (Character character in characters)
                        {

                            Console.WriteLine(character);
                        }

                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("TERRITORIES\n");
                        List<Territory> territories = mng.ListTerritories();
                        foreach (Territory territory in territories)
                        {

                            Console.WriteLine(territory);
                        }

                        break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine("FIGHTS\n");
                        List<Fight> fights = mng.ListFights();
                        foreach (Fight fight in fights)
                        {

                            Console.WriteLine(fight);
                        }

                        break;

                    case 5:

                        House winningHouse = mng.Combat(1, 2, 1);
                        Console.WriteLine(winningHouse);

                        break;

                    case 6:

                        break;

                    default :
                        Console.WriteLine("Choix invalid");
                        break;

                }

                Console.WriteLine("Revenir au Menu principal (o/n) : ");
                revenir = Console.ReadKey().KeyChar;

            } while (choix != 6 &&  revenir == 'o');
            /*
            House house = new House("H1");
            House house2 = new House("H2");

            Character c = new Character();
            Character c1 = new Character("fff","yyh");
            Character c2 = new Character("fff", "yyh", CharaterTypeEnum.LEADER, 0,0);

            c.AddRelative(c2, RelationshipEnum.FRIENDSHIP);
            c.AddRelative(c1, RelationshipEnum.HATRED);
            c1.AddRelative(c2, RelationshipEnum.RIVALRY);
            c2.AddRelative(c, RelationshipEnum.FRIENDSHIP);

            Console.WriteLine(c);
            Console.WriteLine(c1);
            Console.WriteLine(c2);

            house.AddHousers(ref c);
            house.AddHousers(ref c1);

           
            house2.AddHousers(ref c2);

            Console.WriteLine(house);
            Console.WriteLine(house2);

            Territory territory = new Territory(TerritoryType.MOUNTAIN, c);
        
            Fight fight = new Fight(house,house2,territory);
            fight.Winner();*/
            
         }

        private void Menu()
        {

            Console.WriteLine("Menu Principal\n");
            Console.WriteLine("\t *1* Lister les Houses\n");
            Console.WriteLine("\t *2* Lister les Characters\n");
            Console.WriteLine("\t *3* Lister les Territories\n");
            Console.WriteLine("\t *4* Lister les Fights\n");
            Console.WriteLine("\t *5* Combat\n");
            Console.WriteLine("\t *6* Quitter\n");
        }
    }
}
