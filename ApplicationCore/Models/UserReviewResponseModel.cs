using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class UserReviewResponseModel
    {
        public int UserId { get; set; }
        public List<MovieReviewResponseModel> MovieReviews { get; set; }
    }
}