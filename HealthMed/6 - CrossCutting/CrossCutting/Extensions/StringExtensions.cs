﻿using System.Security.Cryptography;
using System.Text;

namespace CrossCutting.Extensions;

public static class StringExtensions
{
    public static string HashPassword(this string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
