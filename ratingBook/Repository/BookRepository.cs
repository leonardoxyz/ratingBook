using ratingBook.Data;
using ratingBook.Model;

namespace ratingBook.Repository
{
    public class BookRepository : RepositoryBase<Book, Guid>, IBookRepository
    {
        public BookRepository(DataContext context) : base(context)
        {

        }
    }
}
