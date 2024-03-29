using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain.BlogPosts;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<BlogPostDto, BlogPost>().ReverseMap();
        CreateMap<BlogCommentDto, BlogComment>().ReverseMap();
        CreateMap<BlogRatingDto, BlogRating>().ReverseMap();
    }
}