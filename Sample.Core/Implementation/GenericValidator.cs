namespace Sample.Core.Implementation
{
    using FluentValidation;

    public class GenericValidator<T>
        : AbstractValidator<T>
    {
    }
}