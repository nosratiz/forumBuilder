using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using FourmBuilder.Common.Helper;
using FourmBuilder.Common.Helper.systemMessage;
using FourmBuilder.Common.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FourmBuilder.Common.Middleware
{
    public class MemberShipMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSetting _options;

        public MemberShipMiddleware(RequestDelegate next, IOptions<JwtSetting> options)
        {
            _next = next;
            _options = options.Value;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.HasAuthorization())
            {
                //get Token From header  and  decode the token with Secret Key
                var token = httpContext.GetAuthorizationToken();

                var secretKey = Encoding.UTF8.GetBytes(_options.SecretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero, // default: 5 min
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateAudience = true, //default : false
                    ValidAudience = _options.ValidAudience,
                    ValidateIssuer = true, //default : false
                    ValidIssuer = _options.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                };

                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var claimsPrincipal = handler.ValidateToken(token, validationParameters, out _);
                    httpContext.User = claimsPrincipal;


               
                }
                catch (Exception ex)
                {
                    await httpContext.WriteError(new ApiMessage(ex.Message), StatusCodes.Status401Unauthorized);
                    return;
                }
            }


            await _next(httpContext);
        }
    }
}
