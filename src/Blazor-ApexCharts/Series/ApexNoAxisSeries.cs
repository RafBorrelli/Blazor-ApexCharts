﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApexCharts
{
    public class ApexNoAxisSeries<TItem> : ApexBaseSeries<TItem>, IApexSeries<TItem> where TItem : class
    {
        [Parameter] public Func<TItem, decimal?> YValue { get; set; }
        [Parameter] public Func<IEnumerable<TItem>, decimal?> YAggregate { get; set; }
        [Parameter] public Func<NoAxisPoint<TItem>, object> OrderBy { get; set; }
        [Parameter] public Func<NoAxisPoint<TItem>, object> OrderByDescending { get; set; }
        [Parameter] public NoAxisType NoAxisType { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Chart.AddSeries(this);
        }

        public ChartType GetChartType()
        {
            switch (NoAxisType)
            {

                case NoAxisType.Donut:
                    return ChartType.Donut;
                case NoAxisType.Pie:
                    return ChartType.Pie;
                case NoAxisType.PolarArea:
                    return ChartType.PolarArea;
                case NoAxisType.RadialBar:
                    return ChartType.RadialBar;
                default:
                    throw new SystemException($"SeriesType {NoAxisType} can not be converted to CartType");
            }
        }

        public IEnumerable<IDataPoint<TItem>> GenerateDataPoints(IEnumerable<TItem> items)
        {
            IEnumerable<NoAxisPoint<TItem>> data;

            if (YValue != null)
            {
                data = items.Select(e => new NoAxisPoint<TItem>
                {
                    X = XValue.Invoke(e),
                    Y = YValue.Invoke(e),
                    Items = new List<TItem> { e }
                });

            }
            else if (YAggregate != null)
            {
                data = items.GroupBy(XValue)
               .Select(d => new NoAxisPoint<TItem>
               {
                   X = d.Key,
                   Y = YAggregate.Invoke(d),
                   Items = d.ToList()
               });
            }
            else
            {
                return new List<IDataPoint<TItem>>();
            }


            if (OrderBy != null)
            {
                data = data.OrderBy(OrderBy);
            }
            else if (OrderByDescending != null)
            {
                data = data.OrderByDescending(OrderByDescending);
            }

            return data;
        }

        public IEnumerable<IDataPoint<TItem>> GetData()
        {
            return GenerateDataPoints(Items);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Chart.RemoveSeries(this);
        }


    }
}
