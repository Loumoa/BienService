namespace Bien_LouMoa.Models;

public record RequestMatchingLogement(
    string? Nom,
    string? Categorie,
    string? Adresse,
    decimal? Surface,
    int? NbChambres,
    int? NbLits,
    int? NbSallesDeBain,
    decimal? Prix);
