using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADE.Seguridad.Application.DTOs;
public class LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class TokenResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public int IdPersona { get; set; }
    public DateTime Expiracion { get; set; }
}

public class RegisterDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int IdPersona { get; set; }
    public int IdRol { get; set; }
}
