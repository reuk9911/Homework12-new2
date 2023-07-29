using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections;
using Homework12_new;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace Skillbox_Homework12
{
    
    public class ClientRepository<T> : IEnumerable<T> where T : Client, new()
    {
        #region Свойства
        public ObservableCollection<T>? ClientList { get; private set; }
        #endregion

        #region Конструкторы
        public ClientRepository()
        {
            this.ClientList = new ObservableCollection<T>();
        }
        #endregion

        #region Методы
        /// <summary>
        /// Сериализация
        /// </summary>
        public void SerializeJson()
        {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText("Serialization.txt", json);
        }

        //public static bool DeserializeJson(string path, out ClientRepository<T>? res)
        //{
        //    if (!File.Exists(path))
        //    {
        //        res = null;
        //        return false;
        //    }
        //    string json = File.ReadAllText(path);

        //    res = JsonConvert.DeserializeObject<ClientRepository<T>>(json);
        //    return true;
        //}

        /// <summary>
        /// Десериализация
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Возвращает результат выполнения десериализации - true или  false</returns>
        public bool DeserializeJson(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            string json = File.ReadAllText(path);

            ClientList = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
            return true;
        }


        /// <summary>
        /// возвращает счет по id
        /// </summary>
        /// <param name="id">id счета</param>
        /// <returns>счет</returns>
        public Bill? FindBillById(int id)
        {
            Bill? bill = null;
            foreach (Client c in ClientList)
            {
                foreach (Bill b in c.Bills)
                {
                    if (b.Id == id)
                    {
                        bill = b;
                        break;
                    }
                }
            }
            return bill;
        }


        /// <summary>
        /// Добавляет уже созданного клиента в репозиторий
        /// </summary>
        /// <param name="t">Клиент</param>
        public void AddClient(T t) 
        {
            ClientList.Add(t);
        }

        /// <summary>
        /// Создает и добавляет нового клиента
        /// </summary>
        /// <param name="Name">Имя клиента</param>
        /// <param name="clientType">Тип клиента</param>
        public void AddNewClient(string Name, EClientType clientType)
        {
            switch (clientType)
            {
                case EClientType.Client:
                    T newClient = new T();
                    ClientList.Add(newClient);
                    newClient.Name = Name;
                    break;

                case EClientType.VipClient:
                    VipClient newVipClient = new VipClient();
                    newVipClient.Name = Name;
                    ClientList.Add((T)(object)newVipClient);
                    break;

                case EClientType.LegalPerson:
                    LegalPerson newLegalPerson = new LegalPerson();
                    newLegalPerson.Name = Name;
                    ClientList.Add((T)(object)newLegalPerson);
                    break;

                default: break;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var e in ClientList)
            {
                yield return e;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)ClientList).GetEnumerator();
        }
        #endregion
    }
}
