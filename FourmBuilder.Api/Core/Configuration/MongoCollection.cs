using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace FourmBuilder.Api.Core.Configuration
{
    public static class MongoCollection
    {
        public static IServiceCollection AddMongoCollection(this IServiceCollection services)
        {
            services.AddMongoRepository<User>("Users");
            services.AddMongoRepository<Role>("Roles");
            services.AddMongoRepository<UserToken>("UserTokens");
            services.AddMongoRepository<Forum>("Forums");
            services.AddMongoRepository<UserFile>("UserFiles");
            services.AddMongoRepository<UserResponse>("UserResponses");

            return services;
        }
    }
}