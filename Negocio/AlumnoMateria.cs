using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Negocio
{
    public class AlumnoMateria
    {
        public int IdAlumnoMateria { get; set; }
        public int IdAlumno { get; set; }
        [DisplayName("Costo Total de las Materia")]
        public int total { get; set; }
        public int IdMateria { get; set; }
        public Negocio.Alumno Alumno { get; set; }
        public Negocio.Materia Materia { get; set; }
        public List<object> AlumnoMaterias { get; set; }


        //Metodos con SqlClient de las Materias de un Alumno
        public static Negocio.Result MateriasAsignadas(int IdAlumno)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "MateriaGetByIdAlumnoMateria";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("@IdAlumno", SqlDbType.Int);
                        collection[0].Value = IdAlumno;


                        //Ir llenando con los parametros
                        cmd.Parameters.AddRange(collection);

                        DataTable tablaAlumnoMateria = new DataTable();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                        //llenamos la tabla
                        adapter.Fill(tablaAlumnoMateria);
                        //si la tabla tiene filas
                        if (tablaAlumnoMateria.Rows.Count > 0)
                        {
                            cmd.Connection.Open();
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaAlumnoMateria.Rows)
                            {
                                Negocio.AlumnoMateria alumnoMateria = new Negocio.AlumnoMateria();
                                alumnoMateria.Alumno = new Negocio.Alumno();
                                alumnoMateria.Materia = new Negocio.Materia();

                                alumnoMateria.IdAlumnoMateria = int.Parse(row[0].ToString());
                                alumnoMateria.IdAlumno = int.Parse(row[1].ToString());
                                alumnoMateria.IdMateria = int.Parse(row[2].ToString());
                                alumnoMateria.Alumno.Nombre = row[3].ToString();
                                alumnoMateria.Alumno.ApellidoPaterno = row[4].ToString();
                                alumnoMateria.Alumno.ApellidoMaterno = row[5].ToString();
                                alumnoMateria.Alumno.Foto = row[6].ToString();
                                alumnoMateria.Materia.IdMateria = int.Parse(row[7].ToString());
                                alumnoMateria.Materia.Nombre = row[8].ToString();
                                alumnoMateria.Materia.Costo = decimal.Parse(row[9].ToString());

                                result.Objects.Add(alumnoMateria);
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
        public static Negocio.Result MateriasNoAsignadas(int IdAlumno)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "MateriasNoAsignadas";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("@IdAlumno", SqlDbType.Int);
                        collection[0].Value = IdAlumno;


                        //Ir llenando con los parametros
                        cmd.Parameters.AddRange(collection);

                        DataTable tablaAlumnoMateria = new DataTable();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                        //llenamos la tabla
                        adapter.Fill(tablaAlumnoMateria);
                        //si la tabla tiene filas
                        if (tablaAlumnoMateria.Rows.Count > 0)
                        {
                            cmd.Connection.Open();
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaAlumnoMateria.Rows)
                            {
                                Negocio.AlumnoMateria alumnoMateria = new Negocio.AlumnoMateria();
                                alumnoMateria.Alumno = new Negocio.Alumno();
                                alumnoMateria.Materia = new Negocio.Materia();

                                alumnoMateria.IdMateria = int.Parse(row[0].ToString());
                                alumnoMateria.Materia.Nombre = row[1].ToString();
                                alumnoMateria.Materia.Costo = decimal.Parse(row[2].ToString());

                                result.Objects.Add(alumnoMateria);
                            }
                            result.Correct = true;
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
        public static Negocio.Result Add(Negocio.AlumnoMateria alumnoMateria)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AlumnoMateriaAdd";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        SqlParameter[] collection = new SqlParameter[2];
                        collection[0] = new SqlParameter("@IdAlumno", SqlDbType.Int);
                        collection[0].Value = alumnoMateria.Alumno.IdAlumno;
                        collection[1] = new SqlParameter("@IdMateria", SqlDbType.Int);
                        collection[1].Value = alumnoMateria.Materia.IdMateria;

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
        public static Negocio.Result Delete(int IdAlumno, int IdMateria)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        
                        cmd.CommandText = "AlumnoMateriaDelete";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        SqlParameter[] collection = new SqlParameter[2];
                        collection[0] = new SqlParameter("@IdAlumno", SqlDbType.Int);
                        collection[0].Value = IdAlumno;
                        collection[1] = new SqlParameter("@IdMatera", SqlDbType.Int);
                        collection[1].Value = IdMateria;

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
    }
}
