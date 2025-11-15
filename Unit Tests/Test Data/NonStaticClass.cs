using System.Diagnostics.Metrics;

namespace Unit_Tests.Test_Data
{
    public class NonStaticClass
    {
        public string PublicField = "";

        public string PublicProperty { get; set; } = "";

        private string PrivateField = "";

        private string PrivateProperty { get; set; } = "";

        public static string PublicStaticField = "";

        public static string PublicStaticProperty { get; set; } = "";

        private static string PrivateStaticField = "";

        private static string PrivateStaticProperty { get; set; } = "";

        public int Counter { get; set; } = 0;

        public void PublicMethod()
        {
            Counter++;
        }

        private int PrivateMethod()
        {
            Counter++;
            return Counter;
        }

        public static int StaticCounter { get; set; } = 2;

        public static void PublicStaticMethod()
        {
            StaticCounter++;
        }

        private static int PrivateStaticMethod()
        {
            StaticCounter++;
            return StaticCounter;
        }
    }
}