using System;
using IBL.BO;
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
    /// Interaction logic for DroneToList.xaml
    /// </summary>
    public partial class DronesListWindow : Window
    {
        IBL.IBL DronetoListWindewBL;
        public DronesListWindow(IBL.IBL blObject)
        {
            InitializeComponent();
            DronetoListWindewBL = blObject;
            DronesListView.ItemsSource = DronetoListWindewBL.GetAllDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneConditions));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Drone(DronetoListWindewBL,StatusSelector.SelectedIndex,WeightSelector.SelectedIndex,this).Show();
        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(WeightSelector.SelectedIndex!=-1)
            {
                DronesListView.ItemsSource = DronetoListWindewBL.GetAllDrones(dro => dro.Conditions == (DroneConditions)StatusSelector.SelectedItem&& dro.MaxWeight == (WeightCategories)WeightSelector.SelectedItem);
            }
            else
                DronesListView.ItemsSource = DronetoListWindewBL.GetAllDrones(dro => dro.Conditions == (DroneConditions)StatusSelector.SelectedItem);



        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StatusSelector.SelectedIndex!=-1)
            {
                DronesListView.ItemsSource = DronetoListWindewBL.GetAllDrones(dro => dro.Conditions == (DroneConditions)StatusSelector.SelectedItem && dro.MaxWeight == (WeightCategories)WeightSelector.SelectedItem);
            }
            else
                DronesListView.ItemsSource = DronetoListWindewBL.GetAllDrones(dro => dro.MaxWeight == (WeightCategories)WeightSelector.SelectedItem);


        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DroneToList d = (DroneToList)DronesListView.SelectedItem;
            new Drone(d, DronetoListWindewBL).Show();
        }
    }
}
