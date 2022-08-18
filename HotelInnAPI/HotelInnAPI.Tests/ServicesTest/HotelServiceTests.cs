using System;
using System.Threading.Tasks;
using FluentAssertions;
using HotelInn.Domain.IRepositories;
using HotelInn.Services.Abstractions;
using HotelInn.Services.Services;
using Moq;
using Xunit;

namespace HotelInnAPI.Tests.ServicesTest
{
    public class HotelServiceTests
    {
        private readonly Mock<IHotelRepository> hotelRepoStub = new Mock<IHotelRepository>();

        /// <summary>
        /// Tests for adding a new hotel
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddNewHotelAsync_NoDataPassed_ReturnsValueCannotBeNullAsync()
        {
            //Arrange
            IHotelService hotelService = new HotelService(new Lazy<IHotelRepository>(hotelRepoStub.Object));

            //Act
            string result = await hotelService.AddNewHotelAsync(null);

            //Assert
            result.Should().BeEquivalentTo("Value cannot be null!");
        }
        [Fact]
        public async Task AddNewHotelAsync_AllValidDataGiven_AddingIsSuccessfulAsync()
        {
            //Arrange
            HotelInn.Contracts.Hotel.NewHotel sampleHotel = new HotelInn.Contracts.Hotel.NewHotel
            {
                Availability = true
            };
            IHotelService hotelService = new HotelService(new Lazy<IHotelRepository>(hotelRepoStub.Object));
            
            //Act
            string result = await hotelService.AddNewHotelAsync(sampleHotel);

            //Assert
            result.Should().BeEquivalentTo("Saved successfully!");
        }
    }
}
