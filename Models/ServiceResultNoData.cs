namespace lievee.Models
{
    public class ServiceResultNoData
    {
        public bool IsSuccess { get; private set; }
        public string? Err { get; private set; }

        public static ServiceResultNoData SuccessNoData() => new ServiceResultNoData { IsSuccess = true };
        public static ServiceResultNoData Failed(string msg) => new ServiceResultNoData { IsSuccess = false, Err = msg };
    }
}
