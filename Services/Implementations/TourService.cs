using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;
using VillaManagementWeb.Services.Interfaces;

namespace VillaManagementWeb.Services.Implementations
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _tourRepository;

        public TourService(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        public async Task<IEnumerable<Tour>> GetAllToursAsync()
        {
            return await _tourRepository.GetAllAsync();
        }

        public async Task<Tour?> GetTourByIdAsync(int id)
        {
            return await _tourRepository.GetByIdAsync(id);
        }

        public async Task<Tour> CreateTourAsync(Tour tour)
        {
            // Business rule: Validate tour name is not empty
            if (string.IsNullOrWhiteSpace(tour.TourName))
            {
                throw new ArgumentException("Tour name cannot be empty.", nameof(tour));
            }

            // Business rule: Validate pricing
            if (tour.PricePerPerson < 0)
            {
                throw new ArgumentException("Tour price per person cannot be negative.", nameof(tour));
            }

            // Business rule: Validate duration
            if (tour.DurationHours <= 0)
            {
                throw new ArgumentException("Tour duration must be greater than 0.", nameof(tour));
            }

            var createdTour = await _tourRepository.AddAsync(tour);
            await _tourRepository.SaveAsync();
            return createdTour;
        }

        public async Task<Tour> UpdateTourAsync(Tour tour)
        {
            // Business rule: Validate tour name is not empty
            if (string.IsNullOrWhiteSpace(tour.TourName))
            {
                throw new ArgumentException("Tour name cannot be empty.", nameof(tour));
            }

            // Business rule: Validate pricing
            if (tour.PricePerPerson < 0)
            {
                throw new ArgumentException("Tour price per person cannot be negative.", nameof(tour));
            }

            // Business rule: Validate duration
            if (tour.DurationHours <= 0)
            {
                throw new ArgumentException("Tour duration must be greater than 0.", nameof(tour));
            }

            // Business rule: Validate tour exists
            var existingTour = await _tourRepository.GetByIdAsync(tour.Id);
            if (existingTour == null)
            {
                throw new ArgumentException($"Tour with ID {tour.Id} not found.", nameof(tour));
            }

            _tourRepository.Update(tour);
            await _tourRepository.SaveAsync();
            return tour;
        }

        public async Task<bool> DeleteTourAsync(int id)
        {
            var tour = await _tourRepository.GetByIdAsync(id);
            if (tour == null)
            {
                return false;
            }

            _tourRepository.Delete(tour);
            await _tourRepository.SaveAsync();
            return true;
        }
    }
}

