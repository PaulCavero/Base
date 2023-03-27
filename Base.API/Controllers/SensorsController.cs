using Base.API.Models.Sensors;
using Base.API.Services.Sensors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Base.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorService sensorService;

        public SensorsController(ISensorService sensorService)
        {
            this.sensorService = sensorService;
        }
        [HttpGet]
        public ActionResult<IQueryable<SensorDTO>> GetAllSensors()
        {
            try
            {
                IQueryable Sensors =
                    this.sensorService.RetrieveAllSensor();

                return Ok(Sensors);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<SensorDTO>> GetSensor(Guid id)
        {
            try
            {
                SensorDTO Sensor =
                    await this.sensorService.RetrieveSensorByIdAsync(id);
                return Ok(Sensor);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutSensor(SensorDTO Sensor)
        {
            try
            {
                await this.sensorService.ModifySensorAsync(Sensor);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<SensorDTO>> PostSensor(SensorDTO Sensor)
        {
            try
            {
                await this.sensorService.AddSensorAsync(Sensor);
                return CreatedAtAction("GetSensor", new { id = Sensor.SensorId }, Sensor);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(Guid id)
        {
            try
            {
                await this.sensorService.RemoveSensorByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
