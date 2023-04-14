﻿namespace Dotnet9.Service.Infrastructure.Repositories;

public class AlbumRepository : Repository<Dotnet9DbContext, Album, Guid>, IAlbumRepository
{
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public AlbumRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork,
        IMultilevelCacheClient multilevelCacheClient) : base(context, unitOfWork)
    {
        _multilevelCacheClient = multilevelCacheClient;
    }

    public async Task<Album?> FindByIdAsync(Guid id)
    {
        return await Context.Albums!.Include(album => album.Categories).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Album?> FindByNameAsync(string name)
    {
        return await Context.Albums!.Include(album => album.Categories).FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Album?> FindBySlugAsync(string slug)
    {
        return await Context.Albums!.Include(album => album.Categories).FirstOrDefaultAsync(x => x.Slug == slug);
    }

    public async Task<List<AlbumBrief>> GetAllBriefAsync()
    {
        //TimeSpan? timeSpan = null;
        //var key = $"{nameof(AlbumRepository)}_{nameof(GetAllBriefAsync)}";
        //var albumList = await _multilevelCacheClient.GetOrSetAsync(key, () =>
        //{
        var albums = await Context.Set<Album>()
            .Select(cat => new AlbumBrief(cat.Name, cat.Slug, cat.Cover,
                cat.Description,
                Context.Set<BlogAlbum>().Count(d => d.AlbumId == cat.Id))).ToListAsync();
        var distinctAlbums = from album in albums
            where album.BlogCount > 0
            orderby album.BlogCount descending
            select album;
        var distinctAlbumList = distinctAlbums.ToList();
        return distinctAlbumList;
        //    if (distinctAlbumList.Any())
        //    {
        //        return new CacheEntry<List<AlbumBrief>>(distinctAlbumList, TimeSpan.FromDays(3))
        //        {
        //            SlidingExpiration = TimeSpan.FromMinutes(5)
        //        };
        //    }

        //    timeSpan = TimeSpan.FromSeconds(5);
        //    return new CacheEntry<List<AlbumBrief>>(distinctAlbumList);
        //}, options =>
        //    options.AbsoluteExpirationRelativeToNow = timeSpan);

        //return albumList;
    }
}