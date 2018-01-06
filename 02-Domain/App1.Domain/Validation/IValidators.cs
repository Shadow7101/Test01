namespace App1.Domain.Validation
{
    public interface IValidators
    {
        bool IsValid { get; }

        string ValidationError { get; }
    }
}
