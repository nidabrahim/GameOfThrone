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
                listCharacter.Add(new CharacterDTO(character));
            }

            return listCharacter;
        }

        [Route("GetHi")]
        public string GetHi() { return "hi";  }
    }
}
