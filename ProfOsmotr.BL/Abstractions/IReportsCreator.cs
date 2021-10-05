﻿using ProfOsmotr.BL.Models;
using ProfOsmotr.BL.Models.ReportData;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    public interface IReportsCreator
    {
        Task<BaseFileResult> CreateCheckupStatusesMedicalReport(params CheckupStatusMedicalReportData[] datas);

        Task<BaseFileResult> CreateCheckupStatusExcerpt(params CheckupStatusExcerptData[] datas);
    }
}