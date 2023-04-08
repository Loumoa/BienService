
using Bien_LouMoa.Models;
using BienLocatif_LouMoa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BienLocatif_LouMoa.Services;

public class BienLocatifService : IService<BienLocatif>
{
    private readonly ApplicationDbContext _context;

    public BienLocatifService(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET : récupérer tous les BienLocatifs de l'utilisateur
    public async Task<List<BienLocatif>> GetAllAsync(int idUtilisateur)
    {
        return await _context.BienLocatifs.Where(b => b.IdProprietaire == idUtilisateur).ToListAsync();
    }

    // GET : récupérer un BienLocatif par Id pour un utilisateur spécifique
    public async Task<BienLocatif?> GetByIdAsync(int id, int idUtilisateur)
    {
        return await _context.BienLocatifs.SingleOrDefaultAsync(b => b.IdBien == id && b.IdProprietaire == idUtilisateur);
    }

    // POST : ajouter un nouveau BienLocatif pour un utilisateur spécifique
    public async Task<BienLocatif> AddAsync(BienLocatif BienLocatif, int idUtilisateur)
    {
        BienLocatif.IdProprietaire = idUtilisateur;
        _context.BienLocatifs.Add(BienLocatif);
        await _context.SaveChangesAsync();
        return BienLocatif;
    }

    // PUT : mettre à jour un BienLocatif existant pour un utilisateur spécifique
    public async Task<BienLocatif?> UpdateAsync(BienLocatif BienLocatif, int idUtilisateur)
    {
        var existingBienLocatif = await GetByIdAsync(BienLocatif.IdBien, idUtilisateur);
        if (existingBienLocatif == null)
        {
            return null;
        }

        existingBienLocatif.Adresse = BienLocatif.Adresse;
        existingBienLocatif.Surface = BienLocatif.Surface;
        existingBienLocatif.NbChambres = BienLocatif.NbChambres;
        existingBienLocatif.NbLits = BienLocatif.NbLits;
        existingBienLocatif.NbSallesDeBain = BienLocatif.NbSallesDeBain;

        _context.Entry(existingBienLocatif).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return existingBienLocatif;
    }

    // DELETE : supprimer un BienLocatif par Id pour un utilisateur spécifique
    public async Task<BienLocatif?> DeleteAsync(int id, int idUtilisateur)
    {
        var BienLocatif = await GetByIdAsync(id, idUtilisateur);
        if (BienLocatif == null)
        {
            return null;
        }

        _context.BienLocatifs.Remove(BienLocatif);
        await _context.SaveChangesAsync();
        return BienLocatif;
    }

    // DELETE ALL : supprimer tous les BienLocatifs d'un utilisateur
    public async Task DeleteAllAsync(int idUtilisateur)
    {
        var BienLocatifs = await GetAllAsync(idUtilisateur);
        _context.BienLocatifs.RemoveRange(BienLocatifs);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> BelongsToAsync(int id, int idUser)
    {
        var BienLocatif = await GetByIdAsync(id, idUser);
        return BienLocatif != null;
    }

    public async Task<ActionResult<List<BienLocatif>>> GetMatchingLogementAsync(RequestMatchingLogement request)
    {
        var logements = await _context.BienLocatifs
            .Where(b => b.Adresse == request.Adresse 
            || b.Categorie == request.Categorie 
            || b.NbChambres == request.NbChambres 
            || b.NbLits == request.NbLits 
            || b.NbSallesDeBain == request.NbSallesDeBain 
            || b.Prix == request.Prix 
            || b.Surface == request.Surface)
            .ToListAsync();
        return logements;
    }
}
