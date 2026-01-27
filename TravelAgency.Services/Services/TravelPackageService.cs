using TravelAgency.Core.Interfaces;
using TravelAgency.Core.Models;

namespace TravelAgency.Services.Services
{
    public class TravelPackageService : ITravelPackageService
    {
        private readonly ITravelPackageRepository _repo;
        public TravelPackageService(ITravelPackageRepository repo) => _repo = repo;

        public async Task<IEnumerable<TravelPackage>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<TravelPackage?> GetByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(TravelPackage package)
        {
            if (package.Price < 0) throw new ArgumentException("Price cannot be negative");

            await _repo.AddAsync(package);
            await _repo.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _repo.SaveChangesAsync();
        }
    }
}