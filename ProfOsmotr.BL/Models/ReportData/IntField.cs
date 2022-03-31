using ProfOsmotr.BL.Abstractions;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class IntField : IReportField
    {
        protected int value;

        public IntField()
        {
            value = 0;
        }

        public IntField(int initialValue)
        {
            value = initialValue;
        }

        public string Value => value.ToString();

        public int IntValue => value;

        public int Add(int num)
        {
            value += num;
            return value;
        }

        public int Increment()
        {
            return Add(1);
        }
    }
}