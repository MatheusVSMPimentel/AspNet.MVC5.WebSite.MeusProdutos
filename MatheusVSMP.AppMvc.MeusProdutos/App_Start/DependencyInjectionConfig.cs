using MatheusVSMP.Business.Core.Notificacoes;
using MatheusVSMP.Business.Models.Fornecedores.Interfaces;
using MatheusVSMP.Business.Models.Fornecedores.Services;
using MatheusVSMP.Business.Models.Produtos.Interfaces;
using MatheusVSMP.Business.Models.Produtos.Services;
using MatheusVSMP.Infra.Data.Context;
using MatheusVSMP.Infra.Data.Repository;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;

namespace MatheusVSMP.AppMvc.MeusProdutos.App_Start
{
    public class DependencyInjectionConfig
    {
        public static void RegisterDIContainer()
        {
            Container webRequestContainer = new Container();
            webRequestContainer.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(webRequestContainer);

            webRequestContainer.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            webRequestContainer.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(webRequestContainer));
        }

        private static void InitializeContainer(Container container) 
        {
            container.Register<IProdutoRepository, ProdutoRepository>(Lifestyle.Scoped);
            container.Register<IFornecedorRepository, FornecedorRepository>(Lifestyle.Scoped);
            container.Register<IEnderecoRepository, EnderecoRepository>(Lifestyle.Scoped);
            container.Register<IProdutoService, ProdutoService>(Lifestyle.Scoped);
            container.Register<IFornecedorService, FornecedorService>(Lifestyle.Scoped);
            container.Register<INotificador, Notificador>(Lifestyle.Scoped);
            container.Register<SqlServerContext>(Lifestyle.Scoped);

            container.RegisterSingleton(() => AutoMapperConfig.GetMapperConfiguration().CreateMapper(container.GetInstance));
        }
    }
}