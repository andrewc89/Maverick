using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;

namespace Maverick.Tests.Data
{
    static class ProductRepository
    {
        public static Mock<Maverick.Repository.IRepository<Product>> Construct ()
        {
            var MockProductRepository = new Mock<Maverick.Repository.IRepository<Product>>();
            var Products = Product.GetProducts();
            MockProductRepository.Setup(x => x.All()).Returns(Products);
            MockProductRepository.Setup(x => x.Get(It.IsAny<long>())).Returns((long ID) => Products.Single(x => x.ID == ID));
            MockProductRepository.Setup(x => x.Add(It.IsAny<Product>())).Returns(
                (Product Entity) =>
                {
                    Entity.ID = Products.Count() + 1;
                    Products.Add(Entity);
                    return true;
                });
            MockProductRepository.Setup(x => x.Update(It.IsAny<Product>())).Returns(
                (Product Entity) =>
                {
                    var Original = Products.Single(x => x.ID == Entity.ID);
                    if (Original == null) return false;
                    if (!string.IsNullOrEmpty(Entity.DisplayName)) Original.DisplayName = Entity.DisplayName;
                    if (Entity.Price != 0.0) Original.Price = Entity.Price;
                    return true;
                });
            MockProductRepository.Setup(x => x.Delete(It.IsAny<long>())).Returns(
                (long ID) =>
                {
                    if (Products.Single(x => x.ID == ID) != null)
                    {
                        var ToRemove = Products.Single(x => x.ID == ID);
                        Products.Remove(ToRemove);
                        return true;
                    }
                    return false;
                });
            return MockProductRepository;
        }
    }
}