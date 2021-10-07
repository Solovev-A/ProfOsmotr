using ProfOsmotr.BL.Abstractions;
using System;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class DoubleField : IReportField
    {
        protected double value;

        public DoubleField()
        {
            value = 0;
        }

        public DoubleField(double initialValue = 0)
        {
            value = initialValue;
        }

        public string Value => Math.Round(value, 2).ToString("0.00");
    }
}