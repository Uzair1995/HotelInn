using HotelInn.Domain.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelInn.Domain.Models
{
    public class UserReview : IDatabaseModel
    {
        [Key]
        public string ReviewId { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        [ForeignKey(nameof(Hotel))]
        public string HotelId { get; set; }
        public DateTime ReviewDateTime { get; set; }
        public string Review { get; set; }
    }
}
