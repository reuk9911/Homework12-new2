using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox_Homework12
{
    public class IdGenerator
    {
        #region Поля и свойства
        /// <summary>
        /// Список использованных Id
        /// </summary>
        public List<int> UsedIds { get; set; }

        /// <summary>
        /// Имя класса-владельца генератора Id
        /// </summary>
        public string Owner { get; }
        #endregion

        #region Конструкторы
        public IdGenerator(string owner)
        {
            UsedIds = new List<int>();
            Owner = owner;
        }
        #endregion

        #region Методы
        public int GetNewId()
        {
            int i = 0;
            do
            {
                i++;
            }
            while (UsedIds.Contains(i));
            UsedIds.Add(i);
            return i;
        }
        #endregion



    }
}
