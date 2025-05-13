namespace APIDevSteam.Models
{
    public class Cartao
    {
        public Guid CartaoId { get; set; }
        public string UsuarioId { get; set; } // FK para o usuário dono do cartão
        public string NomeImpresso { get; set; }
        public string NumeroMascarado { get; set; } // Ex: **** **** **** 1234
        public string Bandeira { get; set; } // Ex: Visa, MasterCard
        public string UltimosDigitos { get; set; } // Ex: 1234
        public DateTime Validade { get; set; }
        public string TokenCartao { get; set; } // Tokenização para segurança (opcional)
        public bool Ativo { get; set; }

    }
}
