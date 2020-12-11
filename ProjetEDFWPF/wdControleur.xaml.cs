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
    /// Logique d'interaction pour wdControleur.xaml
    /// </summary>
    public partial class wdControleur : Window
    {
        edfEntities gst;
        controleur login;
        public wdControleur(edfEntities unGst, controleur unControleur)
        {
            InitializeComponent();
            gst = unGst;
            login = unControleur;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var querry = from client in gst.client
                         where client.idcontroleur == login.id
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

        private void btnInserer_Click(object sender, RoutedEventArgs e)
        {
            if(lstClients.SelectedItem == null)
            {
                MessageBox.Show("Merci de choisir un client");
            }
            else if (txtNvReleve.Text == "")
            {
                MessageBox.Show("Merci d'entrer un releve");
            }
            else if(Convert.ToInt16(txtNvReleve.Text)<(lstClients.SelectedItem as clientCalc).dernierReleve)
            {
                MessageBox.Show("Le nouveau releve ne peut pas être inferieur au dernier releve");
            }
            else
            {
                client leClient = gst.client.ToList().Find(cl => cl.identifiant == (lstClients.SelectedItem as clientCalc).id);
                leClient.ancienReleve = leClient.dernierReleve;
                leClient.dernierReleve = Convert.ToInt16(txtNvReleve.Text);
                gst.SaveChanges();
                lstClients.ItemsSource = null;
                var querry = from client in gst.client
                             where client.idcontroleur == login.id
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
