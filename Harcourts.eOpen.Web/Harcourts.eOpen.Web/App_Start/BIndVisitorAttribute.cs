using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Harcourts.eOpen.Web
{
    public class BindVisitorAttribute : ParameterBindingAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            return new VisitorParameterBinding(parameter);
        }

        public class VisitorParameterBinding : HttpParameterBinding
        {
            private readonly JsonSerializerSettings _jsonSerializerSettings;

            public VisitorParameterBinding(HttpParameterDescriptor descriptor)
                : base(descriptor)
            {
                _jsonSerializerSettings =
                    new JsonSerializerSettings
                    {
                        ContractResolver =
                            new CamelCasePropertyNamesContractResolver(),
                        Formatting = Formatting.Indented
                    };
                _jsonSerializerSettings.Converters.Add(new BinaryConverter());
                _jsonSerializerSettings.Converters.Add(new StringConverter());
            }

            public override async Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider,
                HttpActionContext actionContext,
                CancellationToken cancellationToken)
            {
                var body = await actionContext.Request.Content.ReadAsStringAsync();
                var visitor = VisitorHelper.Create((JToken) JsonConvert.DeserializeObject(body, _jsonSerializerSettings));
                actionContext.ActionArguments[Descriptor.ParameterName] = visitor;
            }
        }
    }
}