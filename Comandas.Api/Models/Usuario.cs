using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comandas.Api.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(150)]        
        public string Email { get; set; }
        [StringLength(16)]
        public string Senha { get; set; }
    }
}
