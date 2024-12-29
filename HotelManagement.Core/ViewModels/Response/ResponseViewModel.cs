﻿using HotelManagement.App.Core.Enums;
using HotelManagement.App.Core.Validation;

namespace HotelManagement.App.Core.ViewModels.Response;
public abstract class ResponseViewModel<T>
{
    public bool IsSuccess { get; protected set; }
    public T? Data { get; protected set; } = default;
    public ErrorCode? ErrorCode { get; protected set; } = default;
    public SuccessCode? SuccessCode { get; protected set; } = default;
    public string? Message { get; protected set; } = string.Empty;
    public string? CustomMessage { get; set; } = string.Empty;

    public IEnumerable<ValidationError>? ValidationErrors { get; protected set; } = Enumerable.Empty<ValidationError>();
}
