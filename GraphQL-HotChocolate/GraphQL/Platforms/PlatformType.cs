using GraphQL_HotChocolate.Data;
using GraphQL_HotChocolate.Models;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace GraphQL_HotChocolate.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Description("Represents any software or service thar as a command line interface.");

            descriptor.Field(p => p.LicenseKey).Ignore();

            descriptor
                .Field(p => p.Commands)
                .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the list of available commands for this platform");
        }

        private class Resolvers
        {
            private readonly ILogger _logger;
            public Resolvers(ILogger<Query> logger)
            {
                _logger = logger;
            }

            public IQueryable<Command> GetCommands([Parent] Platform platform, [ScopedService] AppDbContext context)
            {
                _logger.LogInformation("GetCommands Resolver " + context.GetHashCode().ToString());

                return context.Commands.Where(p => p.PlatformId == platform.Id);
            }
        }
    }
}
