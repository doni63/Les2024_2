using System.ComponentModel.DataAnnotations;

namespace SwitchSelect.Models;

public class Pais
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public List<Estado> Estados { get; set; }
}