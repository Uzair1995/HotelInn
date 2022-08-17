﻿using HotelInn.Domain.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace HotelInn.Domain.Models
{
    public class User : IDatabaseModel
    {
        [Key]
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Contracts.User.User ToDto()
        {
            return new Contracts.User.User
            {
                UserId = UserId,
                Name = Name,
                City = City,
                Address = Address,
                Country = Country
            };
        }
    }
}
