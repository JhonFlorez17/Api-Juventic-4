using JuventicApiReto4.Modelos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace JuventicApiReto4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EmpleadoController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //CONSULTA
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select id,nombre,cargo,imagen
                        from 
                        empleado
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }


        //ELIMINACION
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        delete from empleado 
                        where id=@EmpleadoId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@EmpleadoId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }


        [HttpPut]
        public JsonResult Put(Empleado Emple)
        {
            string query = @"
                        update empleado set 
                        nombre =@EmpleadoNombre,
                        cargo =@EmpleadoCargo,
                        imagen =@EmpleadoImagen
                        where id =@EmpleadoId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@EmpleadoId", Emple.id);
                    myCommand.Parameters.AddWithValue("@EmpleadoNombre", Emple.nombre);
                    myCommand.Parameters.AddWithValue("@EmpleadoCargo", Emple.cargo);
                    myCommand.Parameters.AddWithValue("@EmpleadoImagen", Emple.imagen);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpPost]
        public JsonResult Post(Modelos.Empleado Emple)
        {
            string query = @"
                        insert into empleado 
                        (nombre,cargo,imagen) 
                        values
                         (@EmpleadoNombre,@EmpleadoCargo,@EmpleadoImagen) ;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@EmpleadoId", Emple.id);
                    myCommand.Parameters.AddWithValue("@EmpleadoNombre", Emple.nombre);
                    myCommand.Parameters.AddWithValue("@EmpleadoCargo", Emple.cargo);
                    myCommand.Parameters.AddWithValue("@EmpleadoImagen", Emple.imagen);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }
    }
}
