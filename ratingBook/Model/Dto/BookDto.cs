namespace ratingBook.Model.Dto
{
    public record struct BookDto(Guid Id,Guid LibraryId, string Title, string Author, double Rating);
}
