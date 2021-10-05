using System;
using System.Collections.Generic;

namespace ProfOsmotr.BL.Models.ReportData
{
    internal class MultiItemReportData<TItem>
    {
        public IEnumerable<TItem> Reports { get; }

        public MultiItemReportData(IEnumerable<TItem> reports)
        {
            Reports = reports ?? throw new ArgumentNullException(nameof(reports));
        }
    }
}