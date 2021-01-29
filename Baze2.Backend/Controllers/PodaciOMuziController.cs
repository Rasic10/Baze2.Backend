using Baze2.Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Baze2.Backend.Controllers
{
    [EnableCors("Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class PodaciOMuziController : ControllerBase
    {
        private Broker broker = new Broker();

        // GET: api/<PodaciOMuziController>
        [HttpGet]
        public IActionResult Get()
        {
            List<PodaciOMuzi> podaciOMuzi = new List<PodaciOMuzi>();

            broker.OtvoriKonekciju();

            var command = new NpgsqlCommand(@"SELECT * FROM ""PodaciOMuzi"";", broker.connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            PodaciOMuzi pom = new PodaciOMuzi();
            while (reader.Read())
            {
                pom = new PodaciOMuzi();
                pom.RbPodatakOMuzi = Convert.ToInt32(reader[0]);
                pom.IdZivotinje = reader[1].ToString();
                pom.KolicinaMleka = Convert.ToInt32(reader[2]);
                pom.VremeMuze = Convert.ToDateTime(reader[3].ToString());
                podaciOMuzi.Add(pom);
            }
            broker.ZatvoriKonekciju();

            return Ok(podaciOMuzi);
        }

        // GET api/<PodaciOMuziController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            broker.OtvoriKonekciju();
            var command = new NpgsqlCommand(@"SELECT * FROM ""PodaciOMuzi"" WHERE ""rbPodatakaOMuzi"" = '" + id + "';", broker.connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            PodaciOMuzi pom = new PodaciOMuzi();
            while (reader.Read()) { 
                pom.RbPodatakOMuzi = Convert.ToInt32(reader[0]);
                pom.IdZivotinje = reader[1].ToString();
                pom.KolicinaMleka = Convert.ToInt32(reader[2]);
                pom.VremeMuze = Convert.ToDateTime(reader[3].ToString());
            }
            broker.ZatvoriKonekciju();

            return Ok(pom);
        }

        // POST api/<PodaciOMuziController>
        [HttpPost]
        public IActionResult Post([FromBody] PodaciOMuzi podaciOMuzi)
        {
            // TODO: call a procedure
            broker.OtvoriKonekciju();
            try
            {
                var command = new NpgsqlCommand($"INSERT INTO \"PodaciOMuzi\"(\"rbPodatakaOMuzi\", \"idZivotinje\", \"kolicinaMleka\", \"vremeMuze\")" +
                    $"VALUES('{podaciOMuzi.RbPodatakOMuzi}', '{podaciOMuzi.IdZivotinje}', '{podaciOMuzi.KolicinaMleka}', '{podaciOMuzi.VremeMuze}');", broker.connection);
                command.ExecuteNonQuery();
                var command1 = new NpgsqlCommand($"CALL procedure_odrkolmleka('{podaciOMuzi.IdZivotinje}');", broker.connection);
                command1.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                return Ok(ex.Message);
            }
            broker.ZatvoriKonekciju();

            return Ok(podaciOMuzi);
        }

        // PUT api/<PodaciOMuziController>/5
        [HttpPut]
        public IActionResult Put([FromBody] PodaciOMuzi podaciOMuzi)
        {
            broker.OtvoriKonekciju();
            try
            {
                var command = new NpgsqlCommand($"UPDATE \"PodaciOMuzi\" SET \"rbPodatakaOMuzi\" = '{podaciOMuzi.RbPodatakOMuzi}', " +
                                                                        $"\"idZivotinje\" = '{podaciOMuzi.IdZivotinje}', " +
                                                                        $"\"kolicinaMleka\" = '{podaciOMuzi.KolicinaMleka}', " +
                                                                        $"\"vremeMuze\" = '{podaciOMuzi.VremeMuze}' " +
                                                                    $"WHERE \"rbPodatakaOMuzi\" = '{podaciOMuzi.RbPodatakOMuzi}';", broker.connection);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                return Ok(ex.Message);
            }
            broker.ZatvoriKonekciju();

            return Ok();
        }

        // DELETE api/<PodaciOMuziController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            broker.OtvoriKonekciju();
            // TODO: idZivotinje iz primary key
            var command = new NpgsqlCommand($"DELETE FROM \"PodaciOMuzi\" WHERE \"rbPodatakaOMuzi\" = '{id}';", broker.connection);
            command.ExecuteNonQuery();
            broker.ZatvoriKonekciju();

            return Ok();
        }
    }
}
