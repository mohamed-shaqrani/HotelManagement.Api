using HotelManagement.App.Core.Enums;

namespace HotelManagement.App.Core.ViewModels.Response;

public class SuccessResponseViewModel<T> : ResponseViewModel<T>
{
    public SuccessResponseViewModel(SuccessCode successCode, T? data = default)
    {
        IsSuccess = true;
        SuccessCode = successCode;
        Data = data;
        Message = ResponseMessage.Success(successCode);

    }
}
