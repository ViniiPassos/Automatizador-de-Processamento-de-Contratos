using AutomatizadorDeProcessamentoDeContratos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatizadorDeProcessamentoDeContratos.Services
{
    class ContractService
    {

        private IOnlinePaymentService _onlinePaymentService;

        public ContractService(IOnlinePaymentService onlinePaymentService)
        {
            _onlinePaymentService = onlinePaymentService;
        }

        public void ProcessContract(Contract contract, int months)
        {
            double basicQuota = contract.TotalValue / months; // divide o valor total do contrato pelos meses
            for (int i = 1; i <= months; i++)
            {
                DateTime date = contract.Date.AddMonths(i);
                double updatedQuota = basicQuota + _onlinePaymentService.Interest(basicQuota, i); //200 + 1% * 1
                double fullQuota = updatedQuota + _onlinePaymentService.PaymentFee(updatedQuota); // 200 + 2%
                contract.AddInstallment(new Installment(date, fullQuota)); // instanciamento
            }
        }
    }
}

