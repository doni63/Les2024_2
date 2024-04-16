using System.ComponentModel.DataAnnotations;

namespace SwitchSelect.Service
{
    public class AnoValidadeAttribute: ValidationAttribute
    {

        public AnoValidadeAttribute()
        {
            ErrorMessage = "O ano de validade deve ser igual ou maior que o ano atual.";
        }

        public override bool IsValid(object value)
        {
            if(value is null)
            {
                return true;
            }

            if (value is int ano)
            {
                return ano >= DateTime.Now.Year;
            }
            return false;
        }
    }
}
