using MatheusVSMP.Business.Core.Notificacoes;
using MatheusVSMP.Business.Core.Services;
using MatheusVSMP.Business.Models.Fornecedores.Interfaces;
using MatheusVSMP.Business.Models.Fornecedores.Validators;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MatheusVSMP.Business.Models.Fornecedores.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository, IEnderecoRepository enderecoRepository, INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Fornecedor fornecedor)
        {
            fornecedor.Endereco.Id = fornecedor.Id;
            fornecedor.Endereco.Fornecedor = fornecedor;
            if (!ExecutarValidacao(new FornecedorValidator(), fornecedor) || !ExecutarValidacao(new EnderecoValidator(), fornecedor.Endereco)) return;

            if (await FornecedorExistente(fornecedor)) return;

            await _fornecedorRepository.Adicionar(fornecedor);
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidator(), fornecedor)) return;

            if (await FornecedorExistente(fornecedor)) return;

            await _fornecedorRepository.Atualizar(fornecedor);
        }

        public async Task Remover(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterFornecedorProdutosEndereco(id);
            if (fornecedor.Produtos.Any()) { Notificar("Fornecedor possui produtos cadastros!"); return; }
            if (fornecedor.Endereco != null)
            {
                await _enderecoRepository.Remover(fornecedor.Endereco.Id);
            }
            await _fornecedorRepository.Remover(id);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidator(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        private async Task<bool> FornecedorExistente(Fornecedor fornecedor)
        {
            var fornecedorAtual = await _fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id);
            if (!fornecedorAtual.Any()) return false;

            Notificar("Já existe um fornecedor cadastrado com este documento informado.");
            return true;
        }

        public void Dispose()
        {
            _fornecedorRepository.Dispose();
            _enderecoRepository.Dispose();
        }

    }
}
