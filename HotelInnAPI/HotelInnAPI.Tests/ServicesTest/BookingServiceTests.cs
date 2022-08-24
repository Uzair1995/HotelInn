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

        /// <summary>
        /// Tests for adding a new boking.
        /// </summary>
        /// <returns></returns>
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
                Username = "SampleUserName",
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
                Username = "SampleUserName",
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
                Username = "SampleUserName",
                HotelId = "SampleHotelId",
                CheckinDateTime = DateTime.Now.AddDays(1),
                CheckoutDateTime = DateTime.Now.AddDays(2)
            };

            //Act
            string result = await bookingService.AddNewBookingAsync(newBooking);

            //Assert
            result.Should().BeEquivalentTo("This hotel is all booked!");
        }
        [Fact]
        public async Task AddNewBookingAsync_InvalidUserIdProvided_ReturnsUserNotInRecordsAsync()
        {
            //Arrange
            HotelInn.Domain.Models.Hotel sampleHotel = new HotelInn.Domain.Models.Hotel
            {
                HotelId = "SampleHotelId",
                Availability = true
            };
            hotelRepoStub.Setup(repo => repo.FindHotelAsync(It.IsAny<string>())).ReturnsAsync(sampleHotel);
            userRepoStub.Setup(repo => repo.FindUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((HotelInn.Domain.Models.User)null);
            IBookingService bookingService = new BookingService(new Lazy<IBookingRepository>(bookingRepoStub.Object), new Lazy<IHotelRepository>(hotelRepoStub.Object), new Lazy<IUserRepository>(userRepoStub.Object));
            HotelInn.Contracts.Booking.NewBooking newBooking = new HotelInn.Contracts.Booking.NewBooking
            {
                Username = "SampleUserName",
                HotelId = "SampleHotelId",
                CheckinDateTime = DateTime.Now.AddDays(1),
                CheckoutDateTime = DateTime.Now.AddDays(2)
            };

            //Act
            string result = await bookingService.AddNewBookingAsync(newBooking);

            //Assert
            result.Should().BeEquivalentTo("User ID does not match any records!");
        }
        [Fact]
        public async Task AddNewBookingAsync_AllValidDataGiven_BookingIsSuccessfulAsync()
        {
            //Arrange
            HotelInn.Domain.Models.Hotel sampleHotel = new HotelInn.Domain.Models.Hotel
            {
                HotelId = "SampleHotelId",
                Availability = true
            };
            hotelRepoStub.Setup(repo => repo.FindHotelAsync(It.IsAny<string>())).ReturnsAsync(sampleHotel);
            HotelInn.Domain.Models.User sampleUser = new HotelInn.Domain.Models.User
            {
                Name = "SampleUserName"
            };
            userRepoStub.Setup(repo => repo.FindUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(sampleUser);
            IBookingService bookingService = new BookingService(new Lazy<IBookingRepository>(bookingRepoStub.Object), new Lazy<IHotelRepository>(hotelRepoStub.Object), new Lazy<IUserRepository>(userRepoStub.Object));
            HotelInn.Contracts.Booking.NewBooking newBooking = new HotelInn.Contracts.Booking.NewBooking
            {
                Username = "SampleUserName",
                HotelId = "SampleHotelId",
                CheckinDateTime = DateTime.Now.AddDays(1),
                CheckoutDateTime = DateTime.Now.AddDays(2)
            };

            //Act
            string result = await bookingService.AddNewBookingAsync(newBooking);

            //Assert
            result.Should().BeEquivalentTo("Saved successfully!");
        }


        /// <summary>
        /// Tests to cancel a booking
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteBookingAsync_InvalidBookingIdGiven_ReturnsBookingNotFoundAsync()
        {
            //Arrange
            bookingRepoStub.Setup(repo => repo.FindBookingAsync(It.IsAny<string>())).ReturnsAsync((HotelInn.Domain.Models.Booking)null);
            IBookingService bookingService = new BookingService(new Lazy<IBookingRepository>(bookingRepoStub.Object), new Lazy<IHotelRepository>(hotelRepoStub.Object), new Lazy<IUserRepository>(userRepoStub.Object));
            string bookingId = "SampleBookingId";

            //Act
            string result = await bookingService.DeleteBookingAsync(bookingId);

            //Assert
            result.Should().BeEquivalentTo("Booking ID does not match any records!");
        }
        [Fact]
        public async Task DeleteBookingAsync_ValidBookingIdGiven_ReturnsBookingCancelledAsync()
        {
            //Arrange
            HotelInn.Domain.Models.Booking sampleBooking = new HotelInn.Domain.Models.Booking { BookingId = "SampleBookingId" };
            bookingRepoStub.Setup(repo => repo.FindBookingAsync(It.IsAny<string>())).ReturnsAsync(sampleBooking);
            IBookingService bookingService = new BookingService(new Lazy<IBookingRepository>(bookingRepoStub.Object), new Lazy<IHotelRepository>(hotelRepoStub.Object), new Lazy<IUserRepository>(userRepoStub.Object));
            string bookingId = "SampleBookingId";

            //Act
            string result = await bookingService.DeleteBookingAsync(bookingId);

            //Assert
            result.Should().BeEquivalentTo("Delete successfully!");
        }


        /// <summary>
        /// Tests for finding a booking details
        /// </summary>
        [Fact]
        public async void FindBookingAsync_InvalidBookingIdGiven_ReturnsNullAsync()
        {
            //Arrange
            bookingRepoStub.Setup(repo => repo.FindBookingAsync(It.IsAny<string>())).ReturnsAsync((HotelInn.Domain.Models.Booking)null);
            IBookingService bookingService = new BookingService(new Lazy<IBookingRepository>(bookingRepoStub.Object), new Lazy<IHotelRepository>(hotelRepoStub.Object), new Lazy<IUserRepository>(userRepoStub.Object));
            string bookingId = "SampleBookingId";

            //Act
            HotelInn.Contracts.Booking.Booking result = await bookingService.FindBookingAsync(bookingId);

            //Assert
            result.Should().BeNull();
        }
        [Fact]
        public async void FindBookingAsync_ValidBookingIdGiven_ReturnsBookingObjectAsync()
        {
            //Arrange
            HotelInn.Domain.Models.Booking sampleBooking = new HotelInn.Domain.Models.Booking { BookingId = "SampleBookingId" };
            bookingRepoStub.Setup(repo => repo.FindBookingAsync(It.IsAny<string>())).ReturnsAsync(sampleBooking);
            IBookingService bookingService = new BookingService(new Lazy<IBookingRepository>(bookingRepoStub.Object), new Lazy<IHotelRepository>(hotelRepoStub.Object), new Lazy<IUserRepository>(userRepoStub.Object));
            HotelInn.Contracts.Booking.Booking expectedBooking = new HotelInn.Contracts.Booking.Booking
            {
                BookingId = sampleBooking.BookingId
            };

            //Act
            HotelInn.Contracts.Booking.Booking result = await bookingService.FindBookingAsync(expectedBooking.BookingId);

            //Assert
            result.Should().BeEquivalentTo(expectedBooking);
        }
    }
}
