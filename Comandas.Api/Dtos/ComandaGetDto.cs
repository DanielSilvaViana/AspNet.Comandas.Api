using Comandas.Api.Models;

namespace Comandas.Api.Dtos
{
    public class ComandaGetDto
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; }
        public int SituacaoComanda { get; set; } = 1;
        public List<ComandaItemsGetDto> ComandaItems { get; set; } = new List<ComandaItemsGetDto>();
    }

    public class ComandaItemsGetDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
    }
}
