﻿using Comandas.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Comandas.Api.Dtos
{
    public class CardapioCreateDto
    {
        [StringLength(150)]
        public string Titulo { get; set; }
        [StringLength(300)]
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public bool PossuiPreparo { get; set; }
    }
}
