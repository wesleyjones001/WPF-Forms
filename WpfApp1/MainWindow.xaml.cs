using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
        Session? ValidateLogin()
        {
            bool isValid = false;
            string username = usernameInput.Text;
            string passwordSHA256_hash = sha256_hash(username + "." + passwordInput.Password);

            // Validate Credentials with server SQL (TODO)
            if (!(username == "user" && passwordSHA256_hash == "b23ee88746a876c854a58ee11aa8ef35585e30561a2117d18236cb3edb419b8a"))
            {
                return null;
            }

            Session session = new Session(passwordSHA256_hash);
            isValid = session.IsValid();
            if (isValid)
            {
                return session;
            }
            else
            {
                return null;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Session? session = ValidateLogin();
            if (session != null)
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                this.Close();
            } else
            {
                MessageBox.Show("Username or password is incorrect", "Invalid");
            }
        }
    }
}
