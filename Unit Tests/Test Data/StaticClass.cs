namespace Unit_Tests.Test_Data
{
    public class StaticClass
    {
        public static string PublicStaticField = "";

        public static string PublicStaticProperty { get; set; } = "";

        private static string PrivateStaticField = "";

        private static string PrivateStaticProperty { get; set; } = "";

        public static int StaticCounter { get; set; } = 0;

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