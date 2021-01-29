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
    public class MlekaraController : ControllerBase
    {
        private Broker broker = new Broker();

        // GET: api/<MlekaraController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Mlekara> mlekare = new List<Mlekara>();

            broker.OtvoriKonekciju();

            var command = new NpgsqlCommand(@"SELECT * FROM ""Mlekara"";", broker.connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            Mlekara m = new Mlekara();
            while (reader.Read())
            {
                m = new Mlekara();
                m.SifraMlekare = reader[0].ToString();
                m.Pib = reader[1].ToString();
                m.NazivMlekare = reader[2].ToString();
                m.MaticniBroj = reader[3].ToString();
                m.PttMesta = reader[4].ToString();
                mlekare.Add(m);
            }
            broker.ZatvoriKonekciju();

            return Ok(mlekare);
        }

        // GET api/<MlekaraController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            broker.OtvoriKonekciju();
            var command = new NpgsqlCommand(@"SELECT * FROM ""Mlekara"" WHERE ""sifraMlekare"" = '" + id + "';", broker.connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            Mlekara mlekara = new Mlekara();
            while (reader.Read())
            {
                mlekara.SifraMlekare = reader[0].ToString();
                mlekara.Pib = reader[1].ToString();
                mlekara.NazivMlekare = reader[2].ToString();
                mlekara.MaticniBroj = reader[3].ToString();
                mlekara.PttMesta = reader[4].ToString();
            }
            broker.ZatvoriKonekciju();

            return Ok(mlekara);
        }

        // POST api/<MlekaraController>
        [HttpPost]
        public IActionResult Post([FromBody] Mlekara mlekara)
        {
            broker.OtvoriKonekciju();
            try
            {
                var command = new NpgsqlCommand($"INSERT INTO \"Mlekara\"(\"sifraMlekare\", pib, \"nazivMlekare\", \"maticniBroj\", \"pttMesta\")" +
                    $"VALUES('{mlekara.SifraMlekare}', '{mlekara.Pib}', '{mlekara.NazivMlekare}', '{mlekara.MaticniBroj}', '{mlekara.PttMesta}');", broker.connection);
                command.ExecuteNonQuery();
            }
            catch(NpgsqlException ex)
            {
                return Ok(ex.Message);
            }
            broker.ZatvoriKonekciju();

            return Ok(mlekara);
        }

        // PUT api/<MlekaraController>
        [HttpPut]
        public IActionResult Put([FromBody] Mlekara mlekara)
        {
            broker.OtvoriKonekciju();
            try
            {
                var command = new NpgsqlCommand($"UPDATE \"Mlekara\" SET \"sifraMlekare\" = '{mlekara.SifraMlekare}', " +
                                                                        $"pib = '{mlekara.Pib}', " +
                                                                        $"\"nazivMlekare\" = '{mlekara.NazivMlekare}', " +
                                                                        $"\"maticniBroj\" = '{mlekara.MaticniBroj}', " +
                                                                        $"\"pttMesta\" = '{mlekara.PttMesta}' " +
                                                                    $"WHERE \"sifraMlekare\" = '{mlekara.SifraMlekare}';", broker.connection);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                return Ok(ex.Message);
            }
            broker.ZatvoriKonekciju();

            return Ok();
        }

        // DELETE api/<MlekaraController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            broker.OtvoriKonekciju();
            var command = new NpgsqlCommand($"DELETE FROM \"Mlekara\" WHERE \"sifraMlekare\" = '{id}';", broker.connection);
            command.ExecuteNonQuery();
            broker.ZatvoriKonekciju();

            return Ok();
        }
    }
}
