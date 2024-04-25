﻿namespace Infrastructure.Authentication;

public class JwtOptions
{
    public string SecretKey { get; set; } = string.Empty;
    public int Expires { get; set; }
}