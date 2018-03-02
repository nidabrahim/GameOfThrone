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
    [RoutePrefix("api/RelationType")]
    public class RelationTypeController : ApiController
    {
        ThronesTournamentManager businessManager = new ThronesTournamentManager();

        [Route("GetAllRelationType")]
        public List<RelationTypeDTO> GetAllRelationType()
        {
            List<RelationTypeDTO> listRelationType = new List<RelationTypeDTO>();

            foreach (var relationType in businessManager.ListRelationType())
            {
                listRelationType.Add(new RelationTypeDTO()
                {
                    id = relationType.IdRelationType,
                    name = relationType.Name

                });
            }

            return listRelationType;
        }

        [Route("GetRelationTypeById/{id:int}")]
        public RelationTypeDTO GetRelationTypeById(int id)
        {
            var relationType = businessManager.GetRelationType(id);

            RelationTypeDTO relationTypeDTO = new RelationTypeDTO() {
                id = relationType.IdRelationType,
                name = relationType.Name
            };

            return relationTypeDTO;
        }
    }
}
