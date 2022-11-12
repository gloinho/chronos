﻿using System.ComponentModel.DataAnnotations;

namespace Chronos.Domain.Contracts.Request
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
