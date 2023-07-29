using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox_Homework12
{
    public interface IDeposit<out T> where T : Bill
    {
        /// <summary>
        /// Метод для пополнения счета
        /// </summary>
        /// <param name="BillId"> Id счета</param>
        /// <param name="Sum">сумма</param>
        /// <returns>Счет, на который вносятся деньги</returns>
        T? Deposit(int BillId, decimal Sum);
    }
}
