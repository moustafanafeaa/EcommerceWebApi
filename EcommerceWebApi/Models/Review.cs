﻿namespace EcommerceWebApi.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }

    }
}
