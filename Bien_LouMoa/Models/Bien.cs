using System.ComponentModel.DataAnnotations;

namespace Bien_LouMoa.Models;

public class Bien
{
    [Key]
    public int IdBien { get; set; }

    [Required]
    [StringLength(50)]
    public string Adresse { get; set; }

    [Required]
    public decimal Surface { get; set; }

    [Required]
    public int NbChambres { get; set; }

    [Required]
    public int NbLits { get; set; }

    [Required]
    public int NbSallesDeBain { get; set; }

    [Required]
    public int IdUtilisateur { get; set; }
}