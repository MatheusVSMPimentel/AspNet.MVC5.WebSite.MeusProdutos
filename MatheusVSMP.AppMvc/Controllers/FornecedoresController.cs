using MatheusVSMP.Business.Models.Fornecedores;
using MatheusVSMP.Business.Models.Fornecedores.Interfaces;
using MatheusVSMP.Business.Models.Fornecedores.Services;
using MatheusVSMP.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MatheusVSMP.AppMvc.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly IFornecedorService _fornecedorService;

        public FornecedoresController()
        {
            _fornecedorService = new FornecedorService(new FornecedorRepository(), new EnderecoRepository());
        }

        // GET: Fornecedores
        public async  Task<ActionResult> Index()
        {
            var fornecedor = new Fornecedor() { Nome = nameof(MatheusVSMP), Documento = "18167101053", Endereco = new Endereco() { Cep = "44024057", Bairro = "Jatiuca", Cidade = "Mangabeira", Complemento = string.Empty, Estado = "Bahia",Logradouro = "Rua teste", Numero = "40" }, TipoFornecedor = TipoFornecedor.PessoaFisica, Ativo= true };
            await _fornecedorService.Adicionar(fornecedor);
            return new EmptyResult();
        }
    }
}