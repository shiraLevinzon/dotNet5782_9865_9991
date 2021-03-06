 using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
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
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal BlApi.IBL blObject = BlFactory.GetBl();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void listofbasestation_Click(object sender, RoutedEventArgs e)
        {

        }
        private void PackIcon_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {

        }
        private void enter_Click(object sender, RoutedEventArgs e)
        {
            if(Password.Password=="1234")
            {
                new ListView(blObject).ShowDialog();
            }
            else
            {
                MessageBox.Show("The password is incorrect");
            }
        }

        private void enterCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool flag = true;
                BO.User u= blObject.GetUser(Convert.ToInt32(idTEXTBOX.Text));
                if (passwordCustomer.Password == u.Password)
                {
                    BO.Customer c = blObject.GetCustomer(u.Id);
                    new CostumerWindow(c,blObject,flag).ShowDialog();
                }
                else
                {
                    MessageBox.Show("The password is incorrect");
                }
            }
            catch (BO.MissingIdException ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void nerUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.User user = new BO.User()
                {
                    Id = Convert.ToInt32(idTextBox.Text),
                    Name = nameTextBox.Text,
                    Password = passwordTextBox.Text,
                    Phone = phoneTextBox.Text
                };

                blObject.AddUser(user);
                MessageBox.Show("add user sucsess");
                nerUser.DataContext = null;
               
            }
            catch (BO.DuplicateIdException ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TimeSpan t = new TimeSpan(0);

            foreach (var item in blObject.GetAllDrones(dro => dro.Conditions == (BO.DroneConditions)0))
            {
                blObject.ReleaseDroneFromCharging(item.ID, t);
            }
        }
    }
}
