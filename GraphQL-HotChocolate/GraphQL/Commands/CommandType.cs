using GraphQL_HotChocolate.Data;
using GraphQL_HotChocolate.Models;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace GraphQL_HotChocolate.GraphQL.Commands
{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor.Description("Represents any executable command");

            descriptor
                .Field(c => c.Platform)
                .ResolveWith<Resolvers>(c => c.GetPlatform(default,default))
                .UseDbContext<AppDbContext>()
                .Description("This is the platform to which the command belongs");
        }

        private class Resolvers
        {
            private readonly ILogger _logger;
            public Resolvers(ILogger<Query> logger)
            {
                _logger = logger;
            }
            public Platform GetPlatform([Parent] Command command, [ScopedService] AppDbContext context)
            {
                _logger.LogInformation("GetPlatform Resolver " + context.GetHashCode().ToString());
                return context.Platforms.FirstOrDefault(mm => mm.Id == command.PlatformId);
            }
        }
    }
}
