using System;
using System.Threading.Tasks;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace PingDong.Web.Http.GraphQL
{
    public static class GraphQLExtensions
    {
        public static void FieldAsync(this IObjectGraphType obj, string name, IGraphType type, Func<ResolveFieldContext, Task<object>> resolve = null)
        {
            var field = new FieldType
                {
                    Name = name,
                    ResolvedType = type,
                    Resolver = resolve != null ? new FuncFieldResolver<object>(resolve) : null
                };

            obj.AddField(field);
        }
    }
}
