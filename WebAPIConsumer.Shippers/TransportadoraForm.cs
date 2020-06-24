using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebAPIConsumer.Shippers
{
    public partial class TransportadoraForm : Form
    {
        public TransportadoraForm()
        {
            InitializeComponent();
        }

        private int transportadoraId = 0;
        public void ExibirTransportadora(Transportadora t)
        {
            nomeTextBox.Text = t.Nome;
            telTextBox.Text = t.Telefone;
            transportadoraId = t.TranportadoraId;
        }

        public Transportadora ObterTransportadora()
        {
            var t = new Transportadora();
            t.TranportadoraId = transportadoraId;
            t.Nome = nomeTextBox.Text;
            t.Telefone = telTextBox.Text;
            return t;
        }
    }
}
