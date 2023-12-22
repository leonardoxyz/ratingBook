namespace ratingBook.Model.Dto
{
    public record struct BookDto(Guid LibraryId, string Title, string Author, double Rating);
}
