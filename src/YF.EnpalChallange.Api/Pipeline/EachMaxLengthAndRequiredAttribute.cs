using System.ComponentModel.DataAnnotations;

namespace YF.EnpalChallange.Api.Pipeline;

public class EachMaxLengthAndRequiredAttribute(int maxArrayLength, int maxStringLength) : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var array = value as string?[];
        if (array == null)
        {
            return ValidationResult.Success;
        }

        if (array.Length > maxArrayLength)
        {
            return new ValidationResult($"Array length exceeds {maxArrayLength}.");
        }

        foreach (var str in array)
        {
            if (str == null)
            {
                return new ValidationResult("One or more strings are null.");
            }

            if (str.Length > maxStringLength)
            {
                return new ValidationResult($"One or more strings exceed {maxStringLength} characters.");
            }
        }

        return ValidationResult.Success;
    }
}