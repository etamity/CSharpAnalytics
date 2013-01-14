﻿using System;
#if WINDOWS_STORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using CSharpAnalytics.CustomVariables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace CSharpAnalytics.Test
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void Configuration_Constructor_With_Required_Parameters_Sets_Correct_Properties()
        {
            var sessionTimeout = TimeSpan.FromDays(10);
            var configuration = new Configuration("UA-1234-5", "hostName", sessionTimeout);

            Assert.AreEqual("UA-1234-5", configuration.AccountId);
            Assert.AreEqual("hostName", configuration.HostName);
            Assert.AreEqual(sessionTimeout, configuration.SessionTimeout);
        }

        [TestMethod]
        public void Configuration_Constructor_With_Required_Parameters_Sets_Correct_Defaults()
        {
            var configuration = new Configuration("UA-1234-5", "hostName");

            Assert.IsFalse(configuration.CalculateHostNameHash);
            Assert.IsTrue(configuration.AnonymizeIp);
            Assert.IsFalse(configuration.SendClientTime);
            Assert.AreEqual(20, configuration.SessionTimeout.TotalMinutes);
            Assert.IsFalse(configuration.UseSsl);
        }

        [TestMethod]
        public void Configuration_GetHostNameHash_Returns_Correct_Hash_When_CalculateHostNameHash_Is_True()
        {
            var attackPatternConfiguration = new Configuration("UA-1234-5", "attackpattern.com") { CalculateHostNameHash = true };
            var stickerTalesConfiguration = new Configuration("UA-1234-6", "stickertales.com") { CalculateHostNameHash = true };
            var damiengConfiguration = new Configuration("UA-2345-6", "damieng.com") { CalculateHostNameHash = true };

            Assert.AreEqual(162214764, attackPatternConfiguration.GetHostNameHash());
            Assert.AreEqual(59641711, stickerTalesConfiguration.GetHostNameHash());
            Assert.AreEqual(247163921, damiengConfiguration.GetHostNameHash());
        }

        [TestMethod]
        public void Configuration_GetHostNameHash_Returns_One_When_CalculateHostNameHash_Is_False()
        {
            var attackPatternConfiguration = new Configuration("UA-1234-5", "attackpattern.com") { CalculateHostNameHash = false };
            Assert.AreEqual(1, attackPatternConfiguration.GetHostNameHash());
        }

#if WINDOWS_STORE
        [TestMethod]
        public void Configuration_Constructor_Throws_ArgumentException_If_AccountID_Does_Not_Start_With_UA()
        {
            Assert.ThrowsException<ArgumentException>(() => new Configuration("NO-1234-5", "host"));
        }

        [TestMethod]
        public void Configuration_Constructor_Throws_ArgumentException_If_AccountID_Does_Not_Have_Two_Numeric_Parts()
        {
            Assert.ThrowsException<ArgumentException>(() => new Configuration("UA-1234", "host"));
        }

#endif

#if NET45
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CustomVariable_Constructor_Throws_ArgumentOutOfRange_If_Enum_Undefined()
        {
            var configuration = new Configuration("NO-1234-5", "host");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Configuration_Constructor_Throws_ArgumentException_If_AccountID_Does_Not_Have_Two_Numeric_Parts()
        {
            var configuration = new Configuration("UA-1234", "host");
        }
#endif
    }
}