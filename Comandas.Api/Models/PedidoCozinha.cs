using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comandas.Api.Models
{
    public class PedidoCozinha
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public virtual Comanda Comanda { get; set; }
        public int SituacaoId{ get; set; } = 1;
        public virtual ICollection<PedidoCozinhaItem> PedidoCozinhaItem { get; set; }
    }

    public class PedidoCozinhaItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PedidoCozinhaId { get; set; }
        public virtual PedidoCozinha PedidoCozinha { get; set; }
        public int ComandaItemId  { get; set; }
        public virtual ComandaItem ComandaItem { get; set; }
    }

    public class Mesa
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public int SituacaoMesa { get; set; }       
    }
}
