using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox_Homework12
{
    public class DepositBill : Bill, IDeposit<DepositBill>
    {

        #region Поля и свойства
        /// <summary>
        /// Тип счета
        /// </summary>
        public override string BillType { get; }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public DepositBill() : base()
        {
            this.BillType = "Депозитный счет";
        }
        #endregion


        public DepositBill? Deposit(int BillId, decimal Sum)
        {
            this.Balance += Sum;
            return this;
        }
    }
}
