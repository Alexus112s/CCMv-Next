using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Attributes;
using Reinforced.Typings.Generators;
using System;
using System.Linq;
using System.Reflection;

namespace CCMvNext.Infrastructure.ReinforcedTypings
{
    public class Angular2ApiGenerator : ClassCodeGenerator
    {
        public const string NameSuffix = "Service";

        /// <summary>
        /// Generates the Angular service declaration.
        /// <code><para>@Injectable({ providedIn: 'root' }) export class CookieConsentsService extends ApiInvokeService {...}</para></code>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="result"></param>
        /// <param name="resolver"></param>
        /// <returns></returns>
        public override RtClass GenerateNode(Type element, RtClass result, TypeResolver resolver)
        {
            result = base.GenerateNode(element, result, resolver);
            var name = result.Name;

            result.Name = new RtSimpleTypeName(element.GetShortName() + NameSuffix) { Prefix = name.Prefix };

            result.Extendee = new RtSimpleTypeName("ApiInvokeService");

            var httpServiceType = new RtSimpleTypeName("HttpClient");

            var constructor = new RtConstructor { NeedsSuperCall = true };
            constructor.Arguments.Add(new RtArgument { Type = httpServiceType, Identifier = new RtIdentifier("http") });
            constructor.SuperCallParameters.Add("http");
            constructor.SuperCallParameters.Add($"'{element.GetShortName()}'");

            result.Members.Add(constructor);

            result.Decorators.Add(new RtDecorator("Injectable({ providedIn: 'root' })"));

            return result;
        }
    }

    public class AngularInvokeGenerator : MethodCodeGenerator
    {
        public const string FunctionTemplatePost = "return super.{0}Request('{1}', {2}, {3});";

        public const string FunctionTemplateGet = "return super.{0}Request('{1}', {2});";

        /// <summary>
        /// Generates Controller Action call code.
        /// <code><para>
        /// public async put(id: string, record: ICookieConsentRecord) : Promise&lt;void&gt; {
        /// <para> return super.putRequest('{id}', { 'id': id }, record); }</para>
        /// </para></code>
        /// <para>ToDo: needs some refactoring.</para>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="result"></param>
        /// <param name="resolver"></param>
        /// <returns></returns>
        public override RtFunction GenerateNode(MethodInfo element, RtFunction result, TypeResolver resolver)
        {
            result = base.GenerateNode(element, result, resolver);

            if (element.GetCustomAttribute<RouteAttribute>() != null)
            {
                throw new ArgumentException("[Route] attribute is not supported for now...");
            }

            var tsFunctionAttr = element.GetCustomAttribute<TsFunctionAttribute>();

            // Extract HTTP method
            var hasFormData = false;
            var httpVerb = "get";
            var functionTemplate = FunctionTemplateGet;
            HttpMethodAttribute methodAttr = element.GetCustomAttribute<HttpMethodAttribute>();

            switch (methodAttr)
            {
                case HttpPostAttribute post:
                    hasFormData = true;
                    functionTemplate = FunctionTemplatePost;
                    httpVerb = "post";
                    break;
                case HttpPutAttribute put:
                    hasFormData = true;
                    functionTemplate = FunctionTemplatePost;
                    httpVerb = "put";
                    break;
                case HttpDeleteAttribute delete:
                    hasFormData = false;
                    httpVerb = "delete";
                    break;
                case HttpGetAttribute get:
                default:
                    break;
            }

            string urlPath;

            // handle METHOD "controller/Id" case
            if (methodAttr != null && !string.IsNullOrEmpty(methodAttr.Template))
            {
                // {id:length(24)}
                var temp = methodAttr.Template;
                var indexOfCol = temp.IndexOf(':');
                while (indexOfCol > 0)
                {
                    var bracketIndex = temp.IndexOf("}", indexOfCol);

                    temp = temp.Remove(indexOfCol, bracketIndex - indexOfCol);

                    indexOfCol = temp.IndexOf(':');
                }

                urlPath = temp;
            }
            else
            {
                urlPath = element.Name.Replace(httpVerb, "", StringComparison.OrdinalIgnoreCase); // remove HTTP verb from the path.
            }

            if (tsFunctionAttr == null || tsFunctionAttr.StrongType == null && string.IsNullOrEmpty(tsFunctionAttr.Type))
            {
                // unfold Task
                if (element.ReturnType.Name == "Task`1")
                {
                    result.ReturnType = resolver.ResolveTypeName(element.ReturnType.GetGenericArguments()[0]);
                }
                else if (element.ReturnType.Name == "Task")
                {
                    result.ReturnType = new RtSimpleTypeName("void");
                }
            }

            result.ReturnType = new RtSimpleTypeName("Promise", new[] { result.ReturnType });

            var queryParams = element.GetParameters()
                .Where(param => param.GetCustomAttribute(typeof(FromBodyAttribute)) == null)
                .Select(param => string.Format("'{0}': {0}", param.Name));

            var formParamArray = element.GetParameters()
                .Where(param => param.GetCustomAttribute(typeof(FromBodyAttribute)) != null)
                .ToArray();

            if (formParamArray.Length > 1)
                throw new Exception($"Multiple [FromBody] declarations not supported. Source: {element.DeclaringType}.{element.Name}");

            var formParamString = formParamArray.Length == 0 ? "undefined" : formParamArray[0].Name;

            var paramsCode = "{ " + string.Join(", ", queryParams) + " }";

            if (hasFormData)
            {
                result.Body = new RtRaw(string.Format(functionTemplate, httpVerb, urlPath, paramsCode, formParamString));
            }
            else
            {
                result.Body = new RtRaw(string.Format(functionTemplate, httpVerb, urlPath, paramsCode));
            }

            result.IsAsync = true;

            return result;
        }
    }

}
