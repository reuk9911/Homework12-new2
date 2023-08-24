using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox_Homework12
{
    public class Bill : IDisposable, INotifyPropertyChanged
    {
        #region Поля и Свойства
        

        /// <summary>
        /// Id клиента
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Баланс счета
        /// </summary>
        public decimal Balance 
        {
            get => this.balance;
            set
            {
                if (balance != value)
                {
                    balance = value;
                    OnPropertyChanged("Balance");
                }
            }
        }

        /// <summary>
        /// Тип счета
        /// </summary>
        public virtual string BillType { get; }

        /// <summary>
        /// Статический генератор Id
        /// </summary>
        private static IdGenerator IdGen;

        /// <summary>
        /// Баланс счета
        /// </summary>
        private decimal balance; 

        #endregion

        #region Конструкторы

        /// <summary>
        /// Статический конструктор
        /// </summary>
        static Bill()
        {
            IdGen = new IdGenerator("Bill");
        }
        
        
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Bill()
        {
            this.BillType = "Bill";
            this.Balance = 0.0m;
            this.Id = IdGen.GetNewId();
        }
        #endregion

        #region Методы


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
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
