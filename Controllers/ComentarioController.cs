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
    public class ComentarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ComentarioController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //CONSULTA
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select id,nombre_cli,imagen_cli,profesion_cli,comentario,estado
                        from 
                        comentario
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
                        delete from comentario 
                        where id=@ComentarioId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ComentarioId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }


        [HttpPut]
        public JsonResult Put(Comentario  Comen)
        {
            string query = @"
                        update comentario set 
                        nombre_cli =@ComentarioNombreCli,
                        imagen_cli =@ComentarioImagenCli,
                        profesion_cli =@ComentarioProfesionCli,
                        comentario=@Comentariocomentario,
                        estado=@ComentarioEstado
                        where id =@ComentarioId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ComentarioId", Comen.id);
                    myCommand.Parameters.AddWithValue("@ComentarioNombreCli", Comen.nombre_cli);
                    myCommand.Parameters.AddWithValue("@ComentarioImagenCli", Comen.imagen_cli);
                    myCommand.Parameters.AddWithValue("@ComentarioProfesionCli", Comen.profesion_cli);
                    myCommand.Parameters.AddWithValue("@Comentariocomentario", Comen.comentario);
                    myCommand.Parameters.AddWithValue("@ComentarioEstado", Comen.estado);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpPost]
        public JsonResult Post(Modelos.Comentario Comen)
        {
            string query = @"
                        insert into comentario 
                        (nombre_cli,imagen_cli,profesion_cli,comentario,estado) 
                        values
                         (@ComentarioNombreCli,@ComentarioImagenCli,@ComentarioProfesionCli,@Comentariocomentario,@Comentarioestado) ;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ComentarioId", Comen.id);
                    myCommand.Parameters.AddWithValue("@ComentarioNombreCli", Comen.nombre_cli);
                    myCommand.Parameters.AddWithValue("@ComentarioImagenCli", Comen.imagen_cli);
                    myCommand.Parameters.AddWithValue("@ComentarioProfesionCli", Comen.profesion_cli);
                    myCommand.Parameters.AddWithValue("@Comentariocomentario", Comen.comentario);
                    myCommand.Parameters.AddWithValue("@ComentarioEstado", "pendiente");

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
