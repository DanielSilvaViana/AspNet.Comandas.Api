using Comandas.Shared.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Shared.Validators
{
    public class CardapioItemCreateDTOValidator: AbstractValidator<CardapioCreateDto>
    {
        public CardapioItemCreateDTOValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("O Titulo não pode ser vazio")
                .MaximumLength(150)
                .WithMessage("O Tamanho do Titulo deve ser no max. de 150");
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("A descrição não pode ser vazia")
                .MaximumLength(200)
                .WithMessage("A descrição não pode ter um valor maior de 200");
            RuleFor(x => x.Preco)
                .GreaterThan(0)
                .WithMessage("O Preço dever ser maior que 0");
            RuleFor(x => x.PossuiPreparo)
                .NotEmpty()
                .WithMessage("O Preparo não pode ser vazio");

        }
    }
}
