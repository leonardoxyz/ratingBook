namespace ratingBook.Model.Dto
{
    public record struct LibraryDto(string Address, List<BookDto> Books);
}
