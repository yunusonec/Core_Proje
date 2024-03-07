using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class SkillValidator : AbstractValidator<Skill>
    {
        public SkillValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Yetenek adı boş geçilemez");
            RuleFor(x => x.Value).NotEmpty().WithMessage("Oran boş geçilemez");
            RuleFor(x => x.Value).Must(x => int.TryParse(x, out _)).WithMessage("Oran sayısal bir değer olmalıdır");
            RuleFor(x => x.Title).MinimumLength(1).WithMessage("Yetenek adı en az 1 karakterden oluşmak zorundadır.");
            RuleFor(x => x.Title).MaximumLength(15).WithMessage("Yetenek adı en fazla 15 karakterden oluşmak zorundadır.");
        }
    }
}
