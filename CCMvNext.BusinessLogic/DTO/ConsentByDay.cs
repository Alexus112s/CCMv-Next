using System;

namespace CCMvNext.BusinessLogic.DTO
{
    public class ConsentByDay
    {
        public DateTime Date { get; set; }

        public int ConcentRatePercent => (int)((double)Accepted / Count * 100);

        public int Count { get; set; }

        public int Accepted { get; set; }
    }
}
