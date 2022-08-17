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
        private readonly Mock<Lazy<IBookingRepository>> bookingRepoStub = new Mock<Lazy<IBookingRepository>>();
        private readonly Mock<Lazy<IHotelRepository>> hotelRepoStub = new Mock<Lazy<IHotelRepository>>();
        private readonly Mock<Lazy<IUserRepository>> userRepoStub = new Mock<Lazy<IUserRepository>>();

        
        [Fact]
        public async Task AddNewBookingAsync_NoDataPassed_ReturnsValueCannotBeNullAsync()
        {
            //Arrange
            IBookingService bookingService = new BookingService(bookingRepoStub.Object, hotelRepoStub.Object, userRepoStub.Object);

            //Act
            string result = await bookingService.AddNewBookingAsync(null);

            //Assert
            result.Should().BeEquivalentTo("Value cannot be null!");
        }
    }
}
