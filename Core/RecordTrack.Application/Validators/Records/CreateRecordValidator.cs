using FluentValidation;
using RecordTrack.Application.ViewModels.Records;

namespace RecordTrack.Application.Validators.Records
{
    public class CreateRecordValidator : AbstractValidator<VM_Create_Record>
    {
        public CreateRecordValidator()
        {
            RuleFor(record => record.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please enter a name")
                .MaximumLength(100)
                .MinimumLength(4)
                .WithMessage("Please enter a name with the range between 4 and 100");
                   
            RuleFor(record => record.Price)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a price value")
                .GreaterThan(0)
                .WithMessage("Please enter a value greater than 0");
            
            RuleFor(record => record.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a stock value")
                .Must(stock => stock >= 0)
                .WithMessage("Stock value must be greater than or eqaul to zero");
        }
    }
}
