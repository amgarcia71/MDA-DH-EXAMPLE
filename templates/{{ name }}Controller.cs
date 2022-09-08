using Femsa.Lending.Core.Common.Constant;
using Femsa.Lending.Core.Common.Mapster;
using Femsa.Lending.Core.Common.Models.ExceptionHandling;
using Femsa.Lending.Core.Common.Util;
using Femsa.Lending.Core.Rest.Base;
using Femsa.Lending.Model.Request;
using Femsa.Lending.Model.Response;
using Femsa.Lending.Query.Services;
using Femsa.Lending.Task.Services;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Femsa.Lending.Api.Controllers
{
    [RoutePrefix("api/{{ name }}")]
    public class {{ name }}Controller : ApiControllerBase
    {
        private readonly ITaskServiceCliente taskServiceCliente;
        private readonly IQueryServiceCliente queryServiceCliente;
        private readonly IObjectMapper mapper;

        public {{ name }}Controller(ITaskServiceCliente taskServiceCliente,
            IQueryServiceCliente queryServiceCliente,
            IObjectMapper mapper)
        {
            this.taskServiceCliente = taskServiceCliente;
            this.queryServiceCliente = queryServiceCliente;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof({{ name }}Dto))]
        [SwaggerResponse(GenericConstant.HttpStatus.Business, type: typeof(ErrorDataException))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized)]
        [Route("")]
        public async Task<IHttpActionResult> {{ name }}Create ([FromBody] {{ name }}Rq {{ name }}Rq)
        {
            var {{ name }}Dto = mapper.Map<{{ name }}Dto>({{ name }}Rq);
            {% for col in columns %}

                {{ name }}Dto.{{ col.name }} = this.{{ col.name }};
                
            {% endfor %}  

            {{ col.name }}Dto.CognitoUser = new CognitoUserDto
            {
                IdCognito = this.IdCognitoUser
            };

            var {{ name }} = await taskServiceCliente.{{ name }}Create({{ name }}Dto);
            return ResolveResult({{ name }});
        }

        [HttpGet]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof({{ col.name }}Dto))]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(GenericConstant.HttpStatus.Business, type: typeof(ErrorDataException))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized)]
        [Route("")]
        public async Task<IHttpActionResult> {{ col.name }}Get()
        {
            var {{ col.name }} = await this.queryService{{ col.name }}.{{ col.name }}GetByCognitoId(this.IdUsuarioCognito);
            return ResolveResult<ClienteDto>(cliente);
        }

        //  to be continue ...

}