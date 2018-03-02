using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer;
using WebApiGoT.Models;

namespace WebApiGoT.Controllers
{
    [RoutePrefix("api/character")]
    public class CharacterController : ApiController
    {
        ThronesTournamentManager businessManager = new ThronesTournamentManager();
        

        [Route("GetAllCharacters")]
        public List<CharacterDTO> GetAllCharacters()
        {
            List<CharacterDTO> listCharacter = new List<CharacterDTO>();

            foreach(var character in businessManager.ListCharacters())
            {
                //listCharacter.Add(new CharacterDTO(character));
                listCharacter.Add(new CharacterDTO() {
                    FirstName = character.FirstName,
                    LastName = character.LastName,
                    Bravoury = character.Bravoury,
                    Crazyness = character.Crazyness,
                    Pv = character.Pv
                });
            }

            return listCharacter;
        }

        [Route("GetCharacterById/{id:int}")]
        public CharacterDTO GetCharacterById(int id)
        {
            var character = businessManager.GetCharacter(id);

            CharacterDTO characterDTO = new CharacterDTO()
            {
                FirstName = character.FirstName,
                LastName = character.LastName,
                Bravoury = character.Bravoury,
                Crazyness = character.Crazyness,
                Pv = character.Pv
            };

            return characterDTO;
        }

        [Route("SaveCharacter/{firstName}/{lastName}/{bravoury}/{crazyness}/{pv}/{characterType_id}/{house_id}")]
        [HttpPost]
        public void SaveCharacter(String firstName, String lastName, int bravoury, int crazyness, int pv, int characterType_id, int house_id)
        {
            businessManager.AddCharacter(firstName, lastName, bravoury, crazyness, pv, characterType_id, house_id);

        }

        [Route("UpdateCharacter/{idCharacter}/{firstName}/{lastName}/{bravoury}/{crazyness}/{pv}/{characterType_id}/{house_id}")]
        [HttpPut]
        public void UpdateCharacter(int idCharacter, String firstName, String lastName, int bravoury, int crazyness, int pv, int characterType_id, int house_id)
        {
            businessManager.UpdateCharacter(idCharacter, firstName, lastName, bravoury, crazyness, pv, characterType_id, house_id);

        }

        [Route("DeleteCharacter/{idCharacter}")]
        [HttpDelete]
        public void DeleteCharacter(int idCharacter)
        {
            businessManager.DeleteCharacter(idCharacter);

        }
    }
}
