using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using hbotService.DataObjects;
using hbotService.Models;
using System.Data.Entity.Migrations;
using hbotService.Migrations;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using Autofac;
using hbotService.Services;

namespace hbotService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;


            // Set default and null value handling to "Include" for Json Serializer
            config.Formatters.JsonFormatter.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.Remove(config.Formatters.XmlFormatter);


            RegisterDependencies();

            var migrator = new DbMigrator(new Configuration());
            migrator.Update();
        }


        public static void RegisterDependencies()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options, (configuration, builder) =>
            {
                //Register API controllers
                //builder.RegisterApiControllers(typeof(UserController).Assembly);

                /*Register Libs*/
                builder.RegisterType<EventService>().As<IEventService>();

                /*Register ObjectContexts*/
                //builder.RegisterType<MobileServiceContext>().As<DbContext>().InstancePerDependency();

                /*Register DataRepository Here*/
                //builder.RegisterGeneric(typeof(DataRepository<>)).As(typeof(IDataRepository<>));
            }));

        }
    }
}

