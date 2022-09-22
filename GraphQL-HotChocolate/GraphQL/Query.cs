using GraphQL_HotChocolate.Data;
using GraphQL_HotChocolate.Models;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace GraphQL_HotChocolate.GraphQL
{
    public class Query
    {
        private readonly ILogger _logger;
        public Query(ILogger<Query> logger)
        {
            _logger = logger;
        }


        [UseDbContext(typeof(AppDbContext))]
        //[UseProjection] //Pull back any child objects
        [UseFiltering]
        [UseSorting]
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context)
        {
            _logger.LogInformation("GetPlatform context " +  context.GetHashCode().ToString());
          
            return context.Platforms;
        }

        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        //[UseProjection] //Pull back any child objects
        public IQueryable<Command> GetCommand([ScopedService] AppDbContext context)
        {
            _logger.LogInformation("GetCommand context " +  context.GetHashCode().ToString());

            return context.Commands;
        }
    }
}
