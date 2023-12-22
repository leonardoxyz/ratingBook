namespace ratingBook.Model
{
    public class Library
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Address { get; set; } = string.Empty;

        public List<Book>? Books { get; set; }
    }
}
