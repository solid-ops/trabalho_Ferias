﻿using NUnit.Framework;
using SolidOpsTrabalho.Infra.WindowsServices.Features.Vendas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOpsTrabalho.Infra.WindowsServices.Testes.Features.Vendas
{
    [TestFixture]
    class ArquivosVendasServiceTestes
    {

        [Test]
        public void DeveAbrirERodarOServico()
        {
            //var servico = new ArquivosVendasService();
            //servico.Iniciar();
            var ArquivosVendasService = new ArquivosVendasService();
            //var caminho = ArquivosVendasService.returnPath();
            Process.Start(ArquivosVendasService.returnPath() + "SolidOpsTrabalho.Infra.WindowsServices.exe");
            var process = Process.GetCurrentProcess();
        }

    }
}
