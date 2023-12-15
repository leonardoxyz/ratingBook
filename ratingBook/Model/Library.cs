namespace ratingBook.Model
{
    public class Library
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Adress { get; set; } = string.Empty;
    }
}
