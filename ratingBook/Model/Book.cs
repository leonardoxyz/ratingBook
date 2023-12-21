﻿namespace ratingBook.Model
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string  Author { get; set; } = string.Empty;
        public double Rating { get; set; }

        public Library Library { get; set; }
}
}
