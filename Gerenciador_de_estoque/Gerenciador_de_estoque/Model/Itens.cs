using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gerenciador_de_estoque.Model
{
    public class Itens
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }

        [JsonProperty("codigo")]
        public int Codigo { get; set; }

        [JsonProperty("preco")]
        public double Preco { get; set; }

        [JsonProperty("categoria")]
        public string Categoria { get; set; }
    }

    public class ContItem
    {
        [JsonProperty("cont")]
        public int Contagem { get; set; }
    }
}
