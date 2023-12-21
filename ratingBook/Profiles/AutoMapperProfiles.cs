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
            .ForMember(dest => dest.LibraryId, opt => opt.MapFrom(src => src.Library.Id));

        }
    }
}
