using System.Net.Mime;
using System.Text;

namespace AngularWeb.Api.HttpClients;

public class JsonContent : StringContent
{
    public JsonContent(string content) : this(content, Encoding.UTF8)
    {
    }

    public JsonContent(string content, Encoding encoding)
        : base(content, encoding, MediaTypeNames.Application.Json)
    {
    }
}