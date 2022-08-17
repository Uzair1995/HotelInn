using HotelInn.Contracts.Booking;
using HotelInn.Domain.IRepositories;
using HotelInn.Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace HotelInn.Services.Services
{
    public class BookingService : IBookingService
    {
        private readonly Lazy<IBookingRepository> bookingRepository;
        private readonly Lazy<IHotelRepository> hotelRepository;
        private readonly Lazy<IUserRepository> userRepository;

        public BookingService(
            Lazy<IBookingRepository> bookingRepository,
            Lazy<IHotelRepository> hotelRepository,
            Lazy<IUserRepository> userRepository)
        {
            this.bookingRepository = bookingRepository;
            this.hotelRepository = hotelRepository;
            this.userRepository = userRepository;
        }

        public async Task<string> AddNewBookingAsync(NewBooking newBooking)
        {
            if (newBooking == null)
                return "Value cannot be null";

            if (newBooking.CheckinDateTime >= newBooking.CheckoutDateTime || newBooking.CheckinDateTime <= DateTime.Now)
                return "Invalid checkin checkout time provided!";

            Domain.Models.Hotel hotel = await hotelRepository.Value.FindHotelAsync(newBooking.HotelId);
            if (hotel == null)
                return "Hotel ID does not match any records!";
            if (!hotel.Availability)
                return "This hotel is all booked!";

            Domain.Models.User user = await userRepository.Value.FindUserAsync(newBooking.UserId);
            if (user == null)
                return "User ID does not match any records!";

            Domain.Models.Booking booking = new Domain.Models.Booking
            {
                HotelId = newBooking.HotelId,
                UserId = newBooking.UserId,
                CheckinDateTime = newBooking.CheckinDateTime,
                CheckoutDateTime = newBooking.CheckoutDateTime
            };

            await bookingRepository.Value.AddOrUpdateBookingAsync(booking);
            return "Saved successfully!";
        }
    }
}
