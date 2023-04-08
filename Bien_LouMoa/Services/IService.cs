using BienLocatif_LouMoa.Models;

namespace BienLocatif_LouMoa.Services;

public interface IService<T>
{
    Task<List<T>> GetAllAsync(int idUtilisateur);

    Task<T?> GetByIdAsync(int id, int idUtilisateur);

    Task<T> AddAsync(T model, int idUtilisateur);

    Task<T?> UpdateAsync(T model, int idUtilisateur);
    
    Task<T?> DeleteAsync(int id, int idUtilisateur);
}
