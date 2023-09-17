﻿namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IMemoryCacheHelper _cacheHelper;
    private readonly Dotnet9DbContext _dbContext;
    private readonly CategoryManager _manager;
    private readonly ICategoryRepository _repository;
    private readonly IOptionsSnapshot<SiteOptions> _siteOptions;

    public CategoryController(Dotnet9DbContext dbContext, ICategoryRepository repository,
        IAlbumRepository albumRepository,
        CategoryManager manager,
        IOptionsSnapshot<SiteOptions> siteOptions, IMemoryCacheHelper cacheHelper)
    {
        _dbContext = dbContext;
        _repository = repository;
        _albumRepository = albumRepository;
        _manager = manager;
        _siteOptions = siteOptions;
        _cacheHelper = cacheHelper;
    }

    [HttpGet]
    [NoWrapper]
    public async Task<GetCategoryListResponse> List([FromQuery] GetCategoryListRequest request)
    {
        (Category[]? Categories, long Count) result = await _repository.GetListAsync(request);
        Dictionary<Guid, string>? categoryIdAndNames = await _dbContext.GetCategoryIdAndNames(_cacheHelper);
        List<CategoryDto> categoryDtos = new();
        if (result.Categories == null)
        {
            return new GetCategoryListResponse(categoryDtos, result.Count, true, request.PageSize, request.Current);
        }

        foreach (Category category in result.Categories)
        {
            string parentName = string.Empty;
            if (categoryIdAndNames != null && category.ParentId != null &&
                categoryIdAndNames.ContainsKey(category.ParentId.Value))
            {
                parentName = categoryIdAndNames[category.ParentId.Value];
            }

            CategoryDto categoryDto = category.Adapt<CategoryDto>();
            categoryDto.ParentId = category.ParentId;
            categoryDto.ParentName = parentName;
            categoryDto.Cover = $"{_siteOptions.Value.AssetsRemotePath}/{categoryDto.Cover}";
            categoryDtos.Add(categoryDto);
        }

        return new GetCategoryListResponse(categoryDtos, result.Count, true, request.PageSize, request.Current);
    }

    [HttpGet]
    [Route("/api/[controller]/tree")]
    public async Task<List<CategoryTreeItemDto>> GetCategoryTree()
    {
        List<CategoryTreeItemDto> treeItems = new();
        List<Category>? listItems = await _dbContext.Categories!.ToListAsync();

        void ReadChildren(CategoryTreeItemDto? parentItem, Guid? parentId)
        {
            List<Category> items = listItems.Where(x => x.ParentId == parentId).ToList();

            foreach (Category item in items)
            {
                CategoryTreeItemDto data = new()
                {
                    Title = item.Name,
                    Value = item.Id.ToString(),
                    Key = item.Id.ToString()
                };

                parentItem?.Children.Add(data);

                if (parentId == null)
                {
                    treeItems.Add(data);
                }

                ReadChildren(data, item.Id);
            }
        }

        ReadChildren(null, null);


        return treeItems;
    }

    [HttpDelete]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<int> Delete([FromBody] DeleteCategoryRequest request)
    {
        return await _repository.DeleteAsync(request.Ids);
    }

    [HttpPost]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<CategoryDto> Add([FromBody] AddCategoryRequest request)
    {
        Category category = await _manager.CreateAsync(null, request.SequenceNumber, request.Name, request.Slug,
            request.Cover, request.Description, request.Visible, request.ParentId);
        EntityEntry<Category> categoryFromDb = await _dbContext.AddAsync(category);
        await _dbContext.SaveChangesAsync();
        return categoryFromDb.Entity.Adapt<CategoryDto>();
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<CategoryDto> Update(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        Category category = await _manager.CreateAsync(id, request.SequenceNumber, request.Name, request.Slug,
            request.Cover, request.Description, request.Visible, request.ParentId);
        EntityEntry<Category> categoryFromDb = _dbContext.Update(category);
        await _dbContext.SaveChangesAsync();
        return categoryFromDb.Entity.Adapt<CategoryDto>();
    }
}