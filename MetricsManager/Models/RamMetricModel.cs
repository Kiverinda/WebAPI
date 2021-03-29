﻿using System;

namespace MetricsManager.Models
{
    public class RamMetricModel
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public TimeSpan Time { get; set; }
        public int IdAgent { get; set; }
    }
}
