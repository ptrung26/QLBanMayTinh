﻿namespace BTLWEB.Controllers.Middlewares
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
