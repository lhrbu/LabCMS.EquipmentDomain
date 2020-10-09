using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCMS.Shared.Models
{
    public static class EntityFrameworkCoreValueConverters
    {
        public static ValueConverter<DateTimeOffset?,long?> DataTimeOffsetUtcSecondsConverter { get; } =
            new( dateTimeOffset => dateTimeOffset.HasValue ? dateTimeOffset.Value.ToUnixTimeSeconds() : null,
                 offsetSeconds => offsetSeconds.HasValue ? DateTimeOffset.FromUnixTimeSeconds(offsetSeconds.Value) : null
                );

    }
}
