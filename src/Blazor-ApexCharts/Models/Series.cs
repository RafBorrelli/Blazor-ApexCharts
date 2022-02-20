﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApexCharts
{

    /// <summary> 
    /// Chart Series options.
    /// Use ApexNonAxisChartSeries for Pie and Donut charts.
    /// See https://apexcharts.com/docs/options/series/
    ///
    /// According to the documentation at
    /// https://apexcharts.com/docs/series/
    /// Section 1: data can be a list of single numbers
    /// Sections 2.1 and 3.1: data can be a list of tuples of two numbers
    /// Sections 2.2 and 3.2: data can be a list of objects where x is a string
    /// and y is a number
    /// And according to the demos, data can contain null.
    /// https://apexcharts.com/javascript-chart-demos/line-charts/null-values/
    /// </summary>

    public class Series<TItem> where TItem : class
    {
     
        public IEnumerable<IDataPoint<TItem>> Data { get; set; }
        public string Name { get; set; }
        public MixedType? Type { get; set; }

        [JsonIgnore]
        public IApexSeries<TItem> ApexSeries { get; set; }

        [JsonIgnore]
        public bool IsNoAxis => true;

    }

    public enum MixedType
    {
        Line,
        Area,
        Column,
        Bar,
        Scatter,
        Bubble
    }
}
