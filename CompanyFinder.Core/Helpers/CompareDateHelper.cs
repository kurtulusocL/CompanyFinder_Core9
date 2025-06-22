
namespace CompanyFinder.Core.Helpers
{
    public class CompareDateHelper
    {
        public static bool IsDateOld(DateTime dateFromDatabase)
        {
            return dateFromDatabase < DateTime.Now;
        }
    }
}
