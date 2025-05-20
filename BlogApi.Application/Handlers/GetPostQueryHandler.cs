using BlogApi.Application.Dtos;
using BlogApi.Application.Interfaces;
using BlogApi.Application.Queries;
using MediatR;

namespace BlogApi.Application.Handlers
{
    public class GetPostQueryHandler: IRequestHandler<GetPostQuery, (IEnumerable<PostDto>, int)>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPostQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(IEnumerable<PostDto>, int)> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var (posts, totalCount) = await _unitOfWork.Posts.GetAllAsync(
                request.Page,
                request.PageSize,
                request.Title,
                request.SortBy,
                request.Order
                );
            var postDtos = posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreateAt = p.CreateAt,
                ImagePath = p.ImagePath
            }).ToList();
            return (postDtos, totalCount);
        }
    }
}
