using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using utilityAccounting.Models;
using utilityAccounting.Services;
using utilityAccounting.Types;

namespace utilityAccounting.Controllers
{
    [Route("api/building")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private ApplicationContext db;

        public BuildingController(ApplicationContext context)
        {
            db = context;
        }

        [Route("get")]
        [HttpGet]
        public async Task<ActionResult<int>> Get(int id)
        {
            Building? building = db.Buildings.FirstOrDefault(x => x.Id == id);

            if (building == null) return BadRequest("Неверные данные");

            return Ok(building);
        }

        [Route("stages")]
        [HttpGet]
        public async Task<ActionResult<int[]>> GetStages(int id)
        {
            if (!db.Buildings.Any(x => x.Id == id)) return BadRequest("Объект не найден");

            List<Stage> stages = db.Stages.Where(x => x.BuildingId == id).ToList();

            return Ok(stages);
        }

        [Route("list")]
        [HttpGet]
        public async Task<ActionResult<List<Building>>> GetAll()
        {
            return Ok(db.Buildings.ToList());
        }

        [Route("total-debt")]
        [HttpGet]
        public async Task<ActionResult<int>> GetTotalDebt(int id)
        {
            List<Stage> stages = db.Stages.Where(x => x.BuildingId == id).ToList();
            int total = 0;

            for (int i = 0; i < stages.Count(); i++)
            {
                total += StageDebt.CountStageDebt(stages[i]).result;
            }

            return total;
        }

        [Route("add")]
        [HttpPost]
        public async Task<ActionResult<Building>> Add(IBuilding model)
        {
            if (model == null) return BadRequest("Неверные данные");

            Building building = new()
            {
                Coordinates = model.Coordinates,
                Address = model.Address,
            };

            db.Buildings.Add(building);
            await db.SaveChangesAsync();
            return Ok(building);
        }

        [Route("update")]
        [HttpPut]
        public async Task<ActionResult<Building>> Update(Building model) 
        {
            if (model == null) return BadRequest("Неверные данные");

            if (!db.Buildings.Any(x => x.Id == model.Id)) return BadRequest("Объект не найден");

            db.Buildings.Update(model);
            await db.SaveChangesAsync();
            return Ok(model);
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<ActionResult<Building>> Delete(int id)
        {
            Building? building = db.Buildings.FirstOrDefault(x => x.Id == id);

            if (building == null) return BadRequest("Объект не найден");

            db.Buildings.Remove(building);
            await db.SaveChangesAsync();
            return building;
        }
    }
}
