using MediatR;
using Ninject.Modules;
using Ninject.Extensions.Conventions;

namespace BoxUserMgmt.DataAccess
{
    internal class DataAccessNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IConnectionStringRetriever>().To<DefaultConnectionStringRetriever>().InSingletonScope();
            Kernel.Bind<IConnectionManager>().To<DefaultConnectionManager>().InSingletonScope();

            Bind<IMediator>().To<Mediator>().InSingletonScope();


            Kernel.Bind(
                x => x.FromThisAssembly()
                    .IncludingNonePublicTypes()
                    .SelectAllClasses()
                    .InheritedFrom(typeof (IRequestHandler<,>))
                    .BindAllInterfaces());

            Kernel.Bind(
                x => x.FromThisAssembly()
                    .IncludingNonePublicTypes()
                    .SelectAllClasses()
                    .InheritedFrom(typeof (IAsyncRequestHandler<,>))
                    .BindAllInterfaces());
        }
    }

}
