using projeto_final_bloco_02.Model;
using FluentValidation;
namespace projeto_final_bloco_02.Validator
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(t => t.Descricao)
                .NotEmpty();
        }
    }
}
