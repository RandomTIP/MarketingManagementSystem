namespace MMS.Service.Common.Exceptions
{
    public class RecommendationCountException : RecommendationException
    {
        public RecommendationCountException() : base("Recommendation Count can not exceed 3!")
        {

        }
    }
}
