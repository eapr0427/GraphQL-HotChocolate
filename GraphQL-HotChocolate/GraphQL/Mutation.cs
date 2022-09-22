using GraphQL_HotChocolate.Data;
using GraphQL_HotChocolate.GraphQL.Commands;
using GraphQL_HotChocolate.GraphQL.Platforms;
using GraphQL_HotChocolate.Models;
using HotChocolate;
using HotChocolate.Data;
using System.Threading.Tasks;

namespace GraphQL_HotChocolate.GraphQL
{
    public class Mutation
    {

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, [ScopedService] AppDbContext context)
        {
            var platform = new Platform
            {
                Name = input.Name
            };

            context.Platforms.Add(platform);

            await context.SaveChangesAsync();

            return new AddPlatformPayload(platform);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context)
        {
            var command = new Command
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };

            context.Commands.Add(command);

            await context.SaveChangesAsync();

            return new AddCommandPayload(command);
        
        }
    }
}
