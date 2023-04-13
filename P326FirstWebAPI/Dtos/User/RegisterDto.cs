using FluentValidation;

namespace P326FirstWebAPI.Dtos.User
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
    public class RegisterValidator : AbstractValidator<RegisterDto> 
    {
        public RegisterValidator()
        {
            RuleFor(u => u.FullName).NotEmpty().MinimumLength(5).MaximumLength(20);
            RuleFor(u => u.UserName).NotEmpty().MinimumLength(5).MaximumLength(20);
            RuleFor(u => u.Password).NotEmpty().MinimumLength(6).MaximumLength(15);
            RuleFor(u => u.RePassword).NotEmpty().MinimumLength(6).MaximumLength(15);
            RuleFor(u => u).Custom((u, context) =>
            {
                if (u.Password != u.RePassword)
                {
                    context.AddFailure("Password", "Password does not match ..");
                }
            });





        }
    }

}
