using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicios.Controllers
{
    public class MateriaController : ApiController
    {
        // Mostrar todas las Materias
        [System.Web.Http.Route("api/Materia")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAll()
        {
            Negocio.Materia materia = new Negocio.Materia();
            Negocio.Result result = Negocio.Materia.GetAll();
            if (result.Correct)
            {
                materia.Materias = result.Objects.ToList();
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        //Mostrar un alumno por medio de su ID
        [System.Web.Http.Route("api/Materia/{idMateria}")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetById(int idMateria)
        {
            Negocio.Materia materia = new Negocio.Materia();
            if (idMateria.Equals(null))
            {
                return BadRequest("El campo esta vacio");
            }
            else
            {
                Negocio.Result result = Negocio.Materia.GetById(idMateria);
                if (result.Correct)
                {
                    materia = (Negocio.Materia)result.Object;
                    return Ok(result);
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, result);
                }
            }
        }

        // Añadir un Alumno
        [System.Web.Http.Route("api/Materia/Add")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Add([FromBody] Negocio.Materia materia)
        {
            Negocio.Result result = Negocio.Materia.Add(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // Actualizar un alumno
        [System.Web.Http.Route("api/Materia/update")]
        [System.Web.Http.HttpPut]
        public IHttpActionResult Update([FromBody] Negocio.Materia materia)
        {
            Negocio.Result result = Negocio.Materia.Update(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // Elimina un Alumno
        [System.Web.Http.Route("api/Materia/Delete/{idMateria}")]
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Delete(int idMateria)
        {
            Negocio.Result result = Negocio.Materia.Delete(idMateria);

            if (result.Correct)
            {
                return Ok(result);
            }

            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }
    }
}
