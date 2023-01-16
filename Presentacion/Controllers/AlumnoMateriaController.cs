using Negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Presentacion.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        // GET: AlumnoMateria
        [HttpGet]
        public ActionResult GetAll()
        {
            Negocio.Alumno alumno = new Negocio.Alumno();
            Negocio.Result result = new Negocio.Result();
            //Negocio.Result result = Negocio.Alumno.GetAll();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                    var request = client.GetAsync("AlumnoMateria");
                    request.Wait();

                    var resultServices = request.Result;

                    if (resultServices.IsSuccessStatusCode)
                    {
                        var readTask = resultServices.Content.ReadAsStringAsync().Result;
                        Negocio.Result resultAPI = JsonConvert.DeserializeObject<Negocio.Result>(readTask);

                        result.ErrorMessage = resultAPI.ErrorMessage;
                        result.Correct = resultAPI.Correct;
                        if (resultAPI.Correct)
                        {
                            result.Objects = new List<object>();
                            foreach (var resultItem in resultAPI.Objects)
                            {
                                Negocio.Alumno resultItemList = JsonConvert.DeserializeObject<Negocio.Alumno>(resultItem.ToString());
                                result.Objects.Add(resultItemList);
                            }
                        }

                        result.Correct = true;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Ocurrio un error" + result.Ex;
            }
            if (result.Correct)
            {
                alumno.Alumnos = result.Objects;
            }
            else
            {
                ViewBag.Message = "Algo Ocurrio" + result.ErrorMessage;
            }
            return View(alumno);
        }
        [HttpGet]
        public ActionResult GetAllMateria(int IdAlumno)
        {
            Negocio.AlumnoMateria alumnoMateria = new Negocio.AlumnoMateria();
            alumnoMateria.Alumno = new Negocio.Alumno();
            Negocio.Result result = new Negocio.Result();
            Negocio.Result resultAlumno = new Negocio.Result();
            alumnoMateria.total = 0;
            //Negocio.Result result = Negocio.AlumnoMateria.MateriasAsignadas(IdAlumno);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                    var request = client.GetAsync("AlumnoMateria/Asignadas/" + IdAlumno);
                    request.Wait();

                    var resultServices = request.Result;

                    if (resultServices.IsSuccessStatusCode)
                    {
                        var readTask = resultServices.Content.ReadAsStringAsync().Result;
                        Negocio.Result resultAPI = JsonConvert.DeserializeObject<Negocio.Result>(readTask);

                        result.ErrorMessage = resultAPI.ErrorMessage;
                        result.Correct = resultAPI.Correct;
                        if (resultAPI.Correct)
                        {
                            result.Objects = new List<object>();
                            foreach (var resultItem in resultAPI.Objects)
                            {
                                Negocio.AlumnoMateria resultItemList = JsonConvert.DeserializeObject<Negocio.AlumnoMateria>(resultItem.ToString());
                                result.Objects.Add(resultItemList);
                            }
                        }

                        result.Correct = true;
                    }

                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Ocurrio un error" + result.Ex;
            }
            //Negocio.Result resultAlumno = Negocio.Alumno.GetById(IdAlumno);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                    var request = client.GetAsync("Alumno/" + IdAlumno);
                    request.Wait();

                    var resultServices = request.Result;

                    if (resultServices.IsSuccessStatusCode)
                    {
                        var readTask = resultServices.Content.ReadAsStringAsync().Result;
                        Negocio.Result resultAPI = JsonConvert.DeserializeObject<Negocio.Result>(readTask);

                        resultAlumno.ErrorMessage = resultAPI.ErrorMessage;
                        resultAlumno.Correct = resultAPI.Correct;
                        resultAlumno.Object = resultAPI.Object;
                        if (resultAPI.Correct)
                        {
                            Negocio.Alumno resultItemList = JsonConvert.DeserializeObject<Negocio.Alumno>(resultAlumno.Object.ToString());
                            resultAlumno.Object = resultItemList;
                        }

                        resultAlumno.Correct = true;
                    }

                }
            }
            catch (Exception ex)
            {
                resultAlumno.Correct = false;
                resultAlumno.Ex = ex;
                resultAlumno.ErrorMessage = "Ocurrio un error" + result.Ex;
            }
            if (result.Correct || resultAlumno.Correct)
            {
                alumnoMateria.AlumnoMaterias = result.Objects;
                alumnoMateria.Alumno = (Negocio.Alumno)resultAlumno.Object;
                if (alumnoMateria.AlumnoMaterias != null)
                {
                    foreach (Negocio.AlumnoMateria alumMat in alumnoMateria.AlumnoMaterias)
                    {
                        alumnoMateria.total = alumnoMateria.total + (int)alumMat.Materia.Costo;
                    }
                }
            }
            else
            {
                ViewBag.Message = "Algo Ocurrio" + result.ErrorMessage;
            }
            return View(alumnoMateria);
        }
        [HttpGet]
        public ActionResult GetAllMaterias(int IdAlumno)
        {
            Negocio.AlumnoMateria alumnoMateria = new Negocio.AlumnoMateria();
            alumnoMateria.Alumno = new Negocio.Alumno();
            Negocio.Result result = new Negocio.Result();
            Negocio.Result resultAlumno = new Negocio.Result();

            //Negocio.Result result = Negocio.AlumnoMateria.MateriasNoAsignadas(IdAlumno);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                    var request = client.GetAsync("AlumnoMateria/NoAsignadas/" + IdAlumno);
                    request.Wait();

                    var resultServices = request.Result;

                    if (resultServices.IsSuccessStatusCode)
                    {
                        var readTask = resultServices.Content.ReadAsStringAsync().Result;
                        Negocio.Result resultAPI = JsonConvert.DeserializeObject<Negocio.Result>(readTask);

                        result.ErrorMessage = resultAPI.ErrorMessage;
                        result.Correct = resultAPI.Correct;
                        if (resultAPI.Correct)
                        {
                            result.Objects = new List<object>();
                            foreach (var resultItem in resultAPI.Objects)
                            {
                                Negocio.AlumnoMateria resultItemList = JsonConvert.DeserializeObject<Negocio.AlumnoMateria>(resultItem.ToString());
                                result.Objects.Add(resultItemList);
                            }
                        }

                        result.Correct = true;
                    }

                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Ocurrio un error" + result.Ex;
            }
            //Negocio.Result resultAlumno = Negocio.Alumno.GetById(IdAlumno);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                    var request = client.GetAsync("Alumno/" + IdAlumno);
                    request.Wait();

                    var resultServices = request.Result;

                    if (resultServices.IsSuccessStatusCode)
                    {
                        var readTask = resultServices.Content.ReadAsStringAsync().Result;
                        Negocio.Result resultAPI = JsonConvert.DeserializeObject<Negocio.Result>(readTask);

                        resultAlumno.ErrorMessage = resultAPI.ErrorMessage;
                        resultAlumno.Correct = resultAPI.Correct;
                        resultAlumno.Object = resultAPI.Object;
                        if (resultAPI.Correct)
                        {
                            Negocio.Alumno resultItemList = JsonConvert.DeserializeObject<Negocio.Alumno>(resultAlumno.Object.ToString());
                            resultAlumno.Object = resultItemList;
                        }

                        result.Correct = true;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Ocurrio un error" + result.Ex;
            }
            if (result.Correct)
            {
                alumnoMateria.AlumnoMaterias = result.Objects;
                alumnoMateria.Alumno = (Negocio.Alumno)resultAlumno.Object;
            }
            else
            {
                ViewBag.Message = "Algo Ocurrio" + result.ErrorMessage;
            }
            return View(alumnoMateria);
        }
        [HttpPost]
        public ActionResult AgregarMaterias(Negocio.AlumnoMateria alumnoMateria)
        {
            Negocio.Result result = new Negocio.Result();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(alumnoMateria), Encoding.UTF8, "application/json");
            if (alumnoMateria.AlumnoMaterias != null)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                        var request = client.PostAsync("AlumnoMateria/Add", content);
                        request.Wait();

                        var resultServices = request.Result;

                        if (resultServices.IsSuccessStatusCode)
                        {
                            var readTask = resultServices.Content.ReadAsStringAsync().Result;
                            Negocio.Result resultAPI = JsonConvert.DeserializeObject<Negocio.Result>(readTask);

                            result.ErrorMessage = resultAPI.ErrorMessage;
                            result.Correct = resultAPI.Correct;
                        }
                        result.Correct = true;
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.Ex = ex;
                    result.ErrorMessage = "Ocurrio un error" + result.Ex;
                }
                //foreach (string IdMateria in alumnoMateria.AlumnoMaterias)
                //{
                //    Negocio.AlumnoMateria AlumMatItems = new Negocio.AlumnoMateria();
                //    AlumMatItems.Alumno = new Negocio.Alumno();
                //    AlumMatItems.Alumno.IdAlumno = alumnoMateria.Alumno.IdAlumno;

                //    AlumMatItems.Materia = new Negocio.Materia();
                //    AlumMatItems.Materia.IdMateria = int.Parse(IdMateria);

                //    Negocio.Result resultAdd = Negocio.AlumnoMateria.Add(AlumMatItems);

                //}
                result.Correct = true;
                ViewBag.Message = "Se han agregado nuevas materias al alumno";
                ViewBag.Agregado = true;
                ViewBag.IdAlumno = alumnoMateria.Alumno.IdAlumno;
            }
            else
            {
                result.Correct = false;
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Delete(int IdAlumno, int IdMateria)
        {
            Negocio.Result result = new Negocio.Result();
            //result = Negocio.AlumnoMateria.Delete(IdAlumno,IdMateria);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                    var request = client.DeleteAsync("AlumnoMateria/Delete/" + IdAlumno + "/" + IdMateria);
                    request.Wait();

                    var resultServices = request.Result;

                    if (resultServices.IsSuccessStatusCode)
                    {
                        var readTask = resultServices.Content.ReadAsStringAsync().Result;
                        Negocio.Result resultAPI = JsonConvert.DeserializeObject<Negocio.Result>(readTask);

                        result.ErrorMessage = resultAPI.ErrorMessage;
                        result.Correct = resultAPI.Correct;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "Ocurrio un error" + result.Ex;
            }
            if (result.Correct)
            {
                ViewBag.Message = "Se elimino correctamente";
                ViewBag.Agregado = true;
                ViewBag.IdAlumno = IdAlumno;
            }
            else
            {
                ViewBag.Message = "Algo ocurrio" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Negocio.Alumno alumno)
        {
            Negocio.Result result = new Negocio.Result();
            result = Negocio.Alumno.Login(alumno);

            if (result.Correct)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Contraseña y usuario incorrecto";
                return PartialView("ModalLogin");
            }
        }
    }
}