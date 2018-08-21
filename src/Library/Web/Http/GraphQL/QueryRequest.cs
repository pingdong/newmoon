using System.Text;

namespace PingDong.Web.Http.GraphQL
{
    public class QueryRequest
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public string Variables { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine();

            if (!string.IsNullOrWhiteSpace(OperationName))
                builder.AppendLine($"{nameof(OperationName)} = {OperationName}");

            if (!string.IsNullOrWhiteSpace(NamedQuery))

                builder.AppendLine($"{nameof(NamedQuery)} = {NamedQuery}");

            if (!string.IsNullOrWhiteSpace(Query))
                builder.AppendLine($"{nameof(Query)} = {Query}");

            if (!string.IsNullOrWhiteSpace(Variables))
                builder.AppendLine($"{nameof(Variables)} = {Variables}");


            return builder.ToString();
        }
    }
}
