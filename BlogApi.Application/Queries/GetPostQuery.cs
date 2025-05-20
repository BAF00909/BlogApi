using BlogApi.Application.Dtos;
using MediatR;
namespace BlogApi.Application.Queries
{
    public class GetPostQuery: IRequest<(IEnumerable<PostDto> Posts, int TotalCount)>
    {
        public string? Title { get; set; }
        public string? SortBy { get; set; }
        public string? Order {  get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
