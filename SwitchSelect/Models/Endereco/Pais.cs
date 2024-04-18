using System.ComponentModel.DataAnnotations;

namespace SwitchSelect.Models;

public class Pais
{
    public int Id { get; set; }

    [Display(Name = "Brasil")]
    public string Descricao { get; } = "Brasil";
    public List<Estado> Estados { get; set; }
}