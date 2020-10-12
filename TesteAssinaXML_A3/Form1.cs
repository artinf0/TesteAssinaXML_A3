using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteAssinaXML_A3 {
    public partial class Form1 : Form {
        string type = "1";
        string provider = "SafeSign Standard Cryptographic Service Provider";

        public Form1() {
            InitializeComponent();
            txbProvider.Text = provider;
            txbType.Text = type;
        }

        private void btnTestar_Click(object sender, EventArgs e) {
            provider = txbProvider.Text;
            type = txbType.Text;
            if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(provider)) {

                try {
                    CspParameters csp = new CspParameters(Convert.ToInt32(type), provider);
                    csp.Flags = CspProviderFlags.UseDefaultKeyContainer;

                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);

                    KeyInfo teste = new KeyInfo();
                    teste.AddClause(new RSAKeyValue((RSA)rsa));

                    // Create some data to sign. 
                    byte[] data = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                    txbResultado.Text = "Data			: " + BitConverter.ToString(data);
                    Console.WriteLine("Data			: " + BitConverter.ToString(data));

                    byte[] sig = rsa.SignData(data, "SHA1");

                    txbResultado.Text = "Signature	: " + BitConverter.ToString(sig);
                    Console.WriteLine("Signature	: " + BitConverter.ToString(sig));
                } catch (Exception Ex) {

                    MessageBox.Show("Erro: " + Ex.Message);
                }

            } else {
                MessageBox.Show("O provider e o type não podem ser nulos!");
            }
        }
    }
}
