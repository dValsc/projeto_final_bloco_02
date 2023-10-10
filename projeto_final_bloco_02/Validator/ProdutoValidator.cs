using FluentValidation;
using projeto_final_bloco_02.Model;

namespace projeto_final_bloco_02.Validator
{
    public class ProdutoValidator : AbstractValidator <Produto>
    {
        public ProdutoValidator() 
        {
            RuleFor(p => p.Titulo)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(250);

            RuleFor(p => p.Descricao)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(500);

            RuleFor(p => p.Preco)
            .NotEmpty();

        }
    }
}
