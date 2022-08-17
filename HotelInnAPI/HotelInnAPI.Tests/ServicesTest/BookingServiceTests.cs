using FluentAssertions;
using HotelInn.Domain.IRepositories;
using HotelInn.Services.Abstractions;
using HotelInn.Services.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HotelInnAPI.Tests.ServicesTest
{
    public class BookingServiceTests
    {
        private readonly Mock<IBookingRepository> bookingRepoStub = new Mock<IBookingRepository>();
        private readonly Mock<IHotelRepository> hotelRepoStub = new Mock<IHotelRepository>();
        private readonly Mock<IUserRepository> userRepoStub = new Mock<IUserRepository>();


        [Fact]
        public async Task AddNewBookingAsync_NoDataPassed_ReturnsValueCannotBeNullAsync()
        {
            //Arrange
            IBookingService bookingService = new BookingService(new Lazy<IBookingRepository>(bookingRepoStub.Object), new Lazy<IHotelRepository>(hotelRepoStub.Object), new Lazy<IUserRepository>(userRepoStub.Object));

            //Act
            string result = await bookingService.AddNewBookingAsync(null);

            //Assert
            result.Should().BeEquivalentTo("Value cannot be null!");
        }
        [Fact]
        public async Task AddNewBookingAsync_InvalidCheckinCheckout_ReturnsInvalidCheckInCheckOutTimeAsync()
        {
            //Arrange
            IBookingService bookingService = new BookingService(new Lazy<IBookingRepository>(bookingRepoStub.Object), new Lazy<IHotelRepository>(hotelRepoStub.Object), new Lazy<IUserRepository>(userRepoStub.Object));
            HotelInn.Contracts.Booking.NewBooking newBooking = new HotelInn.Contracts.Booking.NewBooking
            {
                UserId = "SampleUserId",
                HotelId = "SampleHotelId",
                CheckinDateTime = DateTime.Now,
                CheckoutDateTime = DateTime.Now.AddDays(-1)
            };

            //Act
            string result = await bookingService.AddNewBookingAsync(newBooking);

            //Assert
            result.Should().BeEquivalentTo("Invalid checkin checkout time provided!");
        }
        [Fact]
        public async Task AddNewBookingAsync_HotelIdIsInvalid_ReturnsInvalidHotelAsync()
        {
            //Arrange
            hotelRepoStub.Setup(repo => repo.FindHotelAsync(It.IsAny<string>())).ReturnsAsync((HotelInn.Domain.Models.Hotel)null);
            IBookingService bookingService = new BookingService(new Lazy<IBookingRepository>(bookingRepoStub.Object), new Lazy<IHotelRepository>(hotelRepoStub.Object), new Lazy<IUserRepository>(userRepoStub.Object));
            HotelInn.Contracts.Booking.NewBooking newBooking = new HotelInn.Contracts.Booking.NewBooking
            {
                UserId = "SampleUserId",
                HotelId = "SampleHotelId",
                CheckinDateTime = DateTime.Now.AddDays(1),
                CheckoutDateTime = DateTime.Now.AddDays(2)
            };

            //Act
            string result = await bookingService.AddNewBookingAsync(newBooking);

            //Assert
            result.Should().BeEquivalentTo("Hotel ID does not match any records!");
        }
        [Fact]
        public async Task AddNewBookingAsync_HotelIsUnavailable_ReturnsHotelIsBookedAsync()
        {
            //Arrange
            HotelInn.Domain.Models.Hotel sampleHotel = new HotelInn.Domain.Models.Hotel
            {
                HotelId = "SampleHotelId",
                Availability = false
            };
            hotelRepoStub.Setup(repo => repo.FindHotelAsync(It.IsAny<string>())).ReturnsAsync(sampleHotel);
            IBookingService bookingService = new BookingService(new Lazy<IBookingRepository>(bookingRepoStub.Object), new Lazy<IHotelRepository>(hotelRepoStub.Object), new Lazy<IUserRepository>(userRepoStub.Object));
            HotelInn.Contracts.Booking.NewBooking newBooking = new HotelInn.Contracts.Booking.NewBooking
            {
                UserId = "SampleUserId",
                HotelId = "SampleHotelId",
                CheckinDateTime = DateTime.Now.AddDays(1),
                CheckoutDateTime = DateTime.Now.AddDays(2)
            };

            //Act
            string result = await bookingService.AddNewBookingAsync(newBooking);

            //Assert
            result.Should().BeEquivalentTo("This hotel is all booked!");
        }
        //[Fact]
        //public async Task AddNewBookingAsync_NoDataPassed_ReturnsValueCannotBeNullAsync()
        //{
        //    //Arrange
        //    IBookingService bookingService = new BookingService(bookingRepoStub.Object, hotelRepoStub.Object, userRepoStub.Object);

        //    //Act
        //    string result = await bookingService.AddNewBookingAsync(null);

        //    //Assert
        //    result.Should().BeEquivalentTo("User ID does not match any records!");
        //}
        //[Fact]
        //public async Task AddNewBookingAsync_NoDataPassed_ReturnsValueCannotBeNullAsync()
        //{
        //    //Arrange
        //    IBookingService bookingService = new BookingService(bookingRepoStub.Object, hotelRepoStub.Object, userRepoStub.Object);

        //    //Act
        //    string result = await bookingService.AddNewBookingAsync(null);

        //    //Assert
        //    result.Should().BeEquivalentTo("Saved successfully!");
        //}
    }
}
