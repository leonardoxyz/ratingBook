namespace ratingBook.Model.Dto
{
    public record struct BookDto(Guid LibraryId, string title, string author, double rating);
}
