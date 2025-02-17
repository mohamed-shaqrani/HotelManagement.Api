﻿using HotelManagement.Core.Enums;
using HotelManagement.Core.Validation;

namespace HotelManagement.Core.ViewModels.Response;

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
