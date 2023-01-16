using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia
        [HttpGet]
        public ActionResult GetAll()
        {
            Negocio.Materia materia = new Negocio.Materia();
            Negocio.Result result = new Negocio.Result();
            //result = Negocio.Materia.GetAll();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                    var request = client.GetAsync("Materia");
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
                                Negocio.Materia resultItemList = JsonConvert.DeserializeObject<Negocio.Materia>(resultItem.ToString());
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
                materia.Materias = result.Objects;
            }
            else
            {
                ViewBag.Message = "Algo Ocurrio" + result.ErrorMessage;
            }
            return View(materia);
        }
        [HttpGet]
        public ActionResult Form(int? IdMateria)
        {
            Negocio.Materia materia = new Negocio.Materia();
            Negocio.Result result = new Negocio.Result();
            if (IdMateria == null)
            {
                return View();
            }
            else
            {
                //result = Negocio.Materia.GetById(IdMateria.Value);
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                        var request = client.GetAsync("Materia/" + IdMateria.Value);
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
                                Negocio.Materia resultItemList = JsonConvert.DeserializeObject<Negocio.Materia>(result.Object.ToString());
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
                    materia = (Negocio.Materia)result.Object;
                }
                else
                {
                    ViewBag.Message = "Algo Ocurrio" + result.ErrorMessage;
                }
            }
            return View(materia);
        }
        [HttpPost]
        public ActionResult Form(Negocio.Materia materia)
        {
            Negocio.Result result = new Negocio.Result();

            char[] ValidacionNombreMateria = ConfigurationManager.AppSettings["ValidacionNombreMateria"].ToCharArray();
            // &<>/
            foreach (char caracter in ValidacionNombreMateria)
            {
                materia.Nombre = materia.Nombre.Replace(caracter.ToString(), "");
            }
            HttpContent content = new StringContent(JsonConvert.SerializeObject(materia), Encoding.UTF8, "application/json");
            if (materia.IdMateria == 0)
            {
                //result = Negocio.Materia.Add(materia);
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                        var request = client.PostAsync("Materia/Add", content);
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
                //result = Negocio.Materia.Update(materia);
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                        var request = client.PutAsync("Materia/update", content);
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
        public ActionResult Delete(int? IdMateria)
        {
            Negocio.Result result = new Negocio.Result();

            if (IdMateria == null)
            {
                ViewBag.Message = "Algo Ocurrio";
                return PartialView("Modal");
            }
            else
            {
                //result = Negocio.Materia.Delete(IdMateria.Value);
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                        var request = client.DeleteAsync("Materia/Delete/" + IdMateria.Value);
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
    }
}