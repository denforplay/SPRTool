namespace SPR.Shared.Models.API.Interfaces
{
    public interface IResponse<T>
    {
        bool IsSuccess { get; set; }
        T Data { get; set; }
        string Message { get; set; }
    }
}
