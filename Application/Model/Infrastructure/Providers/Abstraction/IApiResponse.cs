namespace Application.Model.Infrastructure.Providers.Abstraction
{
    public interface IApiResponse<out TData> where TData : new()
    {
        bool IsSuccess { get; }

        TData GetResponseData();

        string GetErrorMessage();
    }
}
