using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox_Homework12
{
    public class NonDepositBill : DepositBill, IDeposit<NonDepositBill>
    {
        public override string BillType { get; }

        public NonDepositBill() : base()
        {
            this.BillType = "Не депозитный счет";
        }
        public new NonDepositBill? Deposit(int BillId, decimal Sum)
        {
            this.Balance += Sum;
            return this;
        }
    }
}
