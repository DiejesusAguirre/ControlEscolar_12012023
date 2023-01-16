using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        [DisplayName("Nombre: ")]
        public string Nombre { get; set; }
        [DisplayName("Apellido Paterno: ")]
        public string ApellidoPaterno { get; set; }
        [DisplayName("Apellido Materno: ")]
        public string ApellidoMaterno { get; set; }
        [DisplayName("Foto")]
        public string Foto { get; set; }
        public List<object> Alumnos { get; set; }


        //Metodos con SqlClient
        public static Negocio.Result GetAll()
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AlumnoGetAll";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        DataTable tablaAlumno = new DataTable();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                        //llenamos la tabla
                        adapter.Fill(tablaAlumno);
                        //si la tabla tiene filas
                        if (tablaAlumno.Rows.Count > 0)
                        {
                            cmd.Connection.Open();
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaAlumno.Rows)
                            {
                                Negocio.Alumno alumno = new Negocio.Alumno();
                                alumno.IdAlumno = int.Parse(row[0].ToString());
                                alumno.Nombre = row[1].ToString();
                                alumno.ApellidoPaterno = row[2].ToString();
                                alumno.ApellidoMaterno = row[3].ToString();
                                alumno.Foto = row[4].ToString();

                                result.Objects.Add(alumno);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static Negocio.Result GetById(int IdAlumno)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AlumnoGetById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("@IdAlumno", SqlDbType.Int);
                        collection[0].Value = IdAlumno;


                        //Ir llenando con los parametros
                        cmd.Parameters.AddRange(collection);

                        DataTable tablaAlumno = new DataTable();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);



                        adapter.SelectCommand = cmd;
                        adapter.Fill(tablaAlumno);


                        if (tablaAlumno.Rows.Count > 0)
                        {
                            DataRow row = tablaAlumno.Rows[0];


                            Negocio.Alumno alumno = new Negocio.Alumno();

                            alumno.IdAlumno = int.Parse(row[0].ToString());
                            alumno.Nombre = row[1].ToString();
                            alumno.ApellidoPaterno = row[2].ToString();
                            alumno.ApellidoMaterno = row[3].ToString();
                            alumno.Foto = row[4].ToString();

                            result.Object = alumno;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static Negocio.Result Add(Negocio.Alumno alumno)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AlumnoAdd";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        SqlParameter[] collection = new SqlParameter[4];
                        collection[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                        collection[0].Value = alumno.Nombre;
                        collection[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = alumno.ApellidoPaterno;
                        collection[2] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
                        collection[2].Value = alumno.ApellidoMaterno;
                        collection[3] = new SqlParameter("@Foto", SqlDbType.VarChar);
                        collection[3].Value = alumno.Foto;

                        //Ir llenando con los parametros
                        cmd.Parameters.AddRange(collection);
                        //Abrir la conexion
                        cmd.Connection.Open();

                        int rowAffected = cmd.ExecuteNonQuery();

                        if (rowAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static Negocio.Result Update(Negocio.Alumno alumno)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AlumnoUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        SqlParameter[] collection = new SqlParameter[5];
                        collection[0] = new SqlParameter("@IdAlumno", SqlDbType.Int);
                        collection[0].Value = alumno.IdAlumno;
                        collection[1] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                        collection[1].Value = alumno.Nombre;
                        collection[2] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                        collection[2].Value = alumno.ApellidoPaterno;
                        collection[3] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
                        collection[3].Value = alumno.ApellidoMaterno;
                        collection[4] = new SqlParameter("@Foto", SqlDbType.VarChar);
                        collection[4].Value = alumno.Foto;

                        //Ir llenando con los parametros
                        cmd.Parameters.AddRange(collection);
                        //Abrir la conexion
                        cmd.Connection.Open();

                        int rowAffected = cmd.ExecuteNonQuery();

                        if (rowAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static Negocio.Result Delete(int IdAlumno)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AlumnoDelete";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("@IdAlumno", SqlDbType.Int);
                        collection[0].Value = IdAlumno;

                        //Ir llenando con los parametros
                        cmd.Parameters.AddRange(collection);
                        //Abrir la conexion
                        cmd.Connection.Open();

                        int rowAffected = cmd.ExecuteNonQuery();

                        if (rowAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static Negocio.Result Login(Negocio.Alumno alumno)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "LoginAlumno";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        SqlParameter[] collection = new SqlParameter[2];

                        collection[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                        collection[0].Value = alumno.Nombre;
                        collection[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = alumno.ApellidoPaterno;


                        //Ir llenando con los parametros
                        cmd.Parameters.AddRange(collection);

                        DataTable tablaAlumno = new DataTable();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);



                        adapter.SelectCommand = cmd;
                        adapter.Fill(tablaAlumno);


                        if (tablaAlumno.Rows.Count > 0)
                        {
                            DataRow row = tablaAlumno.Rows[0];
                            Negocio.Alumno resultAlumno = new Negocio.Alumno();
                            resultAlumno.Nombre = row[0].ToString();
                            resultAlumno.ApellidoPaterno = row[1].ToString();
                            resultAlumno.ApellidoMaterno = row[2].ToString();

                            result.Object = resultAlumno;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
