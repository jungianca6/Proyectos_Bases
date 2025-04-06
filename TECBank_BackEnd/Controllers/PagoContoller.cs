using Microsoft.AspNetCore.Mvc;

namespace TECBank_BackEnd.Controllers
{
    [ApiController]
    [Route("Pago/[controller]")]
    public class PagoContoller : ControllerBase
    {
        // GET: Pago/WeatherForecast
        [HttpGet]
        public ActionResult Get()
        {
            // Lógica para obtener datos
            return Ok();
        }

        // GET: Pago/WeatherForecast/5
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            // Lógica para obtener un dato por ID
            return Ok();
        }

        // POST: Pago/WeatherForecast
        [HttpPost]
        public ActionResult Create([FromBody] object data)
        {
            // Lógica para crear un nuevo recurso
            return CreatedAtAction(nameof(GetById), new { id = 0 }, data);
        }

        // PUT: Pago/WeatherForecast/5
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] object data)
        {
            // Lógica para actualizar un recurso existente
            return NoContent();
        }

        // DELETE: Pago/WeatherForecast/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Lógica para eliminar un recurso
            return NoContent();
        }
    }
}
