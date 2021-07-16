#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace basicApi
{
    public static class HttpEndpointsModulesRegistrationExtensions
    {
        
        public static IEndpointRouteBuilder MapMyEndPoint<THttpEndpointsModule>(this IEndpointRouteBuilder endpoints)
        {
            var type = typeof(THttpEndpointsModule);

            var methods = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(m => new {method = m, attribute = m.GetCustomAttribute<HttpMethodAttribute>(true)})
                .Where(m => m.attribute is not null)
                .Select(m => new {
                    methodInfo = m.method, 
                    template = m.attribute?.Template, 
                    httpMethod = m.attribute?.HttpMethods
                })
                .ToList();
            
            foreach (var method in methods)
            {
                if (method.methodInfo.ReturnType != typeof(Task))
                    throw new Exception($"Endpoints must return a {nameof(Task)}.");
                
                if (method.methodInfo.GetParameters().FirstOrDefault()?.ParameterType != typeof(HttpContext))
                    throw new Exception($"{nameof(HttpContext)} must be the first parameter of any endpoint.");

                if (method.template == null) throw new ArgumentNullException("method cannot be null");
                if (method.httpMethod == null) throw new ArgumentNullException("method cannot be null");
             
                
                endpoints.MapMethods( method.template, method.httpMethod, context => {
                    var module = context.RequestServices.GetService(type);

                    if (module is null) {
                        throw new Exception($"{type.Name} is not registered in services collection.");
                    }
                    
                    var parameters = method.methodInfo.GetParameters();
                    List<object?> arguments = new() { context };
                    
                    // skip httpContext
                    foreach (var parameter in parameters.Skip(1))
                    {
                        var arg = context.RequestServices.GetService(parameter.ParameterType);
                        if (arg is null) {
                            throw new Exception($"{parameter.ParameterType} is not registered in services collection.");
                        }

                        arguments.Add(arg);
                    }
                    
                    var task = method.methodInfo.Invoke(module, arguments.ToArray()) as Task;
                    return task!;
                });
            }

            return endpoints;
        }
    }
}
