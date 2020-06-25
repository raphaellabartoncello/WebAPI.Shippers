using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace WebAPIConsumer.Shippers
{
    public partial class frmConsumerWebAPI : Form
    {

        string nomeArquivo = string.Empty;

        public frmConsumerWebAPI()
        {
            InitializeComponent();
        }

        //
        // Obtém uma instância de HttpCliente
        // já configurado com o formato e a url do servidor
        //
        private HttpClient ObterHttClient()
        {
            var formato = new MediaTypeWithQualityHeaderValue("application/json");
            var client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:44394/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(formato);

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
            using (var client = ObterHttClient())
            {
                var resposta = await client.GetAsync("http://localhost:44394/api/transportadoras");
                var conteudo = await resposta.Content.ReadAsAsync<Transportadora[]>();
                dataGridView1.DataSource = conteudo;
                dataGridView1.ReadOnly = true;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregarGrid();

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair?", "Transportadora NorthWind - AVISO",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                e.Cancel = true;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Obter o nome e caminho (path) do arquivo
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nomeArquivo = openFileDialog1.FileName;
            }
        }

        //private void Upload(string fileName, string server)
        //{
        //    using (var fileStream = File.Open(fileName, FileMode.Open))
        //    {
        //        var formato = new MediaTypeWithQualityHeaderValue("multipart/form-data");
        //        var client = new HttpClient();

        //        client.BaseAddress = new Uri("http://localhost:4/");
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(formato);

        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            await fileStream.CopyToAsync(memoryStream);

        //            return response;
        //        }
        //    }
        //}
    }
}
