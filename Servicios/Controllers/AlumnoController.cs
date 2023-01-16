using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace Servicios.Controllers
{
    public class AlumnoController : ApiController
    {
        // Mostrar todos los alumnos
        [System.Web.Http.Route("api/Alumno")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAll()
        {
            Negocio.Alumno alumno = new Negocio.Alumno();
            Negocio.Result result = Negocio.Alumno.GetAll();
            if (result.Correct)
            {
                alumno.Alumnos = result.Objects;
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        //Mostrar un alumno por medio de su ID
        [System.Web.Http.Route("api/Alumno/{idAlumno}")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetById(int idAlumno)
        {
            Negocio.Alumno alumno = new Negocio.Alumno();
            if (idAlumno.Equals(null))
            {
                return BadRequest("El campo esta vacio");
            }
            else
            {
                Negocio.Result result = Negocio.Alumno.GetById(idAlumno);
                if (result.Correct)
                {
                    alumno = (Negocio.Alumno)result.Object;
                    return Ok(result);
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, result);
                }
            }
        }

        // Añadir un Alumno
        [System.Web.Http.Route("api/Alumno/Add")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Add([FromBody]Negocio.Alumno alumno)
        {
            Negocio.Result result = Negocio.Alumno.Add(alumno);
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
        [System.Web.Http.Route("api/Alumno/update")]
        [System.Web.Http.HttpPut]
        public IHttpActionResult Update([FromBody]Negocio.Alumno alumno)
        {
            Negocio.Result result = Negocio.Alumno.Update(alumno);
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
        [System.Web.Http.Route("api/Alumno/Delete/{idAlumno}")]
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Delete(int idAlumno)
        {
            Negocio.Result result = Negocio.Alumno.Delete(idAlumno);
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
