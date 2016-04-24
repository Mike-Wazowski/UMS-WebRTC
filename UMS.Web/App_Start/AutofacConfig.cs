using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using UMS.Database.DAL;
using UMS.Database.DAL.Implementations;

namespace UMS.Web
{
    public static class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterFilterProvider();

            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.Namespace.EndsWith(".Implementations"))
                .AsImplementedInterfaces();

            builder.RegisterType<UmsDbContext>().As<IUmsDbContext>().InstancePerHttpRequest();

            LoadAndRegisterAssemblies(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void LoadAndRegisterAssemblies(ContainerBuilder builder)
        {
            LoadAssemblies();
            RegisterAssemblies(builder);
        }

        private static void LoadAssemblies()
        {
            var assembliesNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach (var assemblyName in assembliesNames)
            {
                Assembly.Load(assemblyName);
            }
        }

        private static void RegisterAssemblies(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly != null)
                {
                    builder.RegisterAssemblyTypes(assembly)
                        .Where(x =>
                        {
                            if (x != null && x.Namespace != null)
                                return x.Namespace.EndsWith(".Implementations");
                            return false;
                        })
                        .AsImplementedInterfaces();
                }
            }
        }
    }
}