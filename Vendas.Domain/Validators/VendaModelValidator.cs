using FluentValidation;
using Vendas.Domain.Models;

namespace Vendas.Domain.Validators
{
    public class VendaModelValidator : AbstractValidator<VendaModel>
    {
        public VendaModelValidator()
        {
            RuleFor(v => v.NumeroVenda)
                .NotEmpty().WithMessage("O número da venda é obrigatório.")
                .MaximumLength(50).WithMessage("O número da venda não pode exceder 50 caracteres.");

            RuleFor(v => v.Cliente)
                .NotEmpty().WithMessage("O cliente é obrigatório.");

            RuleFor(v => v.Filial)
                .NotEmpty().WithMessage("A filial é obrigatória.");

            RuleFor(v => v.ValorTotal)
                .GreaterThan(0).WithMessage("O valor total da venda deve ser maior que zero.");

            RuleForEach(v => v.Itens).SetValidator(new ItemVendaModelValidator());
        }
    }

    public class ItemVendaModelValidator : AbstractValidator<ItemVendaModel>
    {
        public ItemVendaModelValidator()
        {
            RuleFor(i => i.Produto)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.");

            RuleFor(i => i.Quantidade)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

            RuleFor(i => i.PrecoUnitario)
                .GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");
        }
    }
}
