using System.ComponentModel.DataAnnotations;

namespace Comandas.Api.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(16)]
        public string Senha { get; set; }
    }
}
