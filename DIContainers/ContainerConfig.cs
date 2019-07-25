using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DIContainersLibrary;
using DIContainersLibrary.LoginPanel;
using Autofac.Extras.DynamicProxy2;

namespace DIContainers
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            var dataAccess = Assembly.LoadFrom(nameof(DIContainersLibrary));

            builder.RegisterAssemblyTypes(dataAccess)
                .AsSelf()
                .AsImplementedInterfaces();

            builder.RegisterType<GameServer>();

            //create a singleton
            //builder.RegisterType<CharacterSkillPoints>().As<ICharacterSkillPoints>().SingleInstance();
            //Create new object
            //builder.RegisterType<CharacterSkillPoints>().As<ICharacterSkillPoints>().InstancePerDependency();
            //In each scope new instance
            //builder.RegisterType<CharacterSkillPoints>().As<ICharacterSkillPoints>().InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(dataAccess)
            //    .Where(t => t.Namespace.Contains("LoginPanel"))
            //    .AsImplementedInterfaces();

            //Circular dependencies
            //builder.RegisterType<FirstClass>()
            //  .InstancePerLifetimeScope()
            //  .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            //builder.RegisterType<SecondClass>()
            //  .InstancePerLifetimeScope()
            //  .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //Registering types with Func
            //builder.RegisterType<LoginValidator>().As<ILoginValidator>().Named<ILoginValidator>("loginValidator");
            //builder.RegisterType<LoginValidator>().As<ILoginValidator>().SingleInstance().Named<ILoginValidator>("singletonLoginValidator");
            //builder.RegisterType<PasswordValidator>().As<IPasswordValidator>().Named<IPasswordValidator>("passwordValidator");
            //builder.RegisterType<CharacterSkillPoints>().As<ICharacterSkillPoints>().Named<ICharacterSkillPoints>("characterSkillPoints");

            //builder.RegisterType<GameServer>();

            //builder.Register<Func<string, ILoginValidator>>(delegate (IComponentContext context)
            //{
            //    IComponentContext cc = context.Resolve<IComponentContext>();

            //    return cc.ResolveNamed<ILoginValidator>;
            //});

            //Interceptors
            //builder.RegisterType<CharacterSkillPoints>().As<ICharacterSkillPoints>().EnableInterfaceInterceptors();
            //builder.RegisterType<PasswordValidator>().As<IPasswordValidator>().EnableInterfaceInterceptors();
            //builder.RegisterType<LoginValidator>().As<ILoginValidator>().EnableInterfaceInterceptors();

            //Configure interception types in IoC Container
            //builder.Register(x => new CallLogger(Console.Out));

            return builder.Build();
        }
    }
}
