﻿namespace Dotnet9.Web.Pages;

public class PrivacyModel : PageModel
{
    private readonly IDistributedCacheHelper _cacheHelper;
    private readonly IPrivacyRepository _repository;

    public PrivacyModel(IPrivacyRepository repository,
        IDistributedCacheHelper cacheHelper)
    {
        _repository = repository;
        _cacheHelper = cacheHelper;
    }

    public string? ContentHtml { get; set; }

    public async Task OnGet()
    {
        string cacheKey = "Privacy";

        async Task<string?> GetDataFromDb()
        {
            var data = await _repository.GetAsync();
            return data?.Content.Convert2Html();
        }

        ContentHtml = await _cacheHelper.GetOrCreateAsync(cacheKey,
            async e => await GetDataFromDb());
    }
}