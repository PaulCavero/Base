using Base.API.Models.Measurements;
using Base.API.Services.Measurements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Base.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly IMeasurementService measurementService;

        public MeasurementsController(IMeasurementService measurementService)
        {
            this.measurementService = measurementService;
        }
        [HttpGet]
        public ActionResult<IQueryable<MeasurementDTO>> GetAllMeasurements()
        {
            try
            {
                IQueryable Measurements =
                    this.measurementService.RetrieveAllMeasurement();
                return Ok(Measurements);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<MeasurementDTO>> GetMeasurement(long id)
        {
            try
            {
                MeasurementDTO Measurement =
                    await this.measurementService.RetrieveMeasurementByIdAsync(id);
                return Ok(Measurement);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutMeasurement(MeasurementDTO Measurement)
        {
            try
            {
                await this.measurementService.ModifyMeasurementAsync(Measurement);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MeasurementDTO>> PostMeasurement(MeasurementDTO Measurement)
        {
            try
            {
                await this.measurementService.AddMeasurementAsync(Measurement);
                return CreatedAtAction("GetMeasurement", new { id = Measurement.MeasurementId }, Measurement);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasurement(long id)
        {
            try
            {
                await this.measurementService.RemoveMeasurementByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
