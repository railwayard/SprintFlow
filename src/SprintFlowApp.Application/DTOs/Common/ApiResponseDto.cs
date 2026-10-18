
namespace SprintFlowApp.Application.DTOs.Common;

public class ApiResponseDto<T>
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public T? Data { get; set; }

    public ApiResponseDto()
    {
    }

    public ApiResponseDto(bool success, string message, T? data = default)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static ApiResponseDto<T> SuccessResponse(
        T data,
        string message = "Request completed successfully.")
    {
        return new ApiResponseDto<T>(true, message, data);
    }

    public static ApiResponseDto<T> ErrorResponse(string message)
    {
        return new ApiResponseDto<T>(false, message);
    }
}