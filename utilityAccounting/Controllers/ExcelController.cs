using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using utilityAccounting.Models;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;


namespace utilityAccounting.Controllers
{
    public class ExcelController : Controller
    {
        private ApplicationContext db;

        public ExcelController(ApplicationContext context)
        {
            db = context;
        }

        [Route("getExcel")]
        [HttpGet]
        public async Task<ActionResult> GetExcelFile(int id)
        {
            Stage[] stages = db.Stages.Where(x => x.BuildingId == id).ToArray();

            if (stages == null) return BadRequest("Неверные данные");

            int listsLength = stages.Count();

            var package = new ExcelPackage();

            var firstWorksheet = package.Workbook.Worksheets.Add("First Sheet");
            var secondWorksheet = package.Workbook.Worksheets.Add("Second Sheet");
            var thirdWorksheet = package.Workbook.Worksheets.Add("Third Sheet");

            for (int i = 0; i < listsLength; i++)
            {
                int[] tariffs = stages[i].Tariffs;
                firstWorksheet.Cells[1, i + 1].LoadFromCollection(tariffs);

                int[] payments = stages[i].Payments;
                secondWorksheet.Cells[1, i + 1].LoadFromCollection(payments);

                int[] debts = new int[tariffs.Length];
                for (int j = 0; j < tariffs.Length; j++)
                {
                    debts[j] = tariffs[j] - payments[j];
                }
                thirdWorksheet.Cells[1, i + 1].LoadFromCollection(debts);
            }

            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            string excelName = "BuildingsStagesInfo";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}

