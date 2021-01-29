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
    public class ZaposlenController : ControllerBase
    {
        private Broker broker = new Broker();

        // GET: api/<ZaposlenController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/<ZaposlenController>/osnovno
        [HttpGet("osnovno")]
        public IActionResult GetOsnovno()
        {
            List<ZaposlenOsnovno> zaposleni = new List<ZaposlenOsnovno>();

            broker.OtvoriKonekciju();

            var command = new NpgsqlCommand(@"SELECT * FROM ""Zaposleni_osnovno"";", broker.connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            ZaposlenOsnovno m = new ZaposlenOsnovno();
            while (reader.Read())
            {
                m = new ZaposlenOsnovno();
                m.sifraZaposlenog = Convert.ToInt32(reader[0]);
                m.ime = reader[1].ToString();
                m.prezime = reader[2].ToString();
                zaposleni.Add(m);
            }
            broker.ZatvoriKonekciju();

            return Ok(zaposleni);
        }

        // GET: api/<ZaposlenController>/detaljno
        [HttpGet("detaljno")]
        public IActionResult GetDetaljno()
        {
            List<ZaposlenDetaljno> zaposleni = new List<ZaposlenDetaljno>();

            broker.OtvoriKonekciju();

            var command = new NpgsqlCommand(@"SELECT * FROM ""Zaposleni_detalji"";", broker.connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            ZaposlenDetaljno m = new ZaposlenDetaljno();
            while (reader.Read())
            {
                m = new ZaposlenDetaljno();
                m.sifraZaposlenog = Convert.ToInt32(reader[0]);
                m.jmbg = reader[1].ToString();
                m.brLicneKarte = reader[2].ToString();
                zaposleni.Add(m);
            }
            broker.ZatvoriKonekciju();

            return Ok(zaposleni);
        }

        // GET api/<ZaposlenController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ZaposlenController>
        [HttpPost]
        public IActionResult Post([FromBody] Zaposlen zaposlen)
        {
            broker.OtvoriKonekciju();
            try
            {
                var command = new NpgsqlCommand($"INSERT INTO \"zaposleni_view1\"(\"sifraZaposlenog\", ime, prezime, jmbg, \"brLicneKarte\")" +
                    $"VALUES({zaposlen.sifraZaposlenog}, '{zaposlen.ime}', '{zaposlen.prezime}', '{zaposlen.jmbg}', '{zaposlen.brLicneKarte}');", broker.connection);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                return Ok(ex.Message);
            }
            broker.ZatvoriKonekciju();

            return Ok(zaposlen);
        }

        // PUT api/<ZaposlenController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ZaposlenController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
