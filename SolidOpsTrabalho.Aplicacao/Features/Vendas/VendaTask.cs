﻿using SolidOpsTrabalho.Dominio.Features.Vendas;
using SolidOpsTrabalho.Infra.CSV;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace SolidOpsTrabalho.Aplicacao.Features.Vendas
{
    public class VendaTask
    {
        public CSVService _CSVService;
        private string CaminhoPastaDeVendas;
        private string CaminhoPastaDeVendasValidas;
        private string CaminhoPastaDeVendasInvalidas;
        IVendaService _vendaService;
        string NomeDoArquivo;
        private string CaminhoDoArquivoSendoAnalizado;

        public VendaTask(IVendaService vendaService)
        {
            _vendaService = vendaService;
            CaminhoPastaDeVendas = ConfigurationManager.AppSettings["CaminhoPastaVendas"];
            CaminhoPastaDeVendasValidas = ConfigurationManager.AppSettings["CaminhoPastaVendasValidas"];
            CaminhoPastaDeVendasInvalidas = ConfigurationManager.AppSettings["CaminhoPastaVendasInvalidas"];
            
        }

        public void TaskLeitura(string caminho, string nomeDoArquivo)
        {
            this.NomeDoArquivo = nomeDoArquivo;
            _CSVService = new CSVService();
            CaminhoDoArquivoSendoAnalizado = caminho;
            var leitura = Task.Run(() => LerArquivo(caminho));
            leitura.Wait();
            Console.WriteLine(nomeDoArquivo);
        }

        public void TaskValidarVenda(Venda venda)
        {
            var validar = Task.Run(() => ValidarVenda(venda));
            validar.Wait();
        }

        public void TaskMoverInvalida(Venda venda)
        {
            var invalida = Task.Run(() => MoverParaDiretorioDeVendasInvalidas());
            invalida.Wait();
        }

        public void TaskMoverValida(Venda venda)
        {
            var valida = Task.Run(() => MoverParaDiretorioDeVendasValidas());
            _vendaService.Adicionar(venda);
            valida.Wait();
        }

        private void ValidarVenda(Venda venda)
        {

                bool x = venda.Validar();
                int i = 0;
                while (!x && i < 3)
                {
                    x = venda.Validar();
                    i++;
                }
                if (!x)
                    TaskMoverInvalida(venda);
                else
                    TaskMoverValida(venda);
        }

        private void LerArquivo(string caminho)
        {
            Venda venda = new Venda();
            try
            {
               var vendas = _CSVService.LeiturasDeDados(caminho);

               if (vendas.Count == 1)
                {                
                    venda = vendas.LastOrDefault();
                }
            }
            catch (Exception)
            {
                venda = new Venda();
            }
                     
            TaskValidarVenda(venda);
            
        }

        private void MoverParaDiretorioDeVendasValidas()
        {
            MoverArquivo(CaminhoPastaDeVendasValidas);
        }

        private void MoverParaDiretorioDeVendasInvalidas()
        {
            MoverArquivo(CaminhoPastaDeVendasInvalidas);
        }

        private void MoverArquivo(string caminho)
        {
            var caminhoVendas = new DirectoryInfo(CaminhoDoArquivoSendoAnalizado);
            string novo = DateTime.Now.Ticks.ToString() ;
            caminho = Path.Combine(caminho, novo + " - " + NomeDoArquivo);

            File.Move(CaminhoDoArquivoSendoAnalizado, caminho);

        }
    }
}
