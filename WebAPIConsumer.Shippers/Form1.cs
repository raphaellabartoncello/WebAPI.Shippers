using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebAPIConsumer.Shippers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //
        // Obtém uma instância de HttpCliente
        // já configurado com o formato e a url do servidor
        //
        private HttpClient ObterHttpClient()
        {
            var formato =
            new MediaTypeWithQualityHeaderValue(
            "application/json");
            var client = new HttpClient();
            client.BaseAddress =
            new Uri("https://localhost:44394/");
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders
            .Accept
            .Add(formato);
            return client;
        }

        //
        // Analisa se o comando foi bem sucedido
        //
        private void verificarResposta(HttpResponseMessage resposta)
        {
            if (!resposta.IsSuccessStatusCode)
            {
                MessageBox.Show("Erro no servidor:" + resposta.StatusCode);
            }
        }

        //
        // GET: Obtém todas as transportadoras
        //
        private async void CarregarGrid()
        {
            using (var client = ObterHttpClient())
            {
                var resposta = await client
                .GetAsync("api/Transportadoras");
                var conteudo = await resposta
                .Content
               .ReadAsAsync<Transportadora[]>();
                dataGridView1.DataSource = conteudo;
                dataGridView1.ReadOnly = true;
                dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
    }
}
