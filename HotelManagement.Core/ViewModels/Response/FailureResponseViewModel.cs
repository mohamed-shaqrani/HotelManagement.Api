using HotelManagement.App.Core.Enums;
using HotelManagement.App.Core.Validation;

namespace HotelManagement.App.Core.ViewModels.Response;

public class FailureResponseViewModel<T> : ResponseViewModel<T>
{
    public FailureResponseViewModel(ErrorCode errorCode, IEnumerable<ValidationError>? validationErrors = default)
    {
        IsSuccess = false;
        ErrorCode = errorCode;
        Message = ResponseMessage.Failure(errorCode);
        ValidationErrors = validationErrors;
    }
}
