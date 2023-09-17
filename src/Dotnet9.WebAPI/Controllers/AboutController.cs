﻿namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AboutController : ControllerBase
{
    private readonly IMemoryCacheHelper _cacheHelper;
    private readonly Dotnet9DbContext _dbContext;
    private readonly AboutManager _manager;
    private readonly IAboutRepository _repository;
    private const string GetAboutCacheKey = "AboutController.GetAbout";

    public AboutController(Dotnet9DbContext dbContext, IAboutRepository repository, AboutManager manager,
        IMemoryCacheHelper cacheHelper)
    {
        _dbContext = dbContext;
        _repository = repository;
        _manager = manager;
        _cacheHelper = cacheHelper;
    }

    [HttpGet]
    [NoWrapper]
    public async Task<ActionResult<AboutDto>> Get()
    {
        async Task<AboutDto?> GetAboutFromDb()
        {
            var aboutFromDb = await _repository.GetAsync();
            return aboutFromDb == null ? null : new AboutDto(aboutFromDb.Content!);
        }

        var about = await _cacheHelper.GetOrCreateAsync(GetAboutCacheKey,
            async e => await GetAboutFromDb());
        if (about == null)
        {
            return NotFound();
        }

        return about;
    }

    [HttpPost]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<ActionResult<ResponseResult<bool>>> AddOrUpdate(AddOrUpdateAboutRequest request)
    {
        var about = await _repository.GetAsync();
        if (about == null)
        {
            about = _manager.Create(request.Content);
            await _dbContext.AddAsync(about);
        }
        else
        {
            about.ChangeContent(request.Content);
        }

        await _dbContext.SaveChangesAsync();
        _cacheHelper.Remove(GetAboutCacheKey);
        return ResponseResult<bool>.GetSuccess(true);
    }
}