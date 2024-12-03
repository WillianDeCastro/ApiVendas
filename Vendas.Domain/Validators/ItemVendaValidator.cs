using FluentValidation;
using Vendas.Domain.Entities;

namespace Vendas.Domain.Validators
{
    public class ItemVendaValidator : AbstractValidator<ItemVenda>
    {
        public ItemVendaValidator()
        {
            RuleFor(iv => iv.Quantidade)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.")
                .LessThanOrEqualTo(20).WithMessage("Não é permitido vender mais de 20 itens iguais.");

            RuleFor(iv => iv.Desconto)
                .Cascade(CascadeMode.Stop)
                .Must((item, desconto) => ValidarDisconto(item)).WithMessage("O desconto aplicado é inválido.");

            RuleFor(iv => iv.Quantidade)
                .LessThan(4)
                .When(iv => iv.Desconto > 0)
                .WithMessage("Compras abaixo de 4 itens não podem ter desconto.");
        }

        private bool ValidarDisconto(ItemVenda item)
        {
            if (item.Quantidade < 4 && item.Desconto > 0)
                return false; 

            if (item.Quantidade >= 4 && item.Quantidade < 10)
                return item.Desconto <= item.PrecoUnitario * 0.10M;

            if (item.Quantidade >= 10 && item.Quantidade <= 20)
                return item.Desconto <= item.PrecoUnitario * 0.20M; 

            return true;
        }
    }
}
