using ProfOsmotr.BL.Models;
using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    public class ExaminationsStatisticsResponse : BaseResponse<IEnumerable<ExaminationsStatisticsData>>
    {
        public ExaminationsStatisticsResponse(IEnumerable<ExaminationsStatisticsData> entity) : base(entity)
        {
        }

        public ExaminationsStatisticsResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}