using FluentValidation.Results;

namespace VF.Domain.Exceptions
{
    public class VehicleValidationException : Exception
    {
        private readonly List<ValidationFailure> _failures;

        public VehicleValidationException(string message, List<ValidationFailure> failures): base(message)
        {
            _failures = failures;
            int item = 1;
            foreach (var failure in failures)
            {
                Data.Add($"{item}_{failure.PropertyName}", failure.ErrorMessage);
                item++;
            }
        }


        public override string ToString()
        {
            return string.Join("; ", _failures);
        }
    }
}
