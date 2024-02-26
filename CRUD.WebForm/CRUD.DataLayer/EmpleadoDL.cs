using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CRUD.EntityLayer;

namespace CRUD.DataLayer
{
    public class EmpleadoDL
    {
        public List<Empleado> Lista()
        {
            List<Empleado> lista = new List<Empleado>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("select * from fn_empleados()", oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Empleado
                            {
                                IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Departamento = new Departamento
                                {
                                    IdDepartamento = Convert.ToInt32(dr["IdDepartamento"]),
                                    Nombre = dr["Nombre"].ToString()
                                },
                                Sueldo = (decimal)dr["Sueldo"],
                                FechaContrato = dr["FechaContrato"].ToString(),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return lista;
        }

        public Empleado Obtener(int IdEmpleado)
        {
            Empleado entidad = new Empleado();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("select * from fn_empleado(@idEmpleado)", oConexion);
                cmd.Parameters.AddWithValue("@idEmpleado", IdEmpleado);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            entidad.IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]);
                            entidad.NombreCompleto = dr["NombreCompleto"].ToString();
                            entidad.Departamento = new Departamento
                            {
                                IdDepartamento = Convert.ToInt32(dr["IdDepartamento"]),
                                Nombre = dr["Nombre"].ToString()
                            };
                            entidad.Sueldo = (decimal)dr["Sueldo"];
                            entidad.FechaContrato = dr["FechaContrato"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return entidad;
        }

        public bool Crear(Empleado entidad)
        {
            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_CrearEmpleado", oConexion);
                cmd.Parameters.AddWithValue("@NombreCompleto", entidad.NombreCompleto);
                cmd.Parameters.AddWithValue("@IdDepartamento", entidad.Departamento.IdDepartamento);
                cmd.Parameters.AddWithValue("@Sueldo", entidad.Sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", entidad.FechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    respuesta = filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return respuesta;
        }

        public bool Editar(Empleado entidad)
        {
            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_EditarEmpleado", oConexion);
                cmd.Parameters.AddWithValue("@IdEmpleado", entidad.IdEmpleado);
                cmd.Parameters.AddWithValue("@NombreCompleto", entidad.NombreCompleto);
                cmd.Parameters.AddWithValue("@IdDepartamento", entidad.Departamento.IdDepartamento);
                cmd.Parameters.AddWithValue("@Sueldo", entidad.Sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", entidad.FechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    respuesta = filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return respuesta;
        }

        public bool Eliminar(int IdEmpleado)
        {
            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarEmpleado", oConexion);
                cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    respuesta = filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return respuesta;
        }
    }
}
