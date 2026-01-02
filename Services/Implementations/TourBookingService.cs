using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Models;
using VillaManagementWeb.Repositories.Interfaces;
using VillaManagementWeb.Services.Interfaces;

namespace VillaManagementWeb.Services.Implementations
{
    public class TourBookingService : ITourBookingService
    {
        private readonly ITourBookingRepository _tourBookingRepository;
        private readonly VillaDbContext _context;

        public TourBookingService(ITourBookingRepository tourBookingRepository, VillaDbContext context)
        {
            _tourBookingRepository = tourBookingRepository;
            _context = context;
        }

        public async Task<IEnumerable<TourBooking>> GetAllTourBookingsAsync()
        {
            return await _tourBookingRepository.GetAllAsync();
        }

        public async Task<TourBooking?> GetTourBookingByIdAsync(int id)
        {
            return await _tourBookingRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TourBooking>> GetTourBookingsWithToursAsync()
        {
            return await _tourBookingRepository.GetTourBookingsWithToursAsync();
        }

        public async Task<IEnumerable<TourBooking>> GetTourBookingsFilteredAsync(DateTime? tourDate, string? status)
        {
            var query = _context.TourBookings.Include(tb => tb.Tour).AsQueryable();

            if (tourDate.HasValue)
            {
                query = query.Where(tb => tb.TourDate.Date == tourDate.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(tb => tb.Status == status);
            }

            return await query.OrderByDescending(tb => tb.TourDate).ToListAsync();
        }

        public async Task<TourBooking> CreateTourBookingAsync(TourBooking tourBooking)
        {
            // Business rule: Validate customer name is not empty
            if (string.IsNullOrWhiteSpace(tourBooking.CustomerName))
            {
                throw new ArgumentException("Customer name cannot be empty.", nameof(tourBooking));
            }

            // Business rule: Validate contact info is not empty
            if (string.IsNullOrWhiteSpace(tourBooking.ContactInfo))
            {
                throw new ArgumentException("Contact info cannot be empty.", nameof(tourBooking));
            }

            // Business rule: Validate number of people
            if (tourBooking.NumberOfPeople <= 0)
            {
                throw new ArgumentException("Number of people must be greater than 0.", nameof(tourBooking));
            }

            // Business rule: Validate total price
            if (tourBooking.TotalPrice < 0)
            {
                throw new ArgumentException("Total price cannot be negative.", nameof(tourBooking));
            }

            // Business rule: Validate status is not empty
            if (string.IsNullOrWhiteSpace(tourBooking.Status))
            {
                throw new ArgumentException("Status cannot be empty.", nameof(tourBooking));
            }

            var createdTourBooking = await _tourBookingRepository.AddAsync(tourBooking);
            await _tourBookingRepository.SaveAsync();
            return createdTourBooking;
        }

        public async Task<TourBooking> UpdateTourBookingAsync(TourBooking tourBooking)
        {
            // Business rule: Validate customer name is not empty
            if (string.IsNullOrWhiteSpace(tourBooking.CustomerName))
            {
                throw new ArgumentException("Customer name cannot be empty.", nameof(tourBooking));
            }

            // Business rule: Validate contact info is not empty
            if (string.IsNullOrWhiteSpace(tourBooking.ContactInfo))
            {
                throw new ArgumentException("Contact info cannot be empty.", nameof(tourBooking));
            }

            // Business rule: Validate number of people
            if (tourBooking.NumberOfPeople <= 0)
            {
                throw new ArgumentException("Number of people must be greater than 0.", nameof(tourBooking));
            }

            // Business rule: Validate total price
            if (tourBooking.TotalPrice < 0)
            {
                throw new ArgumentException("Total price cannot be negative.", nameof(tourBooking));
            }

            // Business rule: Validate status is not empty
            if (string.IsNullOrWhiteSpace(tourBooking.Status))
            {
                throw new ArgumentException("Status cannot be empty.", nameof(tourBooking));
            }

            // Business rule: Validate tour booking exists
            var existingTourBooking = await _tourBookingRepository.GetByIdAsync(tourBooking.Id);
            if (existingTourBooking == null)
            {
                throw new ArgumentException($"Tour booking with ID {tourBooking.Id} not found.", nameof(tourBooking));
            }

            _tourBookingRepository.Update(tourBooking);
            await _tourBookingRepository.SaveAsync();
            return tourBooking;
        }

        public async Task<bool> DeleteTourBookingAsync(int id)
        {
            var tourBooking = await _tourBookingRepository.GetByIdAsync(id);
            if (tourBooking == null)
            {
                return false;
            }

            _tourBookingRepository.Delete(tourBooking);
            await _tourBookingRepository.SaveAsync();
            return true;
        }
    }
}

