﻿using BlApi;
using BO;
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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : Window
    {
        IBL bl=BlFactory.GetBl();
        public ListView(IBL bL)
        {

            InitializeComponent();
            bl = bL;
            switch (TCview.SelectedIndex)
            {
                case 0:
                    listOfDrones.ItemsSource = bl.GetAllDrones();
                    StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneConditions));
                    WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
                    break;
                default:
                    break;
            }
           
            
        }
        public void FilterByCombiBox()
        {
            if (WeightSelector.SelectedItem == null && StatusSelector.SelectedItem == null)
            {
                //droneToListListView.ItemsSource = bl.GetAllDrones();
                listOfDrones.ItemsSource = bl.GetAllDrones();
            }
            if (WeightSelector.SelectedIndex != -1 && StatusSelector.SelectedIndex != -1)
                listOfDrones.ItemsSource = bl.GetAllDrones(dro => dro.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedIndex && dro.Conditions == (BO.DroneConditions)StatusSelector.SelectedIndex);
            else if (WeightSelector.SelectedIndex != -1 && StatusSelector.SelectedIndex == -1)
                listOfDrones.ItemsSource = bl.GetAllDrones(dro => dro.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedIndex);
            else if (WeightSelector.SelectedIndex == -1 && StatusSelector.SelectedIndex != -1)
                listOfDrones.ItemsSource = bl.GetAllDrones(dro => dro.Conditions == (BO.DroneConditions)StatusSelector.SelectedIndex);


        }

        

        private void Clear1_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = null;
        }

        private void Clear2_Click(object sender, RoutedEventArgs e)
        {
            WeightSelector.SelectedItem = null;
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterByCombiBox();
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterByCombiBox();

        }

        private void listOfDrones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DroneToList d = (DroneToList)listOfDrones.SelectedItem;
            new Drone(d, bl).ShowDialog();
        }

        private void Badd_Click(object sender, RoutedEventArgs e)
        {
            switch(TCview.SelectedIndex)
            {
                case 0:
                    new Drone(bl).ShowDialog();
                    break;
                default:
                    break;
            }
        }

        private void listViewWindow_Activated(object sender, EventArgs e)
        {
            FilterByCombiBox();
        }
    }
}
