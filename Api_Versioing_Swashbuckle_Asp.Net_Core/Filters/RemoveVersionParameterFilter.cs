using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Versioing_Swashbuckle_Asp.Net_Core.Filters
{
    public class RemoveVersionParameterFilter:IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.Single(p=>p.Name=="version");
            operation.Parameters.Remove(versionParameter);
        }
    }
}
