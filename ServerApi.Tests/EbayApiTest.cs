using System;
using Xunit;

using System.Collections.Generic;
using System.Linq;
using System.Net;

using ServerApi.Services;
using ServerApi.Models;

namespace ServerApi.Tests
{
    public class EbayApiTest
    {

        EbayApi _ebay = new EbayApi();

        [Fact]
        public void productSearchTest()
        {
            // Arrange
            string searchTerm = "microwave";
            int entries = 2;

            // Act
            IList<Product> products = _ebay.getProductsByName(searchTerm, entries);
            Product _p = products.First();

            // Assert
            Assert.Equal(expected: entries, actual: products.Count);
            Assert.Contains("microwave", _p.name.ToLower());
            Assert.Equal("56.99 USD", _p.price);

        }

        [Fact]
        public void recommendationsTest()
        {
            // Arrange
            Dictionary<string, string> _f = new Dictionary<string, string>();
            _f.Add("color", "black");
            _f.Add("brand", "samsung");
            _f.Add("size", "42\"");

            // Act
            var products = _ebay.getProductsForRecommendation(_f);
            Product _p = products.First();

            // Assert
            Assert.Equal(expected: 3, actual: products.Count);
            Assert.Contains("samsung", _p.name.ToLower());
            Assert.Contains("42\"", _p.name.ToLower());
        }
    }
}
