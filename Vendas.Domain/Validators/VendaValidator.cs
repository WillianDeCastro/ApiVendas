using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Domain.Entities;

namespace Vendas.Domain.Validators
{
    public class VendaValidator : AbstractValidator<Venda>
    {
        public VendaValidator()
        {
            RuleForEach(v => v.Itens).SetValidator(new ItemVendaValidator());

            RuleFor(v => v.ValorTotal)
                .GreaterThan(0).WithMessage("O valor total da venda deve ser maior que zero.");
        }
    }
}
