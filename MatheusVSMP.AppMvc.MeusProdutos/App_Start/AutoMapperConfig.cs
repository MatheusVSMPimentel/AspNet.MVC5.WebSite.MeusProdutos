using AutoMapper;
using MatheusVSMP.AppMvc.MeusProdutos.ViewModels;
using MatheusVSMP.Business.Models.Fornecedores;
using MatheusVSMP.Business.Models.Produtos;
using Microsoft.Ajax.Utilities;
using System;
using System.Linq;
using System.Reflection;

namespace MatheusVSMP.AppMvc.MeusProdutos.App_Start
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            var profiles = Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));

            return new MapperConfiguration(cfg => profiles.ForEach(e => cfg.AddProfile(Activator.CreateInstance(e) as Profile)));
        }
    }
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
        }
    }
}