using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Skillbox_Homework12
{
    public class Client: IDisposable, IDeposit<Bill>, ITransfer<Bill>
    {
        #region Поля и Свойства
        /// <summary>
        /// Id клиента
        /// </summary>
        public int Id { get; private set; }
        
        /// <summary>
        /// Коллекция счетов клиента
        /// </summary>
        public ObservableCollection<Bill> Bills { get; }

        /// <summary>
        /// Сообщения из методов для подписчиков
        /// </summary>
        public string Message { get; protected set; }
        
        /// <summary>
        /// Имя Клиента
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип Клиента
        /// </summary>
        public virtual string ClientType { get; }

        /// <summary>
        /// Генератор Id
        /// </summary>
        private static IdGenerator IdGen;
        #endregion

        #region Конструкторы

        /// <summary>
        /// Статический конструктор
        /// </summary>
        static Client()
        {
            IdGen = new IdGenerator("Client");
        }

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Client()
        {
            this.ClientType = "Физическое лицо";
            this.Message = "";
            this.Bills = new ObservableCollection<Bill>();
            this.Id = IdGen.GetNewId();
            this.Name = "";

        }

        //public Client(int id)
        //{
        //    this.ClientType = "Физическое лицо";
        //    this.Message = "";
        //    this.Bills = new ObservableCollection<Bill>();
        //    Id = id;
        //    IdGen.UsedIds.Add(id);
        //    this.Name = "";
        //}
        #endregion

        #region Методы

        /// <summary>
        /// Открывает новый счет заданного типа
        /// </summary>
        /// <param name="bType">Тип счета</param>
        public void OpenBill(EBillType bType)
        {
            switch (bType)
            {
                case EBillType.DepositBill: Bills.Add(new DepositBill() as Bill); break;
                case EBillType.NonDepositBill: Bills.Add(new NonDepositBill() as Bill); break;
                default: break;
            }
        }

        /// <summary>
        /// Закрывает счет
        /// </summary>
        /// <param name="BillId">Id счета</param>
        /// <returns></returns>
        public bool CloseBill(int BillId)
        {
            int index = FindBillIndex(BillId);
            if (index == -1)
            {
                Message = "Счет с указанным Id не найден";
                return false;
            }
            if (Bills[index].Balance < 0.0m)
            {
                Message = "Нельзя закрыть счет с отрицательным балансом";
                return false;
            }
            else
            {
                Bills[index].Dispose();
                Bills.RemoveAt(index);
                Message = "Счет удален";
                return true;
            }

        }


        /// <summary>
        /// Вносит средства на счет
        /// </summary>
        /// <param name="BillId">Id счета</param>
        /// <param name="Sum">Сумма</param>
        /// <returns>Возвращает счет, на который вносятся деньги</returns>
        public Bill? Deposit(int BillId, decimal Sum)
        {
            if (Bills == null) return null;
            int k = -1;
            for (int i = 0; i < Bills.Count; i++)
            {
                if (Bills[i].Id == BillId)
                {
                    k = i;
                    break;
                }
            }
            if (k == -1) return null;
            else
            {
                Bills[k].Balance += Sum;
                if (Bills[k] is DepositBill) return (DepositBill)Bills[k];
                return Bills[k];
            }

        }

        /// <summary>
        /// Переводит средства
        /// </summary>
        /// <param name="BillFrom">Счет, с котрорго отправляются деньги</param>
        /// <param name="BillTo">Счет зачисления</param>
        /// <param name="sum">Сумма</param>
        /// <returns>результат выполнения переавода</returns>
        public bool Transfer(Bill BillFrom, Bill BillTo, decimal sum)
        {
            if (BillFrom.Balance < sum)
            {
                this.Message = "Перевод не прошел! Не достаточно средств!";
                return false;
            }
            BillFrom.Balance -= sum;
            BillTo.Balance += sum;
            this.Message = "Перевод прошел успешно!";
            return true;
        }

        /// <summary>
        /// Ищет индекс счета в Bills по Id 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        private int FindBillIndex(int Id)
        {
            for (int i = 0; i < Bills.Count; i++)
            {
                if (Bills[i].Id == Id)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Деструктор
        /// </summary>
        public void Dispose()
        {
            IdGen.UsedIds.Remove(this.Id);
        }
        #endregion
    }
}
