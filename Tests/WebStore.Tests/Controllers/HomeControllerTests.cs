using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebStore.Controllers;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Index();

            //Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ContactUs_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.ContactUs();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void PageNotFound_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.PageNotFound();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.BlogSingle();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Blog();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Cart_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Cart();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Login_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Login();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ProductDetails_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.ProductDetails();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Shop_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Shop();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Checkout_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Checkout();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void SecondAction_Returns_Content()
        {
            const string base_text = "Action with value id:";

            var controller = new HomeController();

            const string id = "TestId";

            var result = controller.SecondAction(id);

            var content = Assert.IsType<ContentResult>(result);

            const string expected_content = base_text + id;

            Assert.Equal(expected_content, content.Content);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Throw_thrown_ApplicationException_with_Test_error()
        {
            var controller = new HomeController();

            controller.Throw();
        }

        [TestMethod]
        public void Throw_thrown_ApplicationException_with_Test_error2()
        {
            const string expected_exception_message = "Test error!";

            var controller = new HomeController();

            Exception exception = null;

            try
            {
                controller.Throw();
            }
            catch (ApplicationException e)
            {

                exception = e;
            }

            var app_exception = Assert.IsType<ApplicationException>(exception);

            Assert.Equal(expected_exception_message, exception.Message);
        }

        [TestMethod]
        public void Throw_thrown_ApplicationException_with_Test_error3()
        {
            const string expected_exception_message = "Test error!";

            var controller = new HomeController();

            var exception = Assert.Throws<ApplicationException>(()=>controller.Throw());

            Assert.Equal(expected_exception_message, exception.Message);
        }

        [TestMethod]
        public void ErrorStatus_404_RedirectTo_Error404()
        {
            var controller = new HomeController();

            const string expected_action_name = nameof(HomeController.PageNotFound);

            const string error_status_code = "404";

            var result = controller.ErrorStatus(error_status_code);

            var redirect_to_action = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(expected_action_name, redirect_to_action.ActionName);

            Assert.Null(redirect_to_action.ControllerName);
        }
    }
}
