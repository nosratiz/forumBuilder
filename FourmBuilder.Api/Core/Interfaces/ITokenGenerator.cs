using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Application.Auth.Dto;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Result;

namespace FourmBuilder.Api.Core.Interfaces
{
    public interface ITokenGenerator
    {
        Task<Result<AuthenticationResult>> Generate(User user, CancellationToken cancellationToken);
    }
}