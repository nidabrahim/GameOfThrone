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
    [RoutePrefix("api/CharacterType")]
    public class CharacterTypeController : ApiController
    {
        ThronesTournamentManager businessManager = new ThronesTournamentManager();


        [Route("GetAllCharacterType")]
        public List<CharacterTypeDTO> GetAllCharacterType()
        {
            List<CharacterTypeDTO> listCharacterType = new List<CharacterTypeDTO>();

            foreach (var characterType in businessManager.ListCharacterType())
            {
                listCharacterType.Add(new CharacterTypeDTO()
                {
                    id = characterType.IdCharacterType,
                    name = characterType.Name
                    
                });
            }

            return listCharacterType;
        }

        [Route("GetCharacterTypeById/{id:int}")]
        public CharacterTypeDTO GetCharacterTypeById(int id)
        {
            var characterType = businessManager.GetCharacterType(id);
            CharacterTypeDTO characterTypeDTO = new CharacterTypeDTO()
            {
                id = characterType.IdCharacterType,
                name = characterType.Name

            };

            return characterTypeDTO;
        }

    }
}
