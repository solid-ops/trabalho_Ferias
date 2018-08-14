﻿using SolidOpsTrabalho.Infra.CSV;
using SolidOpsTrabalho.Infra.CSV.Features.Vendas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidOpsTrabalho.Infra.Utils;
using SolidOpsTrabalho.Aplicacao.Features.Vendas;

namespace teste
{
    class Program
    {
        static void Main(string[] args)
        {
            CSVService a = new CSVService();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folderPath = Path.Combine(desktop, "testeCSV/");
            int x = 0;
            a.GerarMassaDados(folderPath, 4);
            string filePath = Path.Combine(desktop, "testeCSV/Venda" + x++ + ".csv");
            var s = a.LeiturasDeDados(filePath);
            foreach (var item in s)
            {
                Debug.Write(item.Id);
            }

            VendaTask vendaTask = new VendaTask(filePath);

            
        }
    }
}
