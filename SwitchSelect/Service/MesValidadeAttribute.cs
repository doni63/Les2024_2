using System.ComponentModel.DataAnnotations;

public class MesValidadeAttribute : ValidationAttribute
{
    public MesValidadeAttribute()
    {
        ErrorMessage = "O mês de validade deve ser maior que o mês atual.";
    }

    public override bool IsValid(object value)
    {
        if (value is null)
        {
            return true; // Se o valor for nulo, considera válido (outros validadores podem verificar isso)
        }

        if (value is int mes)
        {
            // Verifica se o mês é válido (entre 1 e 12) e se é maior que o mês atual
            return mes >= 1 && mes <= 12 && DateTime.Now.Month <= mes;
        }

        return false; // Se o valor não for um inteiro, considera inválido
    }
}
