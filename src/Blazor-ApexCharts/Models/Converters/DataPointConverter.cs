﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Models
{
    public class DataPointConverter<T> : JsonConverter<IDataPoint<T>>
    {
        public override IDataPoint<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IDataPoint<T> value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                JsonSerializer.Serialize(writer, (IDataPoint<T>)null, options);
            }
            else
            {
                var type = value.GetType();
                if (value is NoAxisPoint<T> noAxisPoint)
                {
                    JsonSerializer.Serialize(writer, noAxisPoint.Y, typeof(decimal), options);
                }
                else
                {
                    JsonSerializer.Serialize(writer, value, type, options);
                }

            }


            //    if (value is DataPoint<T> dataPoint)
            //        JsonSerializer.Serialize(writer, dataPoint, typeof(DataPoint<T>), options);
            //    else if (value is BubblePoint<T> bubblePoint)
            //        JsonSerializer.Serialize(writer, bubblePoint, typeof(BubblePoint<T>), options);
            //    else if (value is ListPoint<T> listPoint)
            //        JsonSerializer.Serialize(writer, listPoint, typeof(ListPoint<T>), options);

            //    else
            //        throw new ArgumentOutOfRangeException(nameof(value), $"Unknown implementation of the interface {nameof(IDataPoint<T>)} for the parameter {nameof(value)}. Unknown implementation: {value?.GetType().Name}");

        }
    }
}

