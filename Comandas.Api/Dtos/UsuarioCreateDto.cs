using System.ComponentModel.DataAnnotations;

namespace Comandas.Api.Dtos
{
    public class UsuarioCreateDto
    {
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(16)]
        public string Senha { get; set; }
    }
}
