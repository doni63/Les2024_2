using SwitchSelect.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace SwitchSelect.Models;

public class Cupom
{
    private readonly SwitchSelectContext _context;
    public int Id { get; set; }
    public string? CodigoCupom { get; set; }

    [Required(ErrorMessage ="Adiconar valor do cupom")]
    public decimal Valor { get; set; }
    public string? Status { get; set; }
    public int? ClienteId { get; set; }

    public  string GerarCodigoCupom()
    {
        const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var codigo = new StringBuilder();

        for (int i = 0; i < 12 ; i++)
        {
            if (i > 0 && i % 4 == 0)
            {
                codigo.Append('-'); 
            }

            int index = random.Next(caracteres.Length);
            codigo.Append(caracteres[index]);
        }

        return codigo.ToString();
    }
}
