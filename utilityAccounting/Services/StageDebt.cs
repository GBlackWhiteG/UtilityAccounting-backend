using utilityAccounting.Models;
using utilityAccounting.Types;

namespace utilityAccounting.Services
{
    public class StageDebt
    {
        public static IResult<int> CountStageDebt(Stage stage)
        {
            IResult<int> result = new IResult<int>();

            if (stage == null)
            {
                result.status = false;
                return result;
            }

            int[] tariff = stage.Tariffs;
            int[] payments = stage.Payments;
            int total = 0;
            int debt = 0;

            for (int i = 0; i < tariff.Length; i++)
            {
                debt = tariff[i] - payments[i];
                if (debt > 0)
                    total += debt;
            }

            result.result = total;
            result.status = true;
            return result;
        }
    }
}
