using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using utilityAccounting.Models;
using utilityAccounting.Services;
using utilityAccounting.Types;

namespace utilityAccounting.Controllers
{
    [Route("api/stage")]
    [ApiController]
    public class StageController : ControllerBase
    {
        private ApplicationContext db;

        public StageController(ApplicationContext context)
        {
            db = context;
        }

        [Route("get")]
        [HttpGet]
        public async Task<ActionResult<Stage>> Get(int id)
        {
            Stage? stage = db.Stages.FirstOrDefault(x => x.Id == id);

            if (stage == null) return BadRequest("Неверные данные");

            return Ok(stage);
        }

        [Route("stage-debt")]
        [HttpGet]
        public async Task<ActionResult<int>> GetStageDebt(int id)
        {
            Stage? stage = db.Stages.FirstOrDefault(x => x.Id == id);

            IResult<int> result = StageDebt.CountStageDebt(stage);

            if (!result.status) return BadRequest("Неверные данные");

            return result.result;
        }

        [Route("list")]
        [HttpGet]
        public async Task<ActionResult<List<Stage>>> GetAll()
        {
            return Ok(db.Stages.ToList());
        }

        [Route("add")]
        [HttpPost]
        public async Task<ActionResult<Stage>> Add(IStage model)
        {
            if (model == null) return BadRequest("Неверные данные");

            if (model.Payments.Length != model.Tariffs.Length)
                return BadRequest("Несовпадение количества элементов в полях: Payments, Tariffs");

            Stage stage = new() {
                BuildingId = model.BuildingId,
                Tariffs = model.Tariffs,
                Payments = model.Payments,
            };

            await db.Stages.AddAsync(stage);
            await db.SaveChangesAsync();
            return Ok(stage);
        }

        [Route("update")]
        [HttpPut]
        public async Task<ActionResult<Stage>> Update(Stage model)
        {
            if (model == null) return BadRequest("Неверные данные");

            if (!db.Stages.Any(x => x.Id == model.Id)) return BadRequest("Объект не найден");

            db.Stages.Update(model);
            await db.SaveChangesAsync();
            return Ok(model);
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<ActionResult<Stage>> Delete(int id)
        {
            Stage? stage = db.Stages.FirstOrDefault(x => x.Id == id);

            if (stage == null) return BadRequest("Неверные данные");

            db.Stages.Remove(stage);
            await db.SaveChangesAsync();
            return Ok(stage);
        }
    }
}
