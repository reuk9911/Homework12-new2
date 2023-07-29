using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox_Homework12
{
    class VipClient: Client
    {
        #region Поля и свойства

        /// <summary>
        /// Тип клиента
        /// </summary>
        public override string ClientType { get; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор без параметров
        /// </summary>

        public VipClient() : base()
        {
            this.ClientType = "Vip клиент";
        }
        
        #endregion
    }
}
