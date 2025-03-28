namespace Comandas.Shared.Dtos
{
    public class ComandaDto
    {
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; }
        public int[] CardapioItems { get; set; } = [];
    }

    public class ComandaCreateDto
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; }
        public int[] CardapioItems { get; set; } = [];
    }

}
