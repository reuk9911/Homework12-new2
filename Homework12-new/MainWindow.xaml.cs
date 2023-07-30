using Skillbox_Homework12;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Homework12_new
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ClientRepository<Client> Clients;
        public MainWindow()
        {
            InitializeComponent();
            Clients = new ClientRepository<Client>();
            //ClientRepository<Client>.DeserializeJson("Serialization.txt", out Clients); 

            // Раскомментировать
            Clients.DeserializeJson("Serialization.txt");
            // *** Раскомментировать

            // Закомментировать
            //Client newClient = new VipClient();
            //Client newClient2 = new LegalPerson();

            //newClient.OpenBill(EBillType.DepositBill);
            //newClient2.OpenBill(EBillType.NonDepositBill);

            //newClient.Name = "Василий";
            //newClient2.Name = "Петька";


            //Clients.AddClient(newClient);
            //Clients.AddClient(newClient2);

            //newClient.Deposit(1, 500.0m);
            //newClient.Transfer(newClient.Bills[0] as DepositBill, newClient2.Bills[0], 300.0m);
            // *** Закомментировать

            ClientsViewGrid.ItemsSource = Clients.ClientList;

        }

        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxClientName.Text == "")
            {
                MessageBox.Show("Введите имя клиента", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
                return;
            }
            else
            {
                if (rbClient.IsChecked == true)
                {
                    Client newClient = new Client();
                    newClient.Name = TextBoxClientName.Text;
                    Clients.AddClient(newClient);
                }
                if (rbVipClient.IsChecked == true)
                {
                    Client newClient = new VipClient();
                    newClient.Name = TextBoxClientName.Text;
                    Clients.AddClient(newClient);
                }
                if (rbLegalPerson.IsChecked == true)
                {
                    Client newClient = new LegalPerson();
                    newClient.Name = TextBoxClientName.Text;
                    Clients.AddClient(newClient);
                }
            }
        }

        private void ClientsViewGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Bill b = (ClientsViewGrid.SelectedItem);
            BillsViewGrid.ItemsSource = ((Client)ClientsViewGrid.SelectedItem).Bills;
        }

        private void ButtonCloseBill_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsViewGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
                return;
            }

            if (BillsViewGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите счет", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
                return;
            }
            Bill bill = (Bill)BillsViewGrid.SelectedItem;
            Client client = (Client)ClientsViewGrid.SelectedItem;
            int id = bill.Id;
            client.CloseBill(id);
        }

        private void ButtonOpenBill_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsViewGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
                return;
            }
            if (rbDepositBill.IsChecked == true)
            {
                ((Client)ClientsViewGrid.SelectedItem).OpenBill(EBillType.DepositBill);
            }
            if (rbNonDepositBill.IsChecked == true)
            {
                ((Client)ClientsViewGrid.SelectedItem).OpenBill(EBillType.NonDepositBill);
            }

        }

        private void ButtonDeposit_Click(object sender, RoutedEventArgs e)
        {
            if (BillsViewGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите счет", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
                return;
            }
            if (TextBoxDepositSum.Text == "")
            {
                MessageBox.Show("Введите сумму", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
                return;
            }

            decimal sum;
            if (decimal.TryParse(TextBoxDepositSum.Text, out sum) == false)
            {
                MessageBox.Show("Введите сумму", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
                return;
            }
            Bill bill = (Bill)BillsViewGrid.SelectedItem;
            Client client = (Client)ClientsViewGrid.SelectedItem;
            client.Deposit(bill.Id, sum);
        }

        private void ButtonTransfer_Click(object sender, RoutedEventArgs e)
        {
            if (BillsViewGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите счет", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
                return;
            }

            int billToId;
            if (Int32.TryParse(TextBoxBillToId.Text, out billToId) == false)
            {
                MessageBox.Show("Укажите Id счета зачисления", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
                return;
            }

            decimal sum;
            if (decimal.TryParse(TextBoxTransferSum.Text, out sum) == false)
            {
                MessageBox.Show("Введите сумму", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
                return;
            }

            Bill? b = Clients.FindBillById(billToId);
            if (b == null)
            {
                MessageBox.Show("Счет с указанным Id не найден", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
                return;
            }
            else
            {
                Client c = (Client)ClientsViewGrid.SelectedItem;
                if (c.Transfer((Bill)BillsViewGrid.SelectedItem, b, sum) == false)
                {
                    MessageBox.Show(c.Message, "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
                    return;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Clients.SerializeJson();
        }
    }
}
