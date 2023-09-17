﻿namespace Dotnet9.WebAPI.Domain.BlogPosts;

public interface IBlogPostRepository
{
    Task<(BlogPost[]? BlogPosts, long Count)> GetListAsync(GetBlogPostListRequest request);
    Task<(BlogPost[]? BlogPosts, long Count)> GetListByCategoryIdAsync(Guid categoryId, int pageIndex, int pageSize);
    Task<(BlogPost[]? BlogPosts, long Count)> GetListByAlbumIdAsync(Guid albumId, int pageIndex, int pageSize);
    Task<(BlogPost[]? BlogPosts, long Count)> GetListByTagIdAsync(Guid tagId, int pageIndex, int pageSize);
    Task<BlogPostBrief[]?> GetListBriefAsync(string? keywords);
    Task<int> DeleteAsync(Guid[] ids);
    Task<BlogPost?> FindByIdAsync(Guid id);
    Task<BlogPost?> FindByTitleAsync(string name);
    Task<BlogPost?> FindBySlugAsync(string slug);
    Task<bool> IncreaseViewCountAsync(string slug);
    Task<int> IncreaseLikeCountAsync(string slug);
}