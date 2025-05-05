using Comandas.Shared.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Shared.Validators
{
    public class MesaCreateDTOValidator: AbstractValidator<MesaCreateDto>
    {
        public MesaCreateDTOValidator()
        {
            RuleFor(x => x.NumeroMesa)
                .NotEmpty()
                .WithMessage("Número da mesa não pode ser vazio");
            RuleFor(x => x.NumeroMesa)
                .GreaterThan(0)
                .WithMessage("Número mesa deve ser maior que zero");
            RuleFor(s => s.SituacaoMesa)
                .Must(s => s == 1 || s == 0)
                .WithMessage("Situação mesa deve ser 0 Disponível e 1 Ocupada");
        }
    }
}
