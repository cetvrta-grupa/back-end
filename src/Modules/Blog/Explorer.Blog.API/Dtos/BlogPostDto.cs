namespace Explorer.Blog.API.Dtos;

public class BlogPostDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Username { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public List<string>? ImageNames { get; set; }
    public string Status { get; set; }
    public List<BlogRatingDto>? Ratings { get; set; }
    public List<BlogCommentDto>? Comments { get; set; }
}