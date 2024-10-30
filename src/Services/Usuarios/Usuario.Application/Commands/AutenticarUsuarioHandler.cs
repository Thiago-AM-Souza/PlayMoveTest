using BuildingBlocks.CQRS;
using BuildingBlocks.Errors;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OneOf;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Usuario.Application.Commands
{
    internal class AutenticarUsuarioHandler 
        : ICommandHandler<AutenticarUsuarioCommand, OneOf<AutenticarUsuarioResult, AppError>>
    {
        private readonly IConfiguration _configuration;

        public AutenticarUsuarioHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<OneOf<AutenticarUsuarioResult, AppError>> Handle(AutenticarUsuarioCommand command, CancellationToken cancellationToken)
        {
            if (command.User.Name != "admin" || command.User.Password != "admin")
            {
                return new ErroNaoEncontrado("Logar com admin admin", ErrorType.NotFound);
            }                        

            // Gera o token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT_SECRET"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, command.User.Name),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new AutenticarUsuarioResult(tokenString);
        }
    }
}
