﻿using AuroraModularis.Core;
using FluentValidation;

namespace AuroraModularis.Modules.I18N.Models;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithLocalisedMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string key)
    {
        var localisatinService = ServiceContainer.Current.Resolve<ILocalisationService>();

        return rule.WithMessage(localisatinService.GetString(key));
    }
}
