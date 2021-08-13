using CCMvNext.BusinessLogic.DTO;
using CCMvNext.Controllers;
using CCMvNext.Infrastructure.ReinforcedTypings;
using CCMvNext.Models;
using CCMvNext.Models.CookieConsent;
using Reinforced.Typings.Fluent;
using System;
using System.Collections.Generic;

namespace CCMvNext.Configuration
{
    public static class TypingsConfig
    {
        /// <summary>
        /// Configures <see cref="Reinforced.Typings"/> C# -> TypeScript converter.
        /// </summary>
        /// <param name="builder"></param>
        public static void Configure(ConfigurationBuilder builder)
        {
            builder.Global(x => x.UseModules().CamelCaseForMethods().CamelCaseForProperties());

            builder.ExportAsInterfaces(Models(), a => a.WithPublicProperties().ExportTo(@"models.ts"));

            builder.ExportAsInterface<Entity>().WithProperty(x => x.Id, cfg => cfg.ForceNullable()).ExportTo(@"models.ts");

            //builder.ExportAsEnums(Enums(), a => a.ExportTo(@"models.ts"));
            builder.ExportAsClasses(AngularApi(),
                c =>
                {
                    c.AddImport("{ HttpClient }", "@angular/common/http")
                    .AddImport("{ Injectable }", "@angular/core")
                    .AddImport("{ ApiInvokeService} ", "./api-invoke.service")
                    .ExportTo(@"api.ts")
                    .WithCodeGenerator<Angular2ApiGenerator>();
                });
        }

        /// <summary>
        /// Lists Controller classes to export as Angular services.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Type> AngularApi()
        {
            yield return typeof(CookieConsentsController);
            yield return typeof(CookieConsentsReportingController);
        }

        /// <summary>
        /// Lists enums to export as TypeScript Enum.
        /// </summary>
        /// <returns></returns>
        //private static IEnumerable<Type> Enums()
        //{ 

        //}

        /// <summary>
        /// Lists model classes to export as TypeScript Interfaces.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Type> Models()
        {
            yield return typeof(CookieConsentRecord);
            yield return typeof(ConsentByDay);
        }
    }
}
