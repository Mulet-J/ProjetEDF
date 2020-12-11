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

namespace ProjetEDFWPF
{
    /// <summary>
    /// Logique d'interaction pour wdAdmin.xaml
    /// </summary>
    public partial class wdAdmin : Window
    {
        edfEntities gst;
        public wdAdmin(edfEntities unGst)
        {
            InitializeComponent();
            gst = unGst;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lstControleurs.ItemsSource = gst.controleur.ToList();
        }

        private void lstControleurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstClients.ItemsSource = null;
            if(lstControleurs.SelectedItem != null)
            {
                int test = (lstControleurs.SelectedItem as controleur).id; //ne marche pas sans cette étape pour une quelconque raison
                var querry = from client in gst.client
                             where client.idcontroleur == test
                             select new clientCalc
                             {
                                 id = client.identifiant,
                                 nom = client.nom,
                                 prenom = client.prenom,
                                 ancienReleve = client.ancienReleve,
                                 dernierReleve = client.dernierReleve,
                                 diffReleve = client.dernierReleve - client.ancienReleve
                             };
                lstClients.ItemsSource = querry.ToList();
            }
        }

        private void btnInsControleur_Click(object sender, RoutedEventArgs e)
        {
            if(txtNomControleur.Text == "")
            {
                MessageBox.Show("Merci d'entrer le nom du contrôleur");
            }
            else if(txtPrenomControleur.Text == "")
            {
                MessageBox.Show("Merci d'entrer le prénom du contrôleur");
            }
            else
            {
                controleur unCtrl = new controleur()
                {
                    nom = txtNomControleur.Text,
                    prenom = txtPrenomControleur.Text,
                    id = gst.controleur.ToList().Last().id + 1,
                    statut = "ctrl",
                    login = txtNomControleur.Text.Substring(0, 1).ToLower() + txtPrenomControleur.Text.Substring(0, 1).ToLower(),
                    mdp = txtNomControleur.Text.Substring(0, 1).ToLower() + txtPrenomControleur.Text.Substring(0, 1).ToLower() + "123"
                };
                gst.controleur.Add(unCtrl);
                gst.SaveChanges();
                lstControleurs.ItemsSource = null;
                lstControleurs.ItemsSource = gst.controleur.ToList();
            }
        }

        private void btnInsClient_Click(object sender, RoutedEventArgs e)
        {
            if(txtNomClient.Text == "")
            {
                MessageBox.Show("Merci d'entrer le nom du client");
            }
            else if (txtPrenomClient.Text == "")
            {
                MessageBox.Show("Merci d'entrer le nom du client");
            }
            else if(lstControleurs.SelectedItem == null)
            {
                MessageBox.Show("Merci de choisir un contrôleur");
            }
            else
            {
                client unClient = new client()
                {
                    ancienReleve = 0,
                    dernierReleve = 0,
                    nom = txtNomClient.Text,
                    prenom = txtPrenomClient.Text,
                    identifiant = gst.client.ToList().Last().identifiant + 1,
                    idcontroleur = (lstControleurs.SelectedItem as controleur).id
                };
                gst.client.Add(unClient);
                gst.SaveChanges();
                lstClients.ItemsSource = null;
                if (lstControleurs.SelectedItem != null)
                {
                    int test = (lstControleurs.SelectedItem as controleur).id; //ne marche pas sans cette étape pour une quelconque raison
                    var querry = from client in gst.client
                                 where client.idcontroleur == test
                                 select new clientCalc
                                 {
                                     id = client.identifiant,
                                     nom = client.nom,
                                     prenom = client.prenom,
                                     ancienReleve = client.ancienReleve,
                                     dernierReleve = client.dernierReleve,
                                     diffReleve = client.dernierReleve - client.ancienReleve
                                 };
                    lstClients.ItemsSource = querry.ToList();
                }
            }
        }


    }
}
