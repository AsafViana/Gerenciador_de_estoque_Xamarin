using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gerenciador_de_estoque.Model
{
    class Perfil
    {

        [JsonProperty("contagem")]
        public int Contagem { get; set; }
    }
}
