﻿using SolidOpsTrabalho.Dominio.Features.Vendas;
using SolidOpsTrabalho.Infra.CSV;
using SolidOpsTrabalho.Infra.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOpsTrabalho.Aplicacao.Features.Vendas
{
    public class VendaService : IVendaService
    {
        public IVendaRepository _repository;

        public VendaService(IVendaRepository repository)
        {
            _repository = repository;
        }

        public void Adicionar(Venda venda)
        {
            venda.Validar();
            _repository.Adicionar(venda);
        }
        public List<string> ListarArquivos(string caminho)
        {
            return ScannerDePasta.ListarArquivos(caminho);
        }

    }
}
