using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Servicios.Controllers
{
    public class AlumnoMateriaController : ApiController
    {
        // Mostrara los Alumnos
        [System.Web.Http.Route("api/AlumnoMateria")]
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

        // Mostrara las materias asignadas a un Alumno
        [System.Web.Http.Route("api/AlumnoMateria/Asignadas/{idAlumno}")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult MateriasAsignadas(int idAlumno)
        {
            Negocio.AlumnoMateria alumnoMateria = new Negocio.AlumnoMateria();
            alumnoMateria.total = 0;
            Negocio.Result result = Negocio.AlumnoMateria.MateriasAsignadas(idAlumno);
            Negocio.Result resultAlumno = Negocio.Alumno.GetById(idAlumno);
            if (result.Correct)
            {
                alumnoMateria.AlumnoMaterias = result.Objects.ToList();
                alumnoMateria.Alumno = (Negocio.Alumno)resultAlumno.Object;
                if (alumnoMateria.AlumnoMaterias.Count > 0)
                {
                    foreach (Negocio.AlumnoMateria alumMat in alumnoMateria.AlumnoMaterias)
                    {
                        alumnoMateria.total = alumnoMateria.total + (int)alumMat.Materia.Costo;
                    }
                }
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // Mostrara las materias que no estan asignadas a un Alumno
        [System.Web.Http.Route("api/AlumnoMateria/NoAsignadas/{idAlumno}")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult MateriasNoAsignadas(int idAlumno)
        {
            Negocio.AlumnoMateria alumnoMateria = new Negocio.AlumnoMateria();
            alumnoMateria.Alumno = new Negocio.Alumno();

            Negocio.Result result = Negocio.AlumnoMateria.MateriasNoAsignadas(idAlumno);
            Negocio.Result resultAlumno = Negocio.Alumno.GetById(idAlumno);
            if (result.Correct)
            {
                alumnoMateria.AlumnoMaterias = result.Objects.ToList();
                alumnoMateria.Alumno = (Negocio.Alumno)resultAlumno.Object;
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // Agregar una materia a un alumno
        [System.Web.Http.Route("api/AlumnoMateria/Add")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Add([FromBody]Negocio.AlumnoMateria alumnoMateria)
        {
            Negocio.Result result = new Negocio.Result();
            //alumnoMateria.AlumnoMaterias = new List<object>();
            if (alumnoMateria.AlumnoMaterias.Count > 0)
            {
                foreach (var IdMateria in alumnoMateria.AlumnoMaterias)
                {
                    Negocio.AlumnoMateria AlumMatItems = new Negocio.AlumnoMateria();
                    AlumMatItems.Alumno = new Negocio.Alumno();
                    AlumMatItems.Alumno.IdAlumno = alumnoMateria.Alumno.IdAlumno;

                    AlumMatItems.Materia = new Negocio.Materia();
                    AlumMatItems.Materia.IdMateria = int.Parse(IdMateria.ToString());

                    result = Negocio.AlumnoMateria.Add(AlumMatItems);
                }
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // DELETE: api/AlumnoMateria/5
        [System.Web.Http.Route("api/AlumnoMateria/Delete/{idAlumno}/{idMateria}")]
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Delete(int idAlumno, int idMateria)
        {
            Negocio.Result result = new Negocio.Result();
            result = Negocio.AlumnoMateria.Delete(idAlumno, idMateria);
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
