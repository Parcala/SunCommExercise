namespace SunCommunitiesExercise.Models
{
    public class FeeResult
    {
        public decimal Amount { get; set; }
        public bool Preferred { get; set; }
        public decimal BaseRate { get; set; }
        public decimal EffectiveRate { get; set; }
        public decimal CalculatedFee { get; set; }
        public bool Capped { get; set; }
    }
}
