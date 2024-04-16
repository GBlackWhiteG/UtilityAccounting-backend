namespace utilityAccounting.Types
{
    public class IResult<Type>
    {
        public Type? result { get; set; }
        public bool status { get; set; }
    }
}
