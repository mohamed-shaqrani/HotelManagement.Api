﻿namespace HotelManagement.Core.Enums;

public enum ErrorCode
{
    None = 00,
    ValidationError = 01,
    DataBaseError = 02,


    // AUTH
    ChangePasswordError = 100,
    IncorrectPassword = 101,

    // USER
    UserNotFound = 200,
    UserNameExist = 201,
    UserEmailExist = 202,
    UserPhoneExist = 203,

    //Room
    RoomNameIsRequired = 300,
    RoomDescriptionIsRequired = 301,
    RoomAlreadyExists = 302,
    RoomIDNotExist = 303,
}
