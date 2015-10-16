namespace Sample.Core.Implementation
{
    using FluentValidation;
    using Sample.Core.Commands;

    public class CreateProductCommandValidator 
        : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty()
                .WithMessage("Product name required.");
            
            RuleFor(x => x.ListPrice)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5)
                .WithMessage("List price must be between 1 and 5.");

            RuleFor(x => x.ProductName)
                .NotEmpty()
                .Length(1, 20)
                .WithMessage("Product name must be no more than 20 characters long.");
        }
    }
}
