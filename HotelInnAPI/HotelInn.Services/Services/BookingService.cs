using HotelInn.Contracts.Booking;
using HotelInn.Domain.IRepositories;
using HotelInn.Services.Abstractions;
using System;
using System.Collections.Generic;
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

        public async Task<string> DeleteBookingAsync(string bookingId)
        {
            if (string.IsNullOrWhiteSpace(bookingId))
                return "Booking ID is not provided!";

            Domain.Models.Booking booking = await bookingRepository.Value.FindBookingAsync(bookingId);
            if (booking == null)
                return "Booking ID does not match any records!";

            await bookingRepository.Value.DeleteBookingAsync(bookingId);
            return "Delete successfully!";
        }

        public Task<Booking> FindBookingAsync(string bookingId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Booking>> FindHotelBookingsAsync(string hotelId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Booking>> FindUserBookingsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateBookingAsync(Booking booking)
        {
            if (booking == null || booking.BookingId == null)
                return "Value cannot be null";

            Domain.Models.Booking alreadySavedData = await bookingRepository.Value.FindBookingAsync(booking.BookingId);
            if (alreadySavedData == null)
                return "Booking ID does not match any records!";

            if (booking.CheckinDateTime >= booking.CheckoutDateTime || booking.CheckinDateTime <= DateTime.Now)
                return "Invalid checkin checkout time provided!";

            Domain.Models.Hotel hotel = await hotelRepository.Value.FindHotelAsync(booking.HotelId);
            if (hotel == null)
                return "Hotel ID does not match any records!";
            if (!hotel.Availability)
                return "This hotel is all booked!";

            Domain.Models.User user = await userRepository.Value.FindUserAsync(booking.UserId);
            if (user == null)
                return "User ID does not match any records!";

            Domain.Models.Booking updateBooking = new Domain.Models.Booking
            {
                BookingId = booking.BookingId,
                UserId = booking.UserId,
                HotelId = booking.HotelId,
                CheckinDateTime = booking.CheckinDateTime,
                CheckoutDateTime = booking.CheckoutDateTime
            };

            await bookingRepository.Value.AddOrUpdateBookingAsync(updateBooking);
            return "Update successfully!";
        }
    }
}
