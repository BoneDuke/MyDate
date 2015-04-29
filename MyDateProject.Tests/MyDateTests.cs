using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDateProject.DataModel;

namespace MyDateProject.Tests
{
    [TestClass]
    public class MyDateTests
    {
        [TestMethod]
        public void ИнициализацияОбъектаЧерезКонструктор1()
        {
            var myDate = new MyDate(1, 1, 2015);
            Assert.AreEqual(myDate.Day, 1);
            Assert.AreEqual(myDate.Month, 1);
            Assert.AreEqual(myDate.Year, 2015);
        }

        [TestMethod]
        public void ИнициализацияОбъектаЧерезКонструктор2()
        {
            try
            {
                var myDate = new MyDate(0, 1, 2015);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail();
            
        }

        [TestMethod]
        public void ИнициализацияОбъектаЧерезКонструктор3()
        {
            try
            {
                var myDate = new MyDate(31, 4, 2015);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail();

        }

        [TestMethod]
        public void ПроверкаВисокосностиГодов()
        {
            Assert.AreEqual(MyDate.IsYearLeap(2015), false);
            Assert.AreEqual(MyDate.IsYearLeap(2000), true);
            Assert.AreEqual(MyDate.IsYearLeap(1900), false);
            Assert.AreEqual(MyDate.IsYearLeap(2008), true);
        }

        [TestMethod]
        public void Високосность29ФевраляОшибка()
        {
            try
            {
                var myDate1 = new MyDate(29, 2, 2015);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void Високосность29Февраля()
        {
            try
            {
                var myDate1 = new MyDate(29, 2, 2012);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.Fail();
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ОшибкиИнициализацииДня1()
        {
            try
            {
                var myDate = new MyDate(32, 1, 2015);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ОшибкиИнициализацииМесяца1()
        {
            try
            {
                var myDate = new MyDate(1, 13, 2015);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(true);
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void ОшибкиИнициализацииМесяца2()
        {
            try
            {
                var myDate = new MyDate(1, 0, 2015);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(true);
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void ЧислоДнейВДате1()
        {
            var myDate = new MyDate(1, 1, 2015);
            int result = 0;
            for (var i = 0; i < myDate.Year; i++)
            {
                if (MyDate.IsYearLeap(i))
                    result += 366;
                else
                    result += 365;
            }

            Assert.AreEqual(myDate.GetDaysCount(), result);
        }

        [TestMethod]
        public void ЧислоДнейВДате2()
        {
            var myDate = new MyDate(1, 1, 2);
            int result = 0;
            for (var i = 0; i < myDate.Year; i++)
            {
                if (MyDate.IsYearLeap(i))
                    result += 366;
                else
                    result += 365;
            }

            Assert.AreEqual(myDate.GetDaysCount(), result);
        }

        [TestMethod]
        public void КонвертацияДатыИзДней1()
        {
            var myDate1 = new MyDate(1, 1, 2015);
            var days1 = myDate1.GetDaysCount();
            var myDate2 = MyDate.GetDateByDays(days1);

            Assert.AreEqual(myDate1.Day, myDate2.Day);
            Assert.AreEqual(myDate1.Month, myDate2.Month);
            Assert.AreEqual(myDate1.Year, myDate2.Year);
        }

        [TestMethod]
        public void КонвертацияДатыИзДней2()
        {
            var myDate1 = new MyDate(30, 11, 2015);
            var days1 = myDate1.GetDaysCount();
            var myDate2 = MyDate.GetDateByDays(days1);

            Assert.AreEqual(myDate1.Day, myDate2.Day);
            Assert.AreEqual(myDate1.Month, myDate2.Month);
            Assert.AreEqual(myDate1.Year, myDate2.Year);
        }

        [TestMethod]
        public void КонвертацияДатыИзДней3()
        {
            var myDate1 = new MyDate(29, 02, 2016);
            var days1 = myDate1.GetDaysCount();
            var myDate2 = MyDate.GetDateByDays(days1);

            Assert.AreEqual(myDate1.Day, myDate2.Day);
            Assert.AreEqual(myDate1.Month, myDate2.Month);
            Assert.AreEqual(myDate1.Year, myDate2.Year);
        }

        [TestMethod]
        public void ПроверкаAddDays1()
        {
            var myDate = new MyDate(01, 01, 2015);
            myDate.AddDays(30);

            Assert.AreEqual(myDate.Day, 31);
            Assert.AreEqual(myDate.Month, 1);
            Assert.AreEqual(myDate.Year, 2015);
        }

        [TestMethod]
        public void ПроверкаAddDays2()
        {
            var myDate = new MyDate(01, 01, 2015);
            myDate.AddDays(130);

            Assert.AreEqual(myDate.Day, 11);
            Assert.AreEqual(myDate.Month, 5);
            Assert.AreEqual(myDate.Year, 2015);
        }

        [TestMethod]
        public void ПроверкаAddDays3()
        {
            var myDate = new MyDate(11, 05, 2015);
            myDate.AddDays(-130);

            Assert.AreEqual(myDate.Day, 1);
            Assert.AreEqual(myDate.Month, 1);
            Assert.AreEqual(myDate.Year, 2015);
        }
    }
}
