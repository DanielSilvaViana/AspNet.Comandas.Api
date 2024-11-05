using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Comandas.Api.Models
{
    public class Comanda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; }
        public int SituacaoComanda { get; set; } = 1;
        public virtual ICollection<ComandaItem> ComandaItems { get; set; }
    }

    public class ComandaItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CardapioItemId { get; set; }
        public virtual CardapioItem CardapioItem { get; set; }
        public int ComandaId { get; set; }
        public virtual Comanda Comanda { get; set; }
    }

    public class CardapioItem
    {
        // Key = significa chave primaria
        // DatabaseGenerated = Valor Gerado da coluna será realizado pelo BD
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public bool PossuiPreparo { get; set; }
    }
}

