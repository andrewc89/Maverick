using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Moq;
using Maverick.Tests.Data;

namespace Maverick.Tests.Controller
{
    public class JsonTests
    {
        private Mock<Maverick.Repository.IRepository<Product>> MockProductRepo;
        private System.Web.Mvc.Controller ProductController;

        public JsonTests ()
        {
            var MockProductRepo = Data.ProductRepository.Construct();
            ProductController = new Maverick.Controllers.BaseController<Product>(MockProductRepo.Object);
        }

        [Fact]
        public void GetWithIDTest ()
        {
            
        }
    }
}