using System.ComponentModel.DataAnnotations;

namespace ExemploAPI.Models
{
    public class Cliente
    {
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "O Campo Nome é Obrigatório")]
        public string ClienteNome { get; set; }

        [Required(ErrorMessage = "O Campo Idade é Obrigatório")]
        public int Idade { get; set; }

        public DateTime? DataCadastro { get; set; } = DateTime.Now;
        public bool? CadastroAtivo { get; set; } = true;
    }
}
