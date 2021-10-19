﻿using System;

namespace LibraryApi.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) :base(message)
        {

        }
    }
}
