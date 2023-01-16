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
    public class Materia
    {
        public int IdMateria { get; set; }
        [DisplayName("Nombre de la Materia")]
        public string Nombre { get; set; }
        [DisplayName("Costo de la Materia")]
        public decimal Costo { get; set; }
        public List<object> Materias { get; set; }

        //Metodos de SqlClient de Materia

        public static Negocio.Result GetAll()
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "MateriaGetAll";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        DataTable tablaMateria = new DataTable();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                        //llenamos la tabla
                        adapter.Fill(tablaMateria);
                        //si la tabla tiene filas
                        if (tablaMateria.Rows.Count > 0)
                        {
                            cmd.Connection.Open();
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaMateria.Rows)
                            {
                                Negocio.Materia materia = new Negocio.Materia();
                                materia.IdMateria = int.Parse(row[0].ToString());
                                materia.Nombre = row[1].ToString();
                                materia.Costo = decimal.Parse(row[2].ToString());

                                result.Objects.Add(materia);
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
        public static Negocio.Result GetById(int IdMateria)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "MateriaGetById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("@IdMateria", SqlDbType.Int);
                        collection[0].Value = IdMateria;


                        //Ir llenando con los parametros
                        cmd.Parameters.AddRange(collection);

                        DataTable tablaMateria = new DataTable();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);



                        adapter.SelectCommand = cmd;
                        adapter.Fill(tablaMateria);


                        if (tablaMateria.Rows.Count > 0)
                        {
                            DataRow row = tablaMateria.Rows[0];


                            Negocio.Materia materia = new Negocio.Materia();

                            materia.IdMateria = int.Parse(row[0].ToString());
                            materia.Nombre = row[1].ToString();
                            materia.Costo = decimal.Parse(row[2].ToString());

                            result.Object = materia;

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
        public static Negocio.Result Add(Negocio.Materia materia)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "MateriaAdd";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        SqlParameter[] collection = new SqlParameter[2];
                        collection[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                        collection[0].Value = materia.Nombre;
                        collection[1] = new SqlParameter("@Costo", SqlDbType.Decimal);
                        collection[1].Value = materia.Costo;

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
        public static Negocio.Result Update(Negocio.Materia materia)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "MateriaUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        SqlParameter[] collection = new SqlParameter[3];
                        collection[0] = new SqlParameter("@IdMateria", SqlDbType.Int);
                        collection[0].Value = materia.IdMateria;
                        collection[1] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                        collection[1].Value = materia.Nombre;
                        collection[2] = new SqlParameter("@Costo", SqlDbType.Decimal);
                        collection[2].Value = materia.Costo;

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
        public static Negocio.Result Delete(int IdMateria)
        {
            Negocio.Result result = new Negocio.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(AccesoDatos.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "MateriaDelete";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("@IdMateria", SqlDbType.Int);
                        collection[0].Value = IdMateria;

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
