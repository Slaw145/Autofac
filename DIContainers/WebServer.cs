using Autofac;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.Lifetime;
using DIContainersLibrary;
using DIContainersLibrary.CharacterClasses;
using DIContainersLibrary.LoginPanel;

namespace DIContainers
{
    class WebServer
    {
        static GameServer gameServer;
        static IContainer container;

        static void Main(string[] args)
        {
            //register automatically all dependencies in this same project
            //var builder = new ContainerBuilder();
            //Assembly executingAssembly = Assembly.GetExecutingAssembly();

            //builder.RegisterAssemblyTypes(executingAssembly)
            //.AsSelf()
            //.AsImplementedInterfaces();

            container = ContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                gameServer = scope.Resolve<GameServer>();
            }

            //usual registering
            //var builder = new ContainerBuilder();
            //builder.RegisterType<LoginValidator>().As<ILoginValidator>();
            //builder.RegisterType<PasswordValidator>().As<IPasswordValidator>();
            //builder.RegisterType<CharacterSkillPoints>().As<ICharacterSkillPoints>();
            //builder.RegisterType<GameServer>();

            //container = builder.Build();

            //using (var scope = container.BeginLifetimeScope())
            //{
            //    gameServer = scope.Resolve<GameServer>();
            //}

            //container = ContainerConfig.Configure();

            //using (var scope = container.BeginLifetimeScope())
            //{
            //    gameServer = scope.Resolve<GameServer>();
            //}

            //Func<string, ILoginValidator> factory = container.Resolve<Func<string, ILoginValidator>>();

            //ILoginValidator loginValidator1 = factory("loginValidator");
            //loginValidator1.CountNumberOfCalling();

            //ILoginValidator loginValidator2 = factory("loginValidator");
            //loginValidator2.CountNumberOfCalling();

            //ILoginValidator singletonLoginValidator1 = factory("singletonLoginValidator");
            //singletonLoginValidator1.CountNumberOfCalling();

            //ILoginValidator singletonLoginValidator2 = factory("singletonLoginValidator");
            //singletonLoginValidator2.CountNumberOfCalling();

            //Lifetime objects
            //container = ContainerConfig.Configure();

            //using (var scope1 = container.BeginLifetimeScope())
            //{
            //    ICharacterSkillPoints characterSkillPoints1 = scope1.Resolve<ICharacterSkillPoints>();
            //    characterSkillPoints1.CountNumberOfCalling();

            //    ICharacterSkillPoints characterSkillPoints2 = scope1.Resolve<ICharacterSkillPoints>();
            //    characterSkillPoints2.CountNumberOfCalling();

            //    gameServer = scope1.Resolve<GameServer>();
            //}

            //using (var scope2 = container.BeginLifetimeScope())
            //{
            //    ICharacterSkillPoints characterSkillPoints1 = scope2.Resolve<ICharacterSkillPoints>();
            //    characterSkillPoints1.CountNumberOfCalling();

            //    ICharacterSkillPoints characterSkillPoints2 = scope2.Resolve<ICharacterSkillPoints>();
            //    characterSkillPoints2.CountNumberOfCalling();
            //}

            //Circular dependencies
            //container = ContainerConfig.Configure();

            //using (var scope = container.BeginLifetimeScope())
            //{
            //    FirstClass firstClass = scope.Resolve<FirstClass>();
            //    SecondClass secondClass = scope.Resolve<SecondClass>();
            //    Console.WriteLine(firstClass.GetNumber());
            //    Console.WriteLine(secondClass.GetNumber());
            //}

            //Register factory through delegate
            //var builder = new ContainerBuilder();
            //builder.RegisterType<DelegateClass>();
            //var iContainer = builder.Build();
            //var delegateClassFactory = iContainer.Resolve<DelegateClass.Factory>();
            //var delegateClass = delegateClassFactory("ABC");
            //Console.WriteLine(delegateClass.getSymbol());

            //Interceptors
            //container = ContainerConfig.Configure();

            //using (var scope = container.BeginLifetimeScope())
            //{
            //    gameServer = scope.Resolve<GameServer>();
            //}

            gameServer.ResolveInterfaces(container);

            bool ifUserIsLoginIn = LogIn();

            ICharacter createdCharacter = CreateCharacter();

            StartGame(createdCharacter, ifUserIsLoginIn);

            Shutdown();

            Console.ReadKey();
        }

        static void Shutdown()
        {
            container.Dispose();
        }

        static bool LogIn()
        {
            bool ifvalidate = gameServer.RegisterUser("assd12", "adasd123@");

            if (ifvalidate)
            {
                Console.WriteLine("Register user");
            }
            else
            {
                Console.WriteLine("Login or password are incorrect!");
            }

            return ifvalidate;
        }

        static ICharacter CreateCharacter()
        {
            ICharacter character = gameServer.CreateCharacter(new Barbarian());

            Console.WriteLine("Skill points after give out.");

            Console.WriteLine(character.Strength);
            Console.WriteLine(character.Stamina);

            return character;
        }

        static void StartGame(ICharacter character, bool ifvalidate)
        {
            bool ifGameIsStarted = gameServer.StartGame(character, ifvalidate);

            if (ifGameIsStarted)
            {
                Console.WriteLine("Start the game");
            }
            else
            {
                Console.WriteLine("Something went wrong!");
            }
        }
    }

    public class DelegateClass
    {
        public delegate DelegateClass Factory(string @string);

        public DelegateClass(string @string)
        {
            this.@string = @string;
        }

        public string @string { get; private set; }

        public string getSymbol()
        {
            return @string;
        }
    }

    class FirstClass
    {
        public int a = 1;

        public SecondClass Dependency1 { get; set; }

        public FirstClass(SecondClass Dependency1)
        {
            this.Dependency1 = Dependency1;
        }

        public int GetNumber()
        {
            return Dependency1.b;
        }
    }

    class SecondClass
    {
        public int b = 13;

        public FirstClass Dependency2 { get; set; }

        //public SecondClass(FirstClass Dependency2)
        //{
        //    this.Dependency2 = Dependency2;
        //}

        public int GetNumber()
        {
            return Dependency2.a;
        }
    }
}
