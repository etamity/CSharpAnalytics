﻿using System;
using CSharpAnalytics.Internal;
#if WINDOWS_STORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace CSharpAnalytics.Test.Internal
{
    [TestClass]
    public class ParameterDefinitionTests
    {
        [TestMethod]
        public void ParameterDefinition_Constructor_Sets_All_Properties()
        {
            Func<string, string> formatter = s => s + "great";
            var parameterDefinition = new ParameterDefinition("name", "label", formatter);

            Assert.AreEqual("name", parameterDefinition.Name);
            Assert.AreEqual("label", parameterDefinition.Label);
            Assert.AreEqual(formatter, parameterDefinition.Formatter);
            Assert.AreEqual("so great", parameterDefinition.Formatter("so "));
        }

        [TestMethod]
        public void ParameterDefinition_Constructor_Sets_Default_Formatter()
        {
            var parameterDefinition = new ParameterDefinition("name", "label");

            Assert.IsNotNull(parameterDefinition.Formatter);
            Assert.AreEqual("testing", parameterDefinition.Formatter("testing"));
        }
    }
}