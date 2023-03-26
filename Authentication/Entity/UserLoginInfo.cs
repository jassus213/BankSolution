﻿namespace Authentication.Entity;

public class UserLoginInfo
{
    public int Id { get; init; }
    public string Login { get; init; }
    public string Password { get; init; }
    public string Provider { get; init; }
}