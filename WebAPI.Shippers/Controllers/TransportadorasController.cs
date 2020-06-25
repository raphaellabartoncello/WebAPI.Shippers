using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Shippers.Models;

namespace WebAPI.Shippers.Controllers
{
    public class TransportadorasController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Transportadora> Get()
        {
            var db = new NorthwindEntities();
            return db.Transportadoras.ToList();
        }

        // GET api/<controller>/5
        public Transportadora Get(int id)
        {
            var db = new NorthwindEntities();
            return db.Transportadoras.Find(id);
        }

        // POST api/<controller>
        public void Post([FromBody] Transportadora t)
        {
            var db = new NorthwindEntities();
            db.Transportadoras.Add(t);
            db.SaveChanges();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] Transportadora t)
        {
            var db = new NorthwindEntities();
            var original = db.Transportadoras.Find(id);
            original.Nome = t.Nome;
            original.Telefone = t.Telefone;
            db.SaveChanges();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            var db = new NorthwindEntities();
            var original = db.Transportadoras.Find(id);
            db.Transportadoras.Remove(original);
            db.SaveChanges();

        }

        //Criar uma API para receber arquivos
        public HttpResponseMessage PostFile()
        {
            HttpResponseMessage resultadoMensagem = null;

            //Armazena nessa variável a chamada feita - chamada atual (posso ter várias chamadas ao mesmo tempo)
            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {
                //Lista utiliada para devolver ao CLIENT a lista do caminho em que o arquivo foi salvo
                var listDocFiles = new List<string>();

                //For para varrer todos os arquivos recebidos
                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    //Obtenção do arquivo contido na collection, obtemos pelo índice
                    var arquivoEnviado = httpRequest.Files[i];
                    var caminhoDoArquivo = HttpContext.Current.Server.MapPath("~/" + arquivoEnviado);

                    //Salvar o arquivo
                    httpRequest.Files[i].SaveAs(caminhoDoArquivo);

                    //Adicionar na lista de arquivos salvos
                    listDocFiles.Add(caminhoDoArquivo);
                }

                //Obter o resultado da execução da API de arquivos
                resultadoMensagem = Request.CreateResponse(HttpStatusCode.Created, listDocFiles);
            }

            else
            {
                resultadoMensagem = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return resultadoMensagem;
        }
    }
}