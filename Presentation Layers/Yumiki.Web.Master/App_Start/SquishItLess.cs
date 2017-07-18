[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Yumiki.Web.Master.App_Start.SquishItLess), "Start")]

namespace Yumiki.Web.Master.App_Start
{
    using SquishIt.Framework;
    using SquishIt.Less;

    public class SquishItLess
    {
        public static void Start()
        {
            Bundle.RegisterStylePreprocessor(new LessPreprocessor());
        }
    }
}