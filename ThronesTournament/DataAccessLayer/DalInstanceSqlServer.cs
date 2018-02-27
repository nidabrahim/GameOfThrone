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

        private static string connexionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='C:\\Users\\youssefNIDA\\Documents\\ISIMA\\S2\\web Services\\Projet\\BD\\db_thrones.mdf';Integrated Security=True;Connect Timeout=30";


        public DalInstanceSqlServer() { }



        public List<House> GetAllHouses()
        {
            List<House> houses = new List<House>();

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
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
                            character.Type = GetCharacterTypeById(Int32.Parse(sqlDataReader["characterType_id"].ToString()));
               

                            characters.Add(character);

                        }
                        house.Housers = characters;
                    }

                    foreach(Character character in house.Housers)
                    {
                        sqlCommand = new SqlCommand("SELECT * FROM Relation WHERE idCharacter1 = " + character.idEntityObject, sqlConnection);
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            List<Relation> relations = new List<Relation>();
                            while (sqlDataReader.Read())
                            {
                                Relation relation = new Relation();
       
                                relation.Character = GetCharacterById( Int32.Parse(sqlDataReader["idCharacter2"].ToString()) );
                                relation.RelationType = GetRelationTypeById( Int32.Parse(sqlDataReader["idRelationType"].ToString()) );
                                
                                relations.Add(relation);

                            }
                            character.Relations = relations;
                        }

                    }
                }

                sqlConnection.Close();
            }

                return houses;
        }

        public House GetHouseById(int id)
        {
            House house = new House();

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM House WHERE IdHouse = " + id, sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        house.idEntityObject = Int32.Parse(sqlDataReader["IdHouse"].ToString());
                        house.Name = sqlDataReader["name"].ToString();
                        house.NumberOfUnities = Int32.Parse(sqlDataReader["numberOfUnities"].ToString());
                    }
                }

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
                        character.Type = GetCharacterTypeById(Int32.Parse(sqlDataReader["characterType_id"].ToString()));


                        characters.Add(character);

                    }
                    house.Housers = characters;
                }

                foreach (Character character in house.Housers)
                {
                    sqlCommand = new SqlCommand("SELECT * FROM Relation WHERE idCharacter1 = " + character.idEntityObject, sqlConnection);
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        List<Relation> relations = new List<Relation>();
                        while (sqlDataReader.Read())
                        {
                            Relation relation = new Relation();

                            relation.Character = GetCharacterById(Int32.Parse(sqlDataReader["idCharacter2"].ToString()));
                            relation.RelationType = GetRelationTypeById(Int32.Parse(sqlDataReader["idRelationType"].ToString()));

                            relations.Add(relation);

                        }
                        character.Relations = relations;
                    }

                }

                sqlConnection.Close();
            }

            return house;
        }

        public void SaveHouse(House house)
        {
            String insertHouseRequest = "INSERT INTO House(name,numberOfUnities) VALUES (@Name,@NumberOfUnities)";

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
            {
                sqlConnection.Open();

                SqlCommand insertCommand = new SqlCommand(insertHouseRequest, sqlConnection);
                insertCommand.Parameters.AddWithValue("@Name", house.Name);
                insertCommand.Parameters.AddWithValue("@NumberOfUnities", house.NumberOfUnities);
                insertCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }

        public void UpdateHouse(House house)
        {
            String updateHouseRequest = "UPDATE House SET name = @Name , numberOfUnities = @NumberOfUnities Where IdHouse = @IdHouse";

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
            {
                sqlConnection.Open();
                
                SqlCommand updateCommand = new SqlCommand(updateHouseRequest, sqlConnection);
                updateCommand.Parameters.AddWithValue("@Name", house.Name);
                updateCommand.Parameters.AddWithValue("@NumberOfUnities", house.NumberOfUnities);

                updateCommand.ExecuteNonQuery();
               
                sqlConnection.Close();
            }
        }

        public void DeleteHouse(House house)
        {
            String deleteHouseRequest = "DELETE FROM House WHERE IdHouse = @IdHouse";
            String deleteFightRequest = "DELETE FROM Fight WHERE houseChalleging_id = @IdHouse OR houseChalleged_id = @IdHouse OR winningHouse_id = @IdHouse";
            String deleteCharacterRequest = "DELETE FROM Character WHERE IdCharacter = @IdCharacter";
            String deleteRelationRequest = "DELETE FROM Relation WHERE IdCharacter1 = @IdCharacter OR IdCharacter2 = @IdCharacter";
            String deleteTerritoryRequest = "DELETE FROM Territory WHERE owner_id = @IdCharacter";

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
            {
                sqlConnection.Open();
              
                SqlCommand deleteFightCommand = new SqlCommand(deleteFightRequest, sqlConnection);
                deleteFightCommand.Parameters.AddWithValue("@IdHouse", house.idEntityObject);
                deleteFightCommand.ExecuteNonQuery();

                foreach (Character character in house.Housers)
                {
                    SqlCommand deleteTerritoryCommand = new SqlCommand(deleteTerritoryRequest, sqlConnection);
                    deleteTerritoryCommand.Parameters.AddWithValue("@IdCharacter", character.idEntityObject);
                    deleteTerritoryCommand.ExecuteNonQuery();

                    SqlCommand deleteRelationCommand = new SqlCommand(deleteRelationRequest, sqlConnection);
                    deleteRelationCommand.Parameters.AddWithValue("@IdCharacter", character.idEntityObject);
                    deleteRelationCommand.ExecuteNonQuery();

                    SqlCommand deleteCharacterCommand = new SqlCommand(deleteCharacterRequest, sqlConnection);
                    deleteCharacterCommand.Parameters.AddWithValue("@IdCharacter", character.idEntityObject);
                    deleteCharacterCommand.ExecuteNonQuery();
                }
                    
                SqlCommand deleteHouseCommand = new SqlCommand(deleteHouseRequest, sqlConnection);
                deleteHouseCommand.Parameters.AddWithValue("@IdHouse", house.idEntityObject);
                deleteHouseCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }



        public List<Character> GetAllCharacters()
        {
            List<Character> characters = new List<Character>();

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
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
                        character.Type = GetCharacterTypeById(Int32.Parse(sqlDataReader["characterType_id"].ToString()));

                        characters.Add(character);
                    }
                }

                foreach (Character character in characters)
                {
                    sqlCommand = new SqlCommand("SELECT * FROM Relation WHERE idCharacter1 = " + character.idEntityObject, sqlConnection);
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        List<Relation> relations = new List<Relation>();
                        while (sqlDataReader.Read())
                        {
                            Relation relation = new Relation();

                            relation.Character = GetCharacterById(Int32.Parse(sqlDataReader["idCharacter2"].ToString()));
                            relation.RelationType = GetRelationTypeById(Int32.Parse(sqlDataReader["idRelationType"].ToString()));

                            relations.Add(relation);

                        }
                        character.Relations = relations;
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
                        character.idEntityObject = Int32.Parse(sqlDataReader["idCharacter"].ToString());
                        character.FirstName = sqlDataReader["firstName"].ToString();
                        character.LastName = sqlDataReader["lastName"].ToString();
                        character.Bravoury = Int32.Parse(sqlDataReader["bravoury"].ToString());
                        character.Crazyness = Int32.Parse(sqlDataReader["crazyness"].ToString());
                        character.Pv = Int32.Parse(sqlDataReader["pv"].ToString());
                        character.Type = GetCharacterTypeById(Int32.Parse(sqlDataReader["characterType_id"].ToString()));

                    }
                }

                sqlCommand = new SqlCommand("SELECT * FROM Relation WHERE idCharacter1 = " + character.idEntityObject, sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    List<Relation> relations = new List<Relation>();
                    while (sqlDataReader.Read())
                    {
                        Relation relation = new Relation();

                        relation.Character = GetCharacterById(Int32.Parse(sqlDataReader["idCharacter2"].ToString()));
                        relation.RelationType = GetRelationTypeById(Int32.Parse(sqlDataReader["idRelationType"].ToString()));

                        relations.Add(relation);

                    }
                    character.Relations = relations;
                }

                sqlConnection.Close();
            }

            return character;
        }
        
        public void SaveCharacter(Character character)
        {
            String insertCharacterRequest = "INSERT INTO Character(firstName,lastName,bravoury,crazyness,pv,characterType_id,house_id) VALUES (@FirstName,@LastName,@Bravoury,@Crazyness,@Pv,@CharacterType_id,@House_id)";
            String insertRelationRequest = "INSERT INTO Relation VALUES (@IdCharacter1,@IdCharacter2,@IdRelationType)";
            Int32 idCharacter = 0;

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
            {
                sqlConnection.Open();
                
                SqlCommand sqlCommand = new SqlCommand("SELECT IDENT_CURRENT ('Character') AS Current_Identity", sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        idCharacter = Int32.Parse(sqlDataReader["Current_Identity"].ToString());
                    }
                }
                
                SqlCommand insertFirstCommand = new SqlCommand(insertCharacterRequest, sqlConnection);
                insertFirstCommand.Parameters.AddWithValue("@FirstName", character.FirstName);
                insertFirstCommand.Parameters.AddWithValue("@LastName", character.LastName);
                insertFirstCommand.Parameters.AddWithValue("@Bravoury", character.Bravoury);
                insertFirstCommand.Parameters.AddWithValue("@Crazyness", character.Crazyness);
                insertFirstCommand.Parameters.AddWithValue("@Pv", character.Pv);
                insertFirstCommand.Parameters.AddWithValue("@CharacterType_id", character.Type.IdCharacterType);
                insertFirstCommand.Parameters.AddWithValue("@House_id", character.House.idEntityObject);
                insertFirstCommand.ExecuteNonQuery();
                
                foreach (Relation relation in character.Relations)
                {
                    SqlCommand insertSecondCommand = new SqlCommand(insertRelationRequest, sqlConnection);
                    insertSecondCommand.Parameters.AddWithValue("@IdCharacter1", idCharacter + 1);
                    insertSecondCommand.Parameters.AddWithValue("@IdCharacter2", relation.Character.idEntityObject);
                    insertSecondCommand.Parameters.AddWithValue("@IdRelationType", relation.RelationType.IdRelationType);
                    insertSecondCommand.ExecuteNonQuery();
                }

                sqlConnection.Close();
            }
        }

        public void UpdateCharacter(Character character)
        {
            String updateCharacterRequest = "UPDATE Character SET firstName = @FirstName, lastName = @LastName, bravoury = @Bravoury, crazyness = @Crazyness, pv = @Pv, characterType_id = @CharacterType_id  Where IdCharacter = @IdCharacter";
            String updateRelationRequest = "UPDATE Relation SET idCharacter2 = @IdCharacter2, idRelationType = @IdRelationType WHERE idCharacter1 = @IdCharacter";

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
            {
                sqlConnection.Open();
              
                SqlCommand updateCharacterCommand = new SqlCommand(updateCharacterRequest, sqlConnection);
                updateCharacterCommand.Parameters.AddWithValue("@FirstName", character.FirstName);
                updateCharacterCommand.Parameters.AddWithValue("@LastName", character.LastName);
                updateCharacterCommand.Parameters.AddWithValue("@Bravoury", character.Bravoury);
                updateCharacterCommand.Parameters.AddWithValue("@Crazyness", character.Crazyness);
                updateCharacterCommand.Parameters.AddWithValue("@Pv", character.Pv);
                updateCharacterCommand.Parameters.AddWithValue("@CharacterType_id", character.Type.IdCharacterType);
                updateCharacterCommand.Parameters.AddWithValue("@IdCharacter", character.idEntityObject);
                updateCharacterCommand.ExecuteNonQuery();

                foreach (Relation relation in character.Relations)
                {
                    SqlCommand insertSecondCommand = new SqlCommand(updateRelationRequest, sqlConnection);
                    insertSecondCommand.Parameters.AddWithValue("@IdCharacter2", relation.Character.idEntityObject);
                    insertSecondCommand.Parameters.AddWithValue("@IdRelationType", relation.RelationType.IdRelationType);
                    insertSecondCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }

        public void DeleteCharacter(Character character)
        {
            String deleteCharacterRequest = "DELETE FROM Character WHERE IdCharacter = @IdCharacter";
            String deleteRelationRequest = "DELETE FROM Relation WHERE IdCharacter1 = @IdCharacter OR IdCharacter2 = @IdCharacter";
            String deleteTerritoryRequest = "DELETE FROM Territory WHERE owner_id = @IdCharacter";

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
            {
                sqlConnection.Open();

                SqlCommand deleteTerritoryCommand = new SqlCommand(deleteTerritoryRequest, sqlConnection);
                deleteTerritoryCommand.Parameters.AddWithValue("@IdCharacter", character.idEntityObject);
                deleteTerritoryCommand.ExecuteNonQuery();

                SqlCommand deleteRelationCommand = new SqlCommand(deleteRelationRequest, sqlConnection);
                deleteRelationCommand.Parameters.AddWithValue("@IdCharacter", character.idEntityObject);
                deleteRelationCommand.ExecuteNonQuery();

                SqlCommand deleteCharacterCommand = new SqlCommand(deleteCharacterRequest, sqlConnection);
                deleteCharacterCommand.Parameters.AddWithValue("@IdCharacter", character.idEntityObject);
                deleteCharacterCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }



        public List<Fight> GetAllFights()
        {
            List<Fight> fights = new List<Fight>();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Fight", sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        Fight fight = new Fight();
                        fight.idEntityObject = Int32.Parse(sqlDataReader["idFight"].ToString());
                        fight.HouseChalleged = GetHouseById(Int32.Parse(sqlDataReader["houseChalleged_id"].ToString()));
                        fight.HouseChalleging = GetHouseById(Int32.Parse(sqlDataReader["houseChalleging_id"].ToString()));
                        fight.WinningHouse = GetHouseById(Int32.Parse(sqlDataReader["winningHouse_id"].ToString()));
                        fight.Territory = GetTerritoryById(Int32.Parse(sqlDataReader["territory_id"].ToString()));
                        fight.War = GetWarById(Int32.Parse(sqlDataReader["war_id"].ToString()));

                        fights.Add(fight);

                    }
                }

                sqlConnection.Close();
            }

            return fights;
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
                        fight.War = GetWarById(Int32.Parse(sqlDataReader["war_id"].ToString()));

                    }
                }
                sqlConnection.Close();
            }

            return fight;
        }

        public List<Fight> GetFightsByIdHouse(int id)
        {
            List<Fight> fights = new List<Fight>();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from Fight where houseChalleging_id = " + id +"or houseChalleged_id = "+ id , sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        Fight fight = new Fight();
                        fight.idEntityObject = Int32.Parse(sqlDataReader["idFight"].ToString());
                        fight.HouseChalleged = GetHouseById(Int32.Parse(sqlDataReader["houseChalleged_id"].ToString()));
                        fight.HouseChalleging = GetHouseById(Int32.Parse(sqlDataReader["houseChalleging_id"].ToString()));
                        fight.WinningHouse = GetHouseById(Int32.Parse(sqlDataReader["winningHouse_id"].ToString()));
                        fight.Territory = GetTerritoryById(Int32.Parse(sqlDataReader["territory_id"].ToString()));
                        fight.War = GetWarById(Int32.Parse(sqlDataReader["war_id"].ToString()));

                        fights.Add(fight);
                    }

                    
                }
                sqlConnection.Close();
            }

            return fights;
        }

        public void SaveFight(Fight fight)
        {
            String insertFightRequest = "INSERT INTO Fight(houseChalleging_id,houseChalleged_id,winningHouse_id,territory_id,war_id) VALUES (@HouseChalleging_id,@HouseChalleged_id,@WinningHouse_id,@Territory_id,@War_id)";

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
               
                SqlCommand insertCommand = new SqlCommand(insertFightRequest, sqlConnection);
                insertCommand.Parameters.AddWithValue("@HouseChalleging_id", fight.HouseChalleging.idEntityObject);
                insertCommand.Parameters.AddWithValue("@HouseChalleged_id", fight.HouseChalleged.idEntityObject);
                insertCommand.Parameters.AddWithValue("@WinningHouse_id", fight.WinningHouse.idEntityObject);
                insertCommand.Parameters.AddWithValue("@Territory_id", fight.Territory.idEntityObject);
                insertCommand.Parameters.AddWithValue("@War_id", fight.War.idEntityObject);
                insertCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }

        public void UpdateFight(Fight fight)
        {
            String updateFightRequest = "UPDATE Fight SET houseChalleging_id = @HouseChalleging_id ,houseChalleged_id = @HouseChalleged_id ,winningHouse_id = @WinningHouse_id ,territory_id = @Territory_id ,war_id =  @War_id WHERE idFight = @idFight ";

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();

                SqlCommand updateCommand = new SqlCommand(updateFightRequest, sqlConnection);
                updateCommand.Parameters.AddWithValue("@HouseChalleging_id", fight.HouseChalleging.idEntityObject);
                updateCommand.Parameters.AddWithValue("@HouseChalleged_id", fight.HouseChalleged.idEntityObject);
                updateCommand.Parameters.AddWithValue("@WinningHouse_id", fight.WinningHouse.idEntityObject);
                updateCommand.Parameters.AddWithValue("@Territory_id", fight.Territory.idEntityObject);
                updateCommand.Parameters.AddWithValue("@War_id", fight.War.idEntityObject);
                updateCommand.Parameters.AddWithValue("@idFight", fight.idEntityObject);
                updateCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }

        public void DeleteFight(Fight fight)
        {
            String deleteFightRequest = "DELETE FROM Fight WHERE IdFight = @IdFight";
           
            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();

                SqlCommand deleteFightCommand = new SqlCommand(deleteFightRequest, sqlConnection);
                deleteFightCommand.Parameters.AddWithValue("@IdFight", fight.idEntityObject);
                deleteFightCommand.ExecuteNonQuery();
                
                sqlConnection.Close();
            }
        }



        public List<Territory> GetAllTerritories()
        {
            List<Territory> Territories = new List<Territory>();

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
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
            String insertTerritoryRequest = "INSERT INTO Territory(territoryType_id,owner_id) VALUES (@TerritoryType_id,@Owner_id)";

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
            {
                sqlConnection.Open();

                SqlCommand insertCommand = new SqlCommand(insertTerritoryRequest, sqlConnection);
                insertCommand.Parameters.AddWithValue("@TerritoryType_id", territory.TerritoryType.IdTerritoryType);
                insertCommand.Parameters.AddWithValue("@Owner_id", territory.Owner.idEntityObject);
                insertCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }

        public void UpdateTerritory(Territory territory)
        {
            String updateTerritoryRequest = "UPDATE Territory SET territoryType_id = @TerritoryType_id ,owner_id = @Owner_id WHERE idTerritory = @IdTerritory ";

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
            {
                sqlConnection.Open();

                SqlCommand updateCommand = new SqlCommand(updateTerritoryRequest, sqlConnection);
                updateCommand.Parameters.AddWithValue("@TerritoryType_id", territory.TerritoryType.IdTerritoryType);
                updateCommand.Parameters.AddWithValue("@Owner_id", territory.Owner.idEntityObject);
                updateCommand.Parameters.AddWithValue("@IdTerritory", territory.idEntityObject);
                updateCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }

        public void DeleteTerritory(Territory territory)
        {
            throw new NotImplementedException();
        }



        public List<TerritoryType> GetAllTerritoryTypes()
        {
            List<TerritoryType> territoryTypes = new List<TerritoryType>();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM TerritoryType", sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        TerritoryType territorytype = new TerritoryType();

                        territorytype.IdTerritoryType = Int32.Parse(sqlDataReader["IdTerritoryType"].ToString());
                        territorytype.Name = sqlDataReader["name"].ToString();

                        territoryTypes.Add(territorytype);
                    }
                }
                sqlConnection.Close();
            }

            return territoryTypes;
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

        public void SaveTerritoryType(TerritoryType territoryType)
        {
            String insertTerritoryTypeRequest = "INSERT INTO TerritoryType(name) VALUES (@Name)";

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
            {
                sqlConnection.Open();

                SqlCommand insertCommand = new SqlCommand(insertTerritoryTypeRequest, sqlConnection);
                insertCommand.Parameters.AddWithValue("@Name", territoryType.Name);
                insertCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }



        public List<CharacterType> GetAllCharacterTypes()
        {
            List<CharacterType> characterTypes = new List<CharacterType>();

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM CharacterType", sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        CharacterType characterType = new CharacterType();

                        characterType.IdCharacterType = Int32.Parse(sqlDataReader["IdCharacterType"].ToString());
                        characterType.Name = sqlDataReader["name"].ToString();

                        characterTypes.Add(characterType);
                    }
                }
                sqlConnection.Close();
            }

            return characterTypes;
        }

        public CharacterType GetCharacterTypeById(int id)
        {
            CharacterType characterType = new CharacterType();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM CharacterType WHERE IdCharacterType = " + id, sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        characterType.IdCharacterType = Int32.Parse(sqlDataReader["IdCharacterType"].ToString());
                        characterType.Name = sqlDataReader["name"].ToString();
                    }
                }
                sqlConnection.Close();
            }

            return characterType;
        }

        public void SaveCharacterType(CharacterType characterType)
        {
            String insertCharacterTypeTypeRequest = "INSERT INTO CharacterType(name) VALUES (@Name)";

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
            {
                sqlConnection.Open();

                SqlCommand insertCommand = new SqlCommand(insertCharacterTypeTypeRequest, sqlConnection);
                insertCommand.Parameters.AddWithValue("@Name", characterType.Name);
                insertCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }




        public List<RelationType> GetAllRelationTypes()
        {
            List<RelationType> relationTypes = new List<RelationType>();

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RelationType", sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        RelationType relationType = new RelationType();

                        relationType.IdRelationType = Int32.Parse(sqlDataReader["IdRelationType"].ToString());
                        relationType.Name = sqlDataReader["name"].ToString();

                        relationTypes.Add(relationType);
                    }
                }
                sqlConnection.Close();
            }

            return relationTypes;
        }

        public RelationType GetRelationTypeById(int id)
        {
            RelationType relationType = new RelationType();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RelationType WHERE IdRelationType = " + id, sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        relationType.IdRelationType = Int32.Parse(sqlDataReader["IdRelationType"].ToString());
                        relationType.Name = sqlDataReader["name"].ToString();
                    }
                }
                sqlConnection.Close();
            }

            return relationType;
        }

        public void SaveRelationType(RelationType relationType)
        {
            String insertRelationTypeTypeTypeRequest = "INSERT INTO RelationType(name) VALUES (@Name)";

            using (SqlConnection sqlConnection = (Connexion.Instance).SqlConnection)
            {
                sqlConnection.Open();

                SqlCommand insertCommand = new SqlCommand(insertRelationTypeTypeTypeRequest, sqlConnection);
                insertCommand.Parameters.AddWithValue("@Name", relationType.Name);
                insertCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
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
                        war.Name = sqlDataReader["name"].ToString();


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

        public War GetWarById(int id)
        {
            War war = new War();

            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM War WHERE idWar = " + id, sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        war.idEntityObject = Int32.Parse(sqlDataReader["idWar"].ToString());
                        war.Name = sqlDataReader["name"].ToString();

                    }
                }

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

                sqlConnection.Close();
            }

            return war;
        }

        public int GetLastId(String table)
        {
            int index = -1;
            using (SqlConnection sqlConnection = new SqlConnection(connexionString))
            {
                sqlConnection.Open();
               
                SqlCommand sqlCommand = new SqlCommand("SELECT IDENT_CURRENT ('" + table + "') AS Current_Identity", sqlConnection);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        index = Int32.Parse(sqlDataReader["Current_Identity"].ToString());
                    }
                }
                sqlConnection.Close();
            }
            return index;
        }



    }
}
