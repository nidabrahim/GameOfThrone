using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DalInstanceSqlServer : IDal
    {

        private static string connexionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename='C:\\Users\\youssefNIDA\\Documents\\ISIMA\\S2\\web Services\\DB\\db_thrones.mdf';Integrated Security = True; Connect Timeout = 30";


        public DalInstanceSqlServer() { }



        public List<House> GetAllHouses()
        {
            List<House> houses = new List<House>();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM House", sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        House house = new House();
                        house.idEntityObject = Int32.Parse(sqlDataReader["idHouse"].ToString());
                        house.Name = sqlDataReader["name"].ToString();
                        house.NumberOfUnities = Int32.Parse(sqlDataReader["numberOfUnities"].ToString());

                        houses.Add(house);
                    }
                }

                foreach (House house in houses)
                {
                    sqlCommand = new SqlCommand("SELECT * FROM Character WHERE house_id = " + house.idEntityObject, sqlConnection);
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        List<Character> characters = new List<Character>();
                        while (sqlDataReader.Read())
                        {
                            Character character = new Character();
                            character.idEntityObject = Int32.Parse(sqlDataReader["idCharacter"].ToString());
                            character.FirstName = sqlDataReader["firstName"].ToString();
                            character.LastName = sqlDataReader["lastName"].ToString();
                            character.Bravoury = Int32.Parse(sqlDataReader["bravoury"].ToString());
                            character.Crazyness = Int32.Parse(sqlDataReader["crazyness"].ToString());
                            character.Pv = Int32.Parse(sqlDataReader["pv"].ToString());
                            //_relationships

                            characters.Add(character);

                        }
                        house.Housers = characters;
                    }
                }

                sqlConnection.Close();
            }

                return houses;
        }

        public List<House> GetAllHousesSup200Unit()
        {
            List<House> houses = GetAllHouses();
            foreach (House h in houses)
            {
                if (h.NumberOfUnities < 200) houses.Remove(h);
            }
            return houses;
        }

        public House GetHouseById(int id)
        {
            House house = new House();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM House WHERE IdHouse = " + id, sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        house.Name = sqlDataReader["name"].ToString();
                        house.NumberOfUnities = Int32.Parse(sqlDataReader["name"].ToString());
                        //Characters


                    }
                }
                sqlConnection.Close();
            }

            return house;
        }

        public void SaveHouse(House house)
        {
            throw new NotImplementedException();
        }

        public void UpdateHouse(House house)
        {
            throw new NotImplementedException();
        }

        public void DeleteHouse(House house)
        {
            throw new NotImplementedException();
        }



        public List<Character> GetAllCharacters()
        {
            List<Character> characters = new List<Character>();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Character", sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        Character character = new Character();
                        character.idEntityObject = Int32.Parse(sqlDataReader["idCharacter"].ToString());
                        character.FirstName = sqlDataReader["firstName"].ToString();
                        character.LastName = sqlDataReader["lastName"].ToString();
                        character.Bravoury = Int32.Parse(sqlDataReader["bravoury"].ToString());
                        character.Crazyness = Int32.Parse(sqlDataReader["crazyness"].ToString());
                        character.Pv = Int32.Parse(sqlDataReader["pv"].ToString());
                        //_relationships

                        characters.Add(character);
                    }
                }
                sqlConnection.Close();
            }

            return characters;
        }

        public Character GetCharacterById(int id)
        {
            Character character = new Character();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Character WHERE IdCharacter = " + id, sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        character.FirstName = sqlDataReader["firstName"].ToString();
                        character.LastName = sqlDataReader["lastName"].ToString();
                        character.Bravoury = Int32.Parse(sqlDataReader["bravoury"].ToString());
                        character.Crazyness = Int32.Parse(sqlDataReader["crazyness"].ToString());
                        character.Pv = Int32.Parse(sqlDataReader["pv"].ToString());
                        //_relationships

                    }
                }
                sqlConnection.Close();
            }

            return character;
        }

        public void SaveCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        public void UpdateCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        public void DeleteCharacter(Character character)
        {
            throw new NotImplementedException();
        }



        public List<Fight> GetAllFights()
        {
            throw new NotImplementedException();
        }

        public Fight GetFightById(int id)
        {
            Fight fight = new Fight();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Fight WHERE IdFight = " + id, sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        fight.idEntityObject = Int32.Parse(sqlDataReader["idFight"].ToString());
                        fight.HouseChalleged = GetHouseById(Int32.Parse(sqlDataReader["houseChalleged_id"].ToString()));
                        fight.HouseChalleging = GetHouseById(Int32.Parse(sqlDataReader["houseChalleging_id"].ToString()));
                        fight.WinningHouse = GetHouseById(Int32.Parse(sqlDataReader["winningHouse_id"].ToString()));
                        fight.Territory = GetTerritoryById(Int32.Parse(sqlDataReader["territory_id"].ToString()));
                        //fight.War = war;

                    }
                }
                sqlConnection.Close();
            }

            return fight;
        }

        public void SaveFight(Fight fight)
        {
            throw new NotImplementedException();
        }

        public void UpdateFight(Fight fight)
        {
            throw new NotImplementedException();
        }

        public void DeleteFight(Fight fight)
        {
            throw new NotImplementedException();
        }



        public List<Territory> GetAllTerritories()
        {
            List<Territory> Territories = new List<Territory>();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Territory", sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        Territory territory = new Territory();
                        territory.idEntityObject = Int32.Parse(sqlDataReader["IdTerritory"].ToString());
                        territory.Owner = GetCharacterById(Int32.Parse(sqlDataReader["owner_id"].ToString()));
                        territory.TerritoryType = GetTerritoryTypeById(Int32.Parse(sqlDataReader["territoryType_id"].ToString()));


                        Territories.Add(territory);
                    }
                }
                sqlConnection.Close();
            }

            return Territories;
        }
        
        public Territory GetTerritoryById(int id)
        {
            Territory territory = new Territory();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Territory WHERE IdTerritory = " + id, sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        territory.idEntityObject = Int32.Parse(sqlDataReader["IdTerritory"].ToString());
                        territory.Owner = GetCharacterById(Int32.Parse(sqlDataReader["owner_id"].ToString()));
                        territory.TerritoryType = GetTerritoryTypeById(Int32.Parse(sqlDataReader["territoryType_id"].ToString()));

                    }
                }
                sqlConnection.Close();
            }

            return territory;
        }

        public void SaveTerritory(Territory territory)
        {
            throw new NotImplementedException();
        }

        public void UpdateTerritory(Territory territory)
        {
            throw new NotImplementedException();
        }

        public void DeleteTerritory(Territory territory)
        {
            throw new NotImplementedException();
        }



        public TerritoryType GetTerritoryTypeById(int id)
        {
            TerritoryType territoryType = new TerritoryType();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM TerritoryType WHERE IdTerritoryType = " + id, sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        territoryType.IdTerritoryType = Int32.Parse(sqlDataReader["IdTerritoryType"].ToString());
                        territoryType.Name = sqlDataReader["name"].ToString();
                    }
                }
                sqlConnection.Close();
            }

            return territoryType;
        }


        public List<War> GetAllWars()
        {
            List<War> Wars = new List<War>();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM War", sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        War war = new War();
                        war.idEntityObject = Int32.Parse(sqlDataReader["idWar"].ToString());


                        Wars.Add(war);
                    }
                }

                foreach (War war in Wars)
                {
                    sqlCommand = new SqlCommand("SELECT * FROM Fight WHERE war_id = " + war.idEntityObject, sqlConnection);
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        List<Fight> fights = new List<Fight>();
                        while (sqlDataReader.Read())
                        {
                            Fight fight = new Fight();
                            fight.idEntityObject = Int32.Parse(sqlDataReader["idFight"].ToString());
                            fight.HouseChalleged = GetHouseById(Int32.Parse(sqlDataReader["houseChalleged_id"].ToString()));
                            fight.HouseChalleging = GetHouseById(Int32.Parse(sqlDataReader["houseChalleging_id"].ToString()));
                            fight.WinningHouse = GetHouseById(Int32.Parse(sqlDataReader["winningHouse_id"].ToString()));
                            fight.Territory = GetTerritoryById(Int32.Parse(sqlDataReader["territory_id"].ToString()));
                            fight.War = war;



                            fights.Add(fight);

                        }
                        war.Fights = fights;
                    }
                }

                sqlConnection.Close();
            }

            return Wars;
        }
    }
}
