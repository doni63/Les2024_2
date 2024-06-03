using SwitchSelect.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

public class MesValidadeAttribute : ValidationAttribute
{
    public MesValidadeAttribute()
    {
        ErrorMessage = "O mês de validade deve ser maior que o mês atual.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var model = (CartaoViewModel)validationContext.ObjectInstance;

        if (model.MesValidade < 1 || model.MesValidade > 12)
        {
            return new ValidationResult("O mês de validade deve estar entre 1 e 12.");
        }

        if (model.AnoValidade == DateTime.Now.Year)
        {
            if (model.MesValidade >= DateTime.Now.Month)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        else if (model.AnoValidade > DateTime.Now.Year)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("O ano de validade deve ser maior ou igual ao ano atual.");
        }
    }
}
