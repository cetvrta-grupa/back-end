﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Internal;
using FluentResults;

namespace Explorer.Blog.Core.UseCases;

public class BlogPostService : CrudService<BlogPostDto, BlogPost>, IBlogPostService
{
    private readonly IBlogPostRepository _blogPostsRepository;
    private readonly IInternalUserService _userService;

    public BlogPostService(IBlogPostRepository repository, IMapper mapper, IInternalUserService userService) : base(repository, mapper)
    {
        _blogPostsRepository = repository;
        _userService = userService;
    }

    public Result<PagedResult<BlogPostDto>> GetAllNonDraft(int page, int pageSize)
    {
        try
        {
            var blogPosts = _blogPostsRepository.GetAllNonDraft(page, pageSize);
            var blogPostDtos = MapToDto(blogPosts);
            foreach (var blogPostDto in blogPostDtos.Value.Results)
            {
                AddUsername(blogPostDto);
            }

            return blogPostDtos;
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<PagedResult<BlogPostDto>> GetAllByUser(int page, int pageSize, int id)
    {
        try
        {
            var blogPosts = _blogPostsRepository.GetAllByUser(page, pageSize, id);
            var blogPostDtos = MapToDto(blogPosts);
            foreach (var blogPostDto in blogPostDtos.Value.Results)
            {
                AddUsername(blogPostDto);
            }

            return blogPostDtos;
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<PagedResult<BlogPostDto>> GetFilteredByStatus(int page, int pageSize, string blogPostStatus)
    {
        try
        {
            if (!Enum.TryParse(blogPostStatus, true, out BlogPostStatus status))
                throw new ArgumentException("Invalid blog post status value.");

            var blogPosts = _blogPostsRepository.GetFilteredByStatus(page, pageSize, status);
            var blogPostDtos = MapToDto(blogPosts);
            foreach (var blogPostDto in blogPostDtos.Value.Results)
            {
                AddUsername(blogPostDto);
            }

            return blogPostDtos;
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public override Result<BlogPostDto> Get(int id)
    {
        try
        {
            var blogPost = CrudRepository.Get(id);
            var blogPostDto = MapToDto(blogPost);
            AddUsername(blogPostDto);

            return blogPostDto;
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public override Result<BlogPostDto> Update(BlogPostDto blogPostDto)
    {
        try
        {
            var blogPost = CrudRepository.Get(blogPostDto.Id);

            if (blogPost.Status != BlogPostStatus.Draft)
                throw new ArgumentException("Only Blog Posts with Draft Status can be updated.");

            blogPost.Update(MapToDomain(blogPostDto));
            var result = CrudRepository.Update(blogPost);
            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<BlogPostDto> Close(int id)
    {
        try
        {
            var blogPost = _blogPostsRepository.Get(id);
            blogPost.Close();
            var result = _blogPostsRepository.Update(blogPost);
            return MapToDto(result);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }
   
    public Result<BlogPostDto> Rate(int id, BlogRatingDto blogRatingDto)
    {
        var blogPost = _blogPostsRepository.Get(id);

        try
        {
            Enum.TryParse<Rating>(blogRatingDto.Rating, out var rating);

            var blogRating = new BlogRating
            {
                UserId = blogRatingDto.UserId,
                Rating = rating,
                TimeStamp = DateTime.Now
            };

            blogPost.AddRating(blogRating);
            var result = _blogPostsRepository.Update(blogPost);
            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
    
    private void AddUsername(BlogPostDto blogPostDto)
    {
        var user = _userService.Get(blogPostDto.UserId);
        blogPostDto.Username = user.Value.Username;
    }
}