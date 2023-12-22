using AutoMapper;
using ratingBook.Model;
using ratingBook.Model.Dto;

namespace ratingBook.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Book, BookDto>()
                            .ReverseMap();
            CreateMap<Library, LibraryDto>().ReverseMap();
        }
    }
}
