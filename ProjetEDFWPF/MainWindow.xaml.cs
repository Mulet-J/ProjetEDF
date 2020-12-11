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

namespace ProjetEDFWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        edfEntities gst;
        public MainWindow()
        {
            InitializeComponent();
            gst = new edfEntities();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            txtError.Text = "";
            if (txtLogin.Text == "")
            {
                txtError.Text = "Merci d'entrer un login";
            }
            else if(txtMdp.Password == "")
            {
                txtError.Text = "Merci d'entrer un mot de passe";
            }
            else
            {
                controleur login = gst.controleur.ToList().Find(log => log.login == txtLogin.Text);
                if (login != null)
                {
                    if (login.mdp == txtMdp.Password)
                    {
                        if(login.statut == "admin")
                        {
                            wdAdmin wdAdmin = new wdAdmin(gst);
                            wdAdmin.Show();
                        }
                        else
                        {
                            wdControleur wdControleur = new wdControleur(gst,login);
                            wdControleur.Show();
                        }
                    }
                    else
                    {
                        txtError.Text = "Login érroné";
                    }
                }
                else
                {
                    txtError.Text = "Login érroné";
                }
            }
        }
    }
}
