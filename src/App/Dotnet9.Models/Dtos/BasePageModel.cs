﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dotnet9.Models.Dtos;

public class BasePageModel
{
    /// <summary>
    ///     页码
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    ///     页大小
    /// </summary>
    public int PageSize { get; set; }

    [BindNever]
    public int Skip
    {
        get
        {
            if (PageIndex < 1)
            {
                PageIndex = 1;
            }

            if (PageSize < 10)
            {
                PageSize = 10;
            }

            int v = (PageIndex - 1) * PageSize;
            return v <= 0 ? 0 : v;
        }
    }
}

public class PageDto<T> where T : class, new()
{
    public PageDto(int total, List<T> data)
    {
        Total = total;
        Data = data;
    }

    public int Total { get; set; }

    public List<T> Data { get; set; } = new();
}