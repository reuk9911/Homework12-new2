using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Skillbox_Homework12
{
    public class BillRepository
    {
        private List<Bill> BillList;
        private List<IDeposit<Bill>> CovarDeposit;
        public string Message { get; private set; }
        public BillRepository()
        {
            BillList = new List<Bill>();
            CovarDeposit = BillList;
        }

        public ObservableCollection<T> GetBills(int ClientId)
        {
            ObservableCollection<T> bList = new ObservableCollection<T>();
            foreach (T b in BillList)
                if (b.ClientId == ClientId)
                    bList.Add(b);
            return bList;
        }
        public void AddBillGeneric(BillType bType, int ClientId)
        {
            switch (bType)
            {
                case BillType.DepositBill: BillList.Add(new DepositBill(ClientId) as T); break;
                case BillType.NonDepositBill: BillList.Add(new NonDepositBill(ClientId) as T); break;
                default: break;
            }
        }

        public bool CloseBillGeneric(int BillIdFrom, int BillIdTo)
        {
            int fromIndex = FindBillIndex(BillIdFrom);
            int toIndex = FindBillIndex(BillIdTo);
            if (fromIndex == -1 || toIndex == -1)
            {
                Message = "Счет с указанным Id не найден";
                return false;
            }
            if (BillList[fromIndex].Balance < 0.0m)
            {
                Message = "Нельзя закрыть счет с отрицательным балансом";
                return false;
            }
            else
            {
                if (BillList[fromIndex].Balance == 0.0m)
                {
                    BillList.RemoveAt(fromIndex);
                    Message = "Счет удален";
                    return true;
                }

                BillList[toIndex].Balance += BillList[fromIndex].Balance;
                BillList.RemoveAt(fromIndex);
                Message = "Счет удален";
                return true;
            }

        }
        public bool TransferGeneric(int BillIdFrom, int BillIdTo, decimal Sum)
        {
            int fromIndex = FindBillIndex(BillIdFrom);
            int toIndex = FindBillIndex(BillIdTo);
            if (fromIndex == -1 || toIndex == -1)
            {
                Message = "Счет с указанным Id не найден";
                return false;
            }
            if (BillList[fromIndex].Balance < Sum)
            {
                Message = "Недостаточно средств на счете";
                return false;
            }
            else
            {
                BillList[fromIndex].Balance -= Sum;
                BillList[toIndex].Balance += Sum;
                Message = "Перевод прошел успешно";
                return true;
            }

        }

        private int FindBillIndex(int Id)
        {
            for (int i = 0; i < BillList.Count; i++)
            {
                if (BillList[i].Id == Id)
                {
                    return i;
                }
            }
            return -1;
        }

        
    }
}