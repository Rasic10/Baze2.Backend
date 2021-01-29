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
    public class KravaController : ControllerBase
    {
        private Broker broker = new Broker();

        // GET: api/<KravaController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Krava> krave = new List<Krava>();

            broker.OtvoriKonekciju();

            // for composite type and enum type
            broker.connection.TypeMapper.MapEnum<Pol>("POL");
            broker.connection.TypeMapper.MapComposite<Rasa>("RASA");

            var command = new NpgsqlCommand(@"SELECT * FROM ""Krava"";", broker.connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            Krava k = new Krava();
            while (reader.Read())
            {
                k = new Krava();
                k.IdZivotinje = reader[0].ToString();
                k.Ime = reader[1].ToString();
                k.DatumRodjenja = Convert.ToDateTime(reader[2].ToString());
                k.IdZivotinjeMajke = reader[3].ToString();
                k.Pol = reader.GetFieldValue<Pol>(4);
                k.Rasa = reader.GetFieldValue<Rasa>(5);
                if (reader[6] != DBNull.Value)
                {
                    k.TrenutnaKolicinaMleka = Convert.ToInt32(reader[6]);
                }
                krave.Add(k);
            }
            broker.ZatvoriKonekciju();

            return Ok(krave);
        }

        // GET: api/<KravaController>
        [HttpGet("krava")]
        public IActionResult GetKrava()
        {
            List<Krava> krave = new List<Krava>();

            broker.OtvoriKonekciju();

            // for composite type and enum type
            broker.connection.TypeMapper.MapEnum<Pol>("POL");
            broker.connection.TypeMapper.MapComposite<Rasa>("RASA");

            var command = new NpgsqlCommand(@"SELECT * FROM ""krava_z"";", broker.connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            Krava k = new Krava();
            while (reader.Read())
            {
                k = new Krava();
                k.IdZivotinje = reader[0].ToString();
                k.Ime = reader[1].ToString();
                k.DatumRodjenja = Convert.ToDateTime(reader[2].ToString());
                k.IdZivotinjeMajke = reader[3].ToString();
                k.Pol = reader.GetFieldValue<Pol>(4);
                k.Rasa = reader.GetFieldValue<Rasa>(5);
                if (reader[6] != DBNull.Value)
                {
                    k.TrenutnaKolicinaMleka = Convert.ToInt32(reader[6]);
                }
                krave.Add(k);
            }
            broker.ZatvoriKonekciju();

            return Ok(krave);
        }

        // GET: api/<KravaController>
        [HttpGet("bik")]
        public IActionResult GetBik()
        {
            List<Krava> krave = new List<Krava>();

            broker.OtvoriKonekciju();

            // for composite type and enum type
            broker.connection.TypeMapper.MapEnum<Pol>("POL");
            broker.connection.TypeMapper.MapComposite<Rasa>("RASA");

            var command = new NpgsqlCommand(@"SELECT * FROM ""krava_m"";", broker.connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            Krava k = new Krava();
            while (reader.Read())
            {
                k = new Krava();
                k.IdZivotinje = reader[0].ToString();
                k.Ime = reader[1].ToString();
                k.DatumRodjenja = Convert.ToDateTime(reader[2].ToString());
                k.IdZivotinjeMajke = reader[3].ToString();
                k.Pol = reader.GetFieldValue<Pol>(4);
                k.Rasa = reader.GetFieldValue<Rasa>(5);
                if (reader[6] != DBNull.Value)
                {
                    k.TrenutnaKolicinaMleka = Convert.ToInt32(reader[6]);
                }
                krave.Add(k);
            }
            broker.ZatvoriKonekciju();

            return Ok(krave);
        }

        // GET api/<KravaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            

            var krava = vratiKravu(id);

            return Ok(krava);
        }

        // POST api/<KravaController>
        [HttpPost]
        public IActionResult Post([FromBody] Krava krava)
        {
            broker.OtvoriKonekciju();
            try
            {
                var command = new NpgsqlCommand($"INSERT INTO \"Krava\"(\"idZivotinje\", ime, \"datumRodjenja\", \"idZivotinjeMajke\", pol, rasa, \"trenutnaKolicinaMleka\")" +
                    $"VALUES('{krava.IdZivotinje}', '{krava.Ime}', '{krava.DatumRodjenja}', '{krava.IdZivotinjeMajke}', '{krava.Pol}', row('{krava.Rasa.Naziv}', '{krava.Rasa.Boja}'), 0);", broker.connection);
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                return Ok(ex.Message);
            }
            broker.ZatvoriKonekciju();

            return Ok(krava);
        }

        // PUT api/<KravaController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Krava krava)
        {
            
            //var k = vratiKravu(Convert.ToInt32(krava.IdZivotinje));

            broker.OtvoriKonekciju();
            //if (k.TrenutnaKolicinaMleka == krava.TrenutnaKolicinaMleka)
            //{
                try
                {
                    var command = new NpgsqlCommand($"UPDATE \"Krava\" SET \"idZivotinje\" = '{krava.IdZivotinje}', " +
                                                                            $"ime = '{krava.Ime}', " +
                                                                            $"\"datumRodjenja\" = '{krava.DatumRodjenja}', " +
                                                                            $"\"idZivotinjeMajke\" = '{krava.IdZivotinjeMajke}', " +
                                                                            $"\"pol\" = '{krava.Pol}', " +
                                                                            //$"\"rasa\" = '{krava.Rasa}' " +
                                                                            $"\"trenutnaKolicinaMleka\" = {krava.TrenutnaKolicinaMleka} " +
                                                                        $"WHERE \"idZivotinje\" = '{krava.IdZivotinje}';", broker.connection);
                    command.ExecuteNonQuery();
                }
                catch (NpgsqlException ex)
                {
                    broker.ZatvoriKonekciju();
                    return Ok(ex.Message);
                }
            //}
            //else
            //{
            //    try
            //    {
            //        var command = new NpgsqlCommand($"UPDATE \"Krava\" SET \"trenutnaKolicinaMleka\" = '{krava.TrenutnaKolicinaMleka}' " +
            //                                                            $"WHERE \"idZivotinje\" = '{krava.IdZivotinje}';", broker.connection);
            //        command.ExecuteNonQuery();
            //    }
            //    catch (NpgsqlException ex)
            //    {
            //        broker.ZatvoriKonekciju();
            //        return Ok(ex.Message);
            //    }
            //}

            broker.ZatvoriKonekciju();

            return Ok();
        }

        // DELETE api/<KravaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }

        private Krava vratiKravu(int id)
        {
            broker.OtvoriKonekciju();

            broker.connection.TypeMapper.MapEnum<Pol>("POL");
            broker.connection.TypeMapper.MapComposite<Rasa>("RASA");
            var command = new NpgsqlCommand(@"SELECT * FROM ""Krava"" WHERE ""idZivotinje"" = '" + id + "';", broker.connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            Krava krava = new Krava();
            while (reader.Read())
            {
                krava.IdZivotinje = reader[0].ToString();
                krava.Ime = reader[1].ToString();
                krava.DatumRodjenja = Convert.ToDateTime(reader[2].ToString());
                krava.IdZivotinjeMajke = reader[3].ToString();
                krava.Pol = reader.GetFieldValue<Pol>(4);
                krava.Rasa = reader.GetFieldValue<Rasa>(5);
                if (reader[6] != DBNull.Value)
                {
                    krava.TrenutnaKolicinaMleka = Convert.ToInt32(reader[6]);
                }
            }
            broker.ZatvoriKonekciju();

            return krava;
        }
    }
}
