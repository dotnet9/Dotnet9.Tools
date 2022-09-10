﻿namespace Dotnet9.WebAPI.Domain.Shared.ActionLogs;

public class AddActionLogRequest
{
    public string UId { get; set; } = null!;
    public string UA { get; set; } = null!;
    public string OS { get; set; } = null!;
    public string Browser { get; set; } = null!;
    public string IP { get; set; } = null!;
    public string? Referer { get; set; }
    public string? AccessName { get; set; }
    public string? Original { get; set; }
    public string? Url { get; set; }
    public string? Controller { get; set; }
    public string? Action { get; set; }
    public string? Method { get; set; }
    public string? Arguments { get; set; }
    public double Duration { get; set; }
}

public class AddActionLogRequestValidator : AbstractValidator<AddActionLogRequest>
{
    public AddActionLogRequestValidator()
    {
        RuleFor(x => x.UId).NotNull().Length(ActionLogConsts.MinUIdLength, ActionLogConsts.MaxUIdLength)
            .WithMessage($"UId长度范围[{ActionLogConsts.MinUIdLength},{ActionLogConsts.MaxUIdLength}]");
        RuleFor(x => x.UA).NotNull().Length(ActionLogConsts.MinUALength, ActionLogConsts.MaxUALength)
            .WithMessage($"UA长度范围[{ActionLogConsts.MinUALength},{ActionLogConsts.MaxUALength}]");
        RuleFor(x => x.OS).NotNull().Length(ActionLogConsts.MinOSLength, ActionLogConsts.MaxOSLength)
            .WithMessage($"OS长度范围[{ActionLogConsts.MinOSLength},{ActionLogConsts.MaxOSLength}]");
        RuleFor(x => x.Browser).NotNull().Length(ActionLogConsts.MinBrowserLength, ActionLogConsts.MaxBrowserLength)
            .WithMessage($"Browser长度范围[{ActionLogConsts.MinBrowserLength},{ActionLogConsts.MaxBrowserLength}]");
        RuleFor(x => x.Referer).MaximumLength(ActionLogConsts.MaxRefererLength)
            .WithMessage($"Referer长度不能大于{ActionLogConsts.MaxRefererLength}");
        RuleFor(x => x.AccessName).MaximumLength(ActionLogConsts.MaxAccessName)
            .WithMessage($"AccessName长度不能大于{ActionLogConsts.MaxAccessName}");
        RuleFor(x => x.Original).MaximumLength(ActionLogConsts.MaxOriginalLength)
            .WithMessage($"Original长度不能大于{ActionLogConsts.MaxOriginalLength}");
        RuleFor(x => x.IP).NotNull().Length(ActionLogConsts.MinIPLength, ActionLogConsts.MaxIPLength)
            .WithMessage($"IP长度范围[{ActionLogConsts.MinIPLength},{ActionLogConsts.MaxIPLength}]");
        RuleFor(x => x.Url).MaximumLength(ActionLogConsts.MaxUrlLength)
            .WithMessage($"Url长度不能大于{ActionLogConsts.MaxUrlLength}");
        RuleFor(x => x.Controller).MaximumLength(ActionLogConsts.MaxControllerLength)
            .WithMessage($"Controller长度不能大于{ActionLogConsts.MaxControllerLength}");
        RuleFor(x => x.Action).MaximumLength(ActionLogConsts.MaxActionLength)
            .WithMessage($"Action长度不能大于{ActionLogConsts.MaxActionLength}");
        RuleFor(x => x.Method).MaximumLength(ActionLogConsts.MaxMethodLength)
            .WithMessage($"Method长度不能大于{ActionLogConsts.MaxMethodLength}");
        RuleFor(x => x.Arguments).MaximumLength(ActionLogConsts.MaxArgumentsLength)
            .WithMessage($"Arguments长度不能大于{ActionLogConsts.MaxArgumentsLength}");
    }
}