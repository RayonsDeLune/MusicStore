using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicShop.Controllers;
using System.Threading.Tasks;
using Moq;
using System.Web.Mvc;
using MusicShop.Models;

namespace UnitTestMusicShop
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            AccountController ControlTest = new AccountController();
            string result = null;
            //Task.Run(async () => result = await ControlTest.Authenticate("admin", "password")).Wait();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            // le mockup permet de simuler une session
            //var homeControllerMock = new Mock<ControllerContext>();
            //homeControllerMock.Setup(foo => foo.HttpContext.Session["tocken"]).Returns("value of tocken");
            AccountController ControlTest = new AccountController();
            //{
            //    ControllerContext = homeControllerMock.Object
            //};
            
            string result = null;
            Task.Run(() => result = ControlTest.Get(1)).Wait(); // remplacer Get par une fonction du AccountController
            Assert.AreEqual("vous etes connecté",result);
        }

        // tester la méthode de connection du accountcontroller
        [TestMethod]
        public void TestLogIn()
        {
            AccountController ControlTest = new AccountController();
            ActionResult result = null;
            Task.Run(() => result = ControlTest.LogIn(new User("test@test.fr", "test"))).Wait();
            Assert.IsNotNull(result);
        }
    }
}
