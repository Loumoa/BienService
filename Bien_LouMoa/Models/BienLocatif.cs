using System.ComponentModel.DataAnnotations;

namespace BienLocatif_LouMoa.Models;

public class BienLocatif
{
    [Key]
    public int IdBien { get; set; }

    [Required]
    [StringLength(128)]
    public string Nom { get; set; }

    [Required]
    [StringLength(24)]
    public string Categorie { get; set; }

    [Required]
    [StringLength(128)]
    public string Adresse { get; set; }
    
    [Required]
    // numeric(15,1)
    [Range(0, 999999999999999.9)]
    public decimal Surface { get; set; }

    [Required]
    public int NbChambres { get; set; }

    [Required]
    public int NbLits { get; set; }

    [Required]
    public int NbSallesDeBain { get; set; }

    [Required]
    // numeric(15,2)
    [Range(0, 9999999999999999.99)]
    public decimal Prix { get; set; }

    [Required]
    public int IdProprietaire { get; set; }
}