namespace lievee.Models
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }
        public string? Err { get; private set; }

        public static ServiceResult<T> Success(T _data) => new ServiceResult<T> { IsSuccess = true, Data = _data };
        public static ServiceResult<T> Failed(string msg) => new ServiceResult<T> { IsSuccess = false, Err = msg };
    }
}
