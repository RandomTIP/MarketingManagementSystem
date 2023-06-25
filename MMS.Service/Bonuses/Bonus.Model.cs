namespace MMS.Service.Bonuses
{
    public partial class Bonus
    {
        public int DistributorId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime CalculationDate { get; private set; }
        public decimal DistributorTotalSales { get; private set; }
        public decimal FirstLevelTotalSales { get; private set; }
        public decimal SecondLevelTotalSales { get; private set; }
        public decimal DistributorBonusShare { get; private set; }
        public decimal FirstLevelBonusShare { get; private set; }
        public decimal SecondLevelBonusShare { get; private set; }
        public decimal TotalBonus { get; private set; }
    }
}
