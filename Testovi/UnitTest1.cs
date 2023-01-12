using Banka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Testovi
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            UpraviteljTransakcijama ut = new UpraviteljTransakcijama();

            Assert.ThrowsException<InvalidBankAccountException>(() => ut.PrebaciSredstva("HR77", "HR11", 10));
        }

        [TestMethod]
        public void TestMethod2()
        {
            UpraviteljTransakcijama ut = new UpraviteljTransakcijama();

            Assert.ThrowsException<InvalidAmountException>(() => ut.PrebaciSredstva("HR11", "HR11", -10));
        }

        [TestMethod]
        public void TestMethod3()
        {
            UpraviteljTransakcijama ut = new UpraviteljTransakcijama();

            var transakcija = ut.PrebaciSredstva("HR11", "HR22", 30000);

            Assert.IsTrue(
            
                transakcija.Izvor.Stanje == 70000 &&
                transakcija.Odrediste.Stanje == 80000
            );
        }

        [TestMethod]
        public void TestMethod4()
        {
            UpraviteljTransakcijama ut = new UpraviteljTransakcijama();

            var transakcija = ut.PrebaciSredstva("HR11", "HR22", 30000);

            Assert.IsTrue(

                transakcija.Naplaceno == 30000 &&
                transakcija.PreostaloNaplatiti == 0
            );
        }

        [TestMethod]
        public void TestMethod5()
        {
            UpraviteljTransakcijama ut = new UpraviteljTransakcijama();

            var transakcija = ut.PrebaciSredstva("HR66", "HR55", 3500);

            Assert.IsTrue(

                transakcija.Izvor.Stanje == 0 &&
                transakcija.Odrediste.Stanje == 10000
            );
        }

        [TestMethod]
        public void TestMethod6()
        {
            UpraviteljTransakcijama ut = new UpraviteljTransakcijama();

            var transakcija = ut.PrebaciSredstva("HR66", "HR55", 3500);

            Assert.IsTrue(

                transakcija.Naplaceno == 2000 &&
                transakcija.PreostaloNaplatiti == 1500
            );
        }

        [TestMethod]
        public void TestMethod7()
        {
            UpraviteljTransakcijama ut = new UpraviteljTransakcijama();

            Assert.ThrowsException<InvalidBankAccountException>(()=> ut.OdobriMinus("HR77", 1000));
        }

        [TestMethod]
        public void TestMethod8()
        {
            UpraviteljTransakcijama ut = new UpraviteljTransakcijama();
            ut.OdobriMinus("HR66", 2000);

            var transakcija = ut.PrebaciSredstva("HR66", "HR55", 3500);

            Assert.IsTrue(

                transakcija.Izvor.Stanje == -1500 &&
                transakcija.Odrediste.Stanje == 11500
            );
        }

        [TestMethod]
        public void TestMethod9()
        {
            UpraviteljTransakcijama ut = new UpraviteljTransakcijama();
            ut.OdobriMinus("HR66", 2000);

            var transakcija = ut.PrebaciSredstva("HR66", "HR55", 4500);

            Assert.IsTrue(

                transakcija.Izvor.Stanje == -2000 &&
                transakcija.Odrediste.Stanje == 12000 &&
                transakcija.Naplaceno == 4000 &&
                transakcija.PreostaloNaplatiti == 500
            );
        }
    }
}
