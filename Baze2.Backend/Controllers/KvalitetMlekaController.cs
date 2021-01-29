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
    public class KvalitetMlekaController : ControllerBase
    {
        private Broker broker = new Broker();

        // GET: api/<KvalitetMlekaController>
        [HttpGet]
        public IActionResult Get()
        {
            List<KvalitetMleka> kvalitetMleka = new List<KvalitetMleka>();

            broker.OtvoriKonekciju();

            var command = new NpgsqlCommand(@"SELECT * FROM ""KvalitetMleka"";", broker.connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            KvalitetMleka km = new KvalitetMleka();
            while (reader.Read())
            {
                km = new KvalitetMleka();
                km.datumKvaliteta = Convert.ToDateTime(reader[0].ToString());
                km.rbParametra = Convert.ToInt32(reader[1]);
                km.rbPodatakaOMuzi = Convert.ToInt32(reader[2]);
                km.idZivotinje = reader[3].ToString();
                km.datumPodaciKvaliteta = Convert.ToDateTime(reader[4].ToString());
                km.kolicinaMleka = Convert.ToInt32(reader[5]);
                kvalitetMleka.Add(km);
            }
            broker.ZatvoriKonekciju();

            return Ok(kvalitetMleka);
        }

        // GET api/<KvalitetMlekaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(DateTime id)
        {
            broker.OtvoriKonekciju();
            var command = new NpgsqlCommand(@"SELECT * FROM ""KvalitetMleka"" WHERE ""datumKvaliteta"" = '" + id + "';", broker.connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            KvalitetMleka km = new KvalitetMleka();
            while (reader.Read())
            {
                km.datumKvaliteta = Convert.ToDateTime(reader[0].ToString());
                km.rbParametra = Convert.ToInt32(reader[1]);
                km.rbPodatakaOMuzi = Convert.ToInt32(reader[2]);
                km.idZivotinje = reader[3].ToString();
                km.datumPodaciKvaliteta = Convert.ToDateTime(reader[4].ToString());
                km.kolicinaMleka = Convert.ToInt32(reader[5]);
            }
            broker.ZatvoriKonekciju();

            return Ok(km);
        }

        // POST api/<KvalitetMlekaController>
        [HttpPost]
        public IActionResult Post([FromBody] KvalitetMleka kvalitetMleka)
        {
            broker.OtvoriKonekciju();
            try
            {
                var command = new NpgsqlCommand($"INSERT INTO \"KvalitetMleka\"(\"datumKvaliteta\", \"rbParametra\", \"rbPodatakaOMuzi\", \"idZivotinje\", \"datumPodaciKvaliteta\")" +
                    $"VALUES('{kvalitetMleka.datumKvaliteta}', '{kvalitetMleka.rbParametra}', '{kvalitetMleka.rbPodatakaOMuzi}', '{kvalitetMleka.idZivotinje}', '{kvalitetMleka.datumPodaciKvaliteta}');", broker.connection);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                return Ok(ex.Message);
            }
            broker.ZatvoriKonekciju();

            return Ok(kvalitetMleka);
        }

        // PUT api/<KvalitetMlekaController>/5
        [HttpPut]
        public IActionResult Put([FromBody] KvalitetMleka kvalitetMleka)
        {
            broker.OtvoriKonekciju();
            try
            {
                var command = new NpgsqlCommand($"UPDATE \"KvalitetMleka\" SET \"datumKvaliteta\" = '{kvalitetMleka.datumKvaliteta}', " +
                                                                        $"\"rbParametra\" = '{kvalitetMleka.rbParametra}', " +
                                                                        $"\"rbPodatakaOMuzi\" = '{kvalitetMleka.rbPodatakaOMuzi}', " +
                                                                        $"\"idZivotinje\" = '{kvalitetMleka.idZivotinje}', " +
                                                                        $"\"datumPodaciKvaliteta\" = '{kvalitetMleka.datumPodaciKvaliteta}', " +
                                                                        $"\"kolicinaMleka\" = '{kvalitetMleka.kolicinaMleka}' " +
                                                                    $"WHERE \"datumKvaliteta\" = '{kvalitetMleka.datumKvaliteta}';", broker.connection);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                return Ok(ex.Message);
            }
            broker.ZatvoriKonekciju();

            return Ok();
        }

        // DELETE api/<KvalitetMlekaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
