using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FourmBuilder.Api.Installer
{
    public interface IInstaller
    {
        void InstallServices(IConfiguration configuration, IServiceCollection services);
    }
}