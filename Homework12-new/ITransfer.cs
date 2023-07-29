using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox_Homework12
{
    interface ITransfer<in T> where T:Bill
    {
        /// <summary>
        /// Перевод средств
        /// </summary>
        /// <param name="billFrom">Ссылка на счет отправителя</param>
        /// <param name="billTo">Ссылка на счет получателя</param>
        /// <param name="sum">Сумма</param>
        /// <returns>Результат операции</returns>
        bool Transfer(T billFrom, Bill billTo, decimal sum);
    }
}
