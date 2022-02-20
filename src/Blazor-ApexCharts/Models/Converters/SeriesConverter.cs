﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Models
{
    public class SeriesConverter<T> : JsonConverter<List<Series<T>>> where T : class
    {
        public override List<Series<T>> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, List<Series<T>> value, JsonSerializerOptions options)
        {

            if (value == null || !value.Any())
            {
                JsonSerializer.Serialize(writer, (IDataPoint<T>)null, options);
            }
            else
            {
                //var type = value.IsNoAxis
                var series = value.First();
                if (series.IsNoAxis)
                {
                    var data = series.Data.Select(e => (NoAxisPoint<T>)e).Where(e => e.Y != null).Select(e => (decimal)e.Y);
                    JsonSerializer.Serialize(writer, data, typeof(IEnumerable<decimal>), options);
                }
                else
                {
                    JsonSerializer.Serialize(writer, value, options);
                }
            }
        }
    }
}

