using System.Reflection;
using Unit_Tests.Test_Data;
using DanielSteginkUtils.Utilities;

namespace Unit_Tests1.Utilities
{
    [TestClass]
    public sealed class ClassIntegrationsTests
    {
        [TestMethod]
        public void NonStaticClassTests()
        {
            #region Get/Set for Public/Private Fields/Properties
            Dictionary<string, string> values = new Dictionary<string, string>()
            {
                { "PublicField", "F1" },
                { "PublicProperty", "P1" },
                { "PublicStaticField", "F1_S" },
                { "PublicStaticProperty", "P1_S" },
                { "PrivateField", "F2" },
                { "PrivateProperty", "P2" },
                { "PrivateStaticField", "F2_S" },
                { "PrivateStaticProperty", "P2_S" },
            };

            NonStaticClass input = new NonStaticClass();
            foreach (string fieldName in values.Keys)
            {
                if (fieldName.Contains("Field"))
                {
                    ClassIntegrations.SetField(input, fieldName, values[fieldName]);
                }
                else
                {
                    ClassIntegrations.SetProperty(input, fieldName, values[fieldName]);
                }
            }

            foreach (string fieldName in values.Keys)
            {
                string value;
                if (fieldName.Contains("Field"))
                {
                    value = ClassIntegrations.GetField<NonStaticClass, string>(input, fieldName);
                }
                else
                {
                    value = ClassIntegrations.GetProperty<NonStaticClass, string>(input, fieldName);
                }

                Assert.AreEqual(values[fieldName], value);
            }
            #endregion

            #region Call for Public/Private Methods
            ClassIntegrations.CallFunction<NonStaticClass, object>(input, "PublicMethod", new object[] { });
            int result = ClassIntegrations.CallFunction<NonStaticClass, int>(input, "PrivateMethod", new object[] { });
            Assert.AreEqual(input.Counter, result);

            ClassIntegrations.CallFunction<NonStaticClass, object>(input, "PublicStaticMethod", new object[] { });
            result = ClassIntegrations.CallFunction<NonStaticClass, int>(input, "PrivateStaticMethod", new object[] { });
            Assert.AreEqual(NonStaticClass.StaticCounter, result);
            #endregion
        }

        [TestMethod]
        public void StaticClassTests()
        {
            #region Get/Set for Public/Private Fields/Properties
            Dictionary<string, string> values = new Dictionary<string, string>()
            {
                { "PublicStaticField", "F1_S" },
                { "PublicStaticProperty", "P1_S" },
                { "PrivateStaticField", "F2_S" },
                { "PrivateStaticProperty", "P2_S" },
            };

            foreach (string fieldName in values.Keys)
            {
                if (fieldName.Contains("Field"))
                {
                    ClassIntegrations.SetField<StaticClass>(null, fieldName, values[fieldName]);
                }
                else
                {
                    ClassIntegrations.SetProperty<StaticClass>(null, fieldName, values[fieldName]);
                }
            }

            foreach (string fieldName in values.Keys)
            {
                string value;
                if (fieldName.Contains("Field"))
                {
                    value = ClassIntegrations.GetField<StaticClass, string>(null, fieldName);
                }
                else
                {
                    value = ClassIntegrations.GetProperty<StaticClass, string>(null, fieldName);
                }

                Assert.AreEqual(values[fieldName], value);
            }
            #endregion

            #region Call for Public/Private Methods
            ClassIntegrations.CallFunction<StaticClass, object>(null, "PublicStaticMethod", new object[] { });
            int result = ClassIntegrations.CallFunction<StaticClass, int>(null, "PrivateStaticMethod", new object[] { });
            Assert.AreEqual(StaticClass.StaticCounter, result);
            #endregion
        }
    }
}