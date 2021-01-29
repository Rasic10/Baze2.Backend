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
    public class ObracunController : ControllerBase
    {
        private Broker broker = new Broker();

        // GET: api/<ObracunController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Obracun> obracun = new List<Obracun>();

            broker.OtvoriKonekciju();

            var command = new NpgsqlCommand(@"SELECT * FROM ""Obracun"";", broker.connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            Obracun o = new Obracun();
            while (reader.Read())
            {
                o = new Obracun();
                o.SifraObracuna = reader[0].ToString();
                o.DatumObracuna = Convert.ToDateTime(reader[1].ToString());
                o.PeriodOd = reader[2].ToString();
                o.PeriodDo = reader[3].ToString();
                o.Litara = Convert.ToInt32(reader[4]);
                o.SifraMlekare = reader[5].ToString();
                o.NazivMlekare = reader[6].ToString();
                obracun.Add(o);
            }
            broker.ZatvoriKonekciju();

            return Ok(obracun);
        }

        // GET api/<ObracunController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            broker.OtvoriKonekciju();
            var command = new NpgsqlCommand(@"SELECT * FROM ""Obracun"" WHERE ""sifraObracuna"" = '" + id + "';", broker.connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            Obracun o = new Obracun();
            while (reader.Read())
            {
                o.SifraObracuna = reader[0].ToString();
                o.DatumObracuna = Convert.ToDateTime(reader[1].ToString());
                o.PeriodOd = reader[2].ToString();
                o.PeriodDo = reader[3].ToString();
                o.Litara = Convert.ToInt32(reader[4]);
                o.SifraMlekare = reader[5].ToString();
                o.NazivMlekare = reader[6].ToString();
            }
            broker.ZatvoriKonekciju();

            return Ok(o);
        }

        // POST api/<ObracunController>
        [HttpPost]
        public IActionResult Post([FromBody] Obracun obracun)
        {
            broker.OtvoriKonekciju();
            try
            {
                var command = new NpgsqlCommand($"INSERT INTO \"Obracun\"(\"sifraObracuna\", \"datumObracuna\", \"periodOd\", \"periodDo\", litara, \"sifraMlekare\")" +
                    $"VALUES('{obracun.SifraObracuna}', '{obracun.DatumObracuna}', '{obracun.PeriodOd}', '{obracun.PeriodDo}', {obracun.Litara}, '{obracun.SifraMlekare}');", broker.connection);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                return Ok(ex.Message);
            }
            broker.ZatvoriKonekciju();

            return Ok(obracun);
        }

        // PUT api/<ObracunController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Obracun obracun)
        {
            broker.OtvoriKonekciju();
            try
            {
                var command = new NpgsqlCommand($"UPDATE \"Obracun\" SET \"sifraObracuna\" = '{obracun.SifraObracuna}', " +
                                                                        $"\"datumObracuna\" = '{obracun.DatumObracuna}', " +
                                                                        $"\"periodOd\" = '{obracun.PeriodOd}', " +
                                                                        $"\"periodDo\" = '{obracun.PeriodDo}', " +
                                                                        $"litara = {obracun.Litara}, " +
                                                                        $"\"sifraMlekare\" = '{obracun.SifraMlekare}', " +
                                                                        $"\"nazivMlekare\" = '{obracun.NazivMlekare}' " +
                                                                    $"WHERE \"sifraObracuna\" = '{obracun.SifraObracuna}';", broker.connection);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                return Ok(ex.Message);
            }
            broker.ZatvoriKonekciju();

            return Ok();
        }

        // DELETE api/<ObracunController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
