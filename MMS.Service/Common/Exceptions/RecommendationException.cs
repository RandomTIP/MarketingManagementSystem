namespace MMS.Service.Common.Exceptions
{
    public class RecommendationException : Exception
    {
        public RecommendationException() : base("Creation of Recommendation failed!")
        {

        }

        public RecommendationException(string message) : base(message)
        {

        }
    }
}
