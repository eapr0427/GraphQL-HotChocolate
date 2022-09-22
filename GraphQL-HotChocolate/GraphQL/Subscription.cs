using GraphQL_HotChocolate.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQL_HotChocolate.GraphQL
{
    public class Subscription
    {

        [Subscribe]
        [Topic]
        public Platform OnPlatformAdded([EventMessage] Platform platform) => platform;
    }
}
