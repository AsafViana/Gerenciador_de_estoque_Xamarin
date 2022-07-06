using Gerenciador_de_estoque.Model;
using Gerenciador_de_estoque.Regras;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gerenciador_de_estoque.ViewModel
{
    public class NovoUsuarioController
    {
        string ReferenciaUsuarioCripto = Criptografia.Criptografando("usuario");

        
        private string Empresa, Email, Senha;

        Dictionary<string, string> Dados = new Dictionary<string, string>();
        FirebaseModel Banco = new FirebaseModel();

        public void Cadastrar(string empresa, string email, string senha)
        {
            Empresa = empresa;
            Email = email;
            Senha = senha;

            Dados.Add("Senha", Senha);

            Dados.Add("Email", Email);
        }

    }
}
