using Negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
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

                    var request = client.GetAsync("Alumno");
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
        public ActionResult Form(int? IdAlumno)
        {
            Negocio.Alumno alumno = new Negocio.Alumno();
            Negocio.Result result = new Negocio.Result();
            if (IdAlumno == null)
            {
                alumno.Foto = "";
                return View(alumno);
            }
            else
            {
                //Negocio.Result result = Negocio.Alumno.GetById(IdAlumno.Value);
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

                            result.ErrorMessage = resultAPI.ErrorMessage;
                            result.Correct = resultAPI.Correct;
                            result.Object = resultAPI.Object;
                            if (resultAPI.Correct)
                            {
                                Negocio.Alumno resultItemList = JsonConvert.DeserializeObject<Negocio.Alumno>(result.Object.ToString());
                                result.Object = resultItemList;
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
                    alumno = (Negocio.Alumno)result.Object;
                }
                else
                {
                    ViewBag.Message = "Algo Ocurrio" + result.ErrorMessage;
                }
            }
            return View(alumno);
        }
        [HttpPost]
        public ActionResult Form(Negocio.Alumno alumno)
        {
            HttpPostedFileBase file = Request.Files["IfImage"];
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en la propiedad de imagen en el objeto alumno
                alumno.Foto = Convert.ToBase64String(ImagenBytes);
            }
            Negocio.Result result = new Negocio.Result();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(alumno), Encoding.UTF8, "application/json");
            if (alumno.IdAlumno == 0)
            {
                //result = Negocio.Alumno.Add(alumno);
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                        var request = client.PostAsync("Alumno/Add", content);
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
                if (result.Correct)
                {
                    ViewBag.Message = "Se ha registrado correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ha ocurrido un error" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            else
            {
                //result = Negocio.Alumno.Update(alumno);
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                        var request = client.PutAsync("Alumno/update", content);
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
                if (result.Correct)
                {
                    ViewBag.Message = "Se ha actualizado correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ha ocurrido un error" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
        }
        [HttpGet]
        public ActionResult Delete(int? IdAlumno)
        {
            Negocio.Result result = new Negocio.Result();

            if (IdAlumno == null)
            {
                ViewBag.Message = "Algo Ocurrio";
                return PartialView("Modal");
            }
            else
            {
                //result = Negocio.Alumno.Delete(IdAlumno.Value);
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                        var request = client.DeleteAsync("Alumno/Delete/" + IdAlumno.Value);
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
                    return PartialView("Modal");
                }

                else
                {
                    ViewBag.Message = "Algo ocurrio" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
        }
        public static byte[] ConvertToBytes(HttpPostedFileBase Imagen)
        {
            byte[] data = null;

            System.IO.BinaryReader reader = new System.IO.BinaryReader(Imagen.InputStream);
            data = reader.ReadBytes((int)Imagen.ContentLength);

            return data;
        }
    }
}