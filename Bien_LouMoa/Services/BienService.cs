
using Bien_LouMoa.Models;
using Microsoft.EntityFrameworkCore;

namespace Bien_LouMoa.Services;

public class BienService : IService<Bien>
{
    private readonly ApplicationDbContext _context;

    public BienService(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET : récupérer tous les biens de l'utilisateur
    public async Task<List<Bien>> GetAllAsync(int idUtilisateur)
    {
        return await _context.Biens.Where(b => b.IdUtilisateur == idUtilisateur).ToListAsync();
    }

    // GET : récupérer un bien par Id pour un utilisateur spécifique
    public async Task<Bien?> GetByIdAsync(int id, int idUtilisateur)
    {
        return await _context.Biens.SingleOrDefaultAsync(b => b.IdBien == id && b.IdUtilisateur == idUtilisateur);
    }

    // POST : ajouter un nouveau bien pour un utilisateur spécifique
    public async Task<Bien> AddAsync(Bien bien, int idUtilisateur)
    {
        bien.IdUtilisateur = idUtilisateur;
        _context.Biens.Add(bien);
        await _context.SaveChangesAsync();
        return bien;
    }

    // PUT : mettre à jour un bien existant pour un utilisateur spécifique
    public async Task<Bien?> UpdateAsync(Bien bien, int idUtilisateur)
    {
        var existingBien = await GetByIdAsync(bien.IdBien, idUtilisateur);
        if (existingBien == null)
        {
            return null;
        }

        existingBien.Adresse = bien.Adresse;
        existingBien.Surface = bien.Surface;
        existingBien.NbChambres = bien.NbChambres;
        existingBien.NbLits = bien.NbLits;
        existingBien.NbSallesDeBain = bien.NbSallesDeBain;

        _context.Entry(existingBien).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return existingBien;
    }

    // DELETE : supprimer un bien par Id pour un utilisateur spécifique
    public async Task<Bien?> DeleteAsync(int id, int idUtilisateur)
    {
        var bien = await GetByIdAsync(id, idUtilisateur);
        if (bien == null)
        {
            return null;
        }

        _context.Biens.Remove(bien);
        await _context.SaveChangesAsync();
        return bien;
    }
}
