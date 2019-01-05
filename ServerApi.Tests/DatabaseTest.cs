using System;
using Xunit;

using System.Collections.Generic;
using System.Linq;
using System.Net;

using ServerApi.Database;

namespace ServerApi.Tests
{
    public class DatabaseTest
    {
        Connection _c = new Connection();

        [Fact]
        public void userRegistration()
        {
            // Act
            int userId = _c.registerUser("davidalvarado320@gmail.com");

            // Assert
            Assert.Equal(6, userId);
        }

        [Fact]
        public void getUserDetailForRecommendationsTest()
        {
            // Arrange
            int userId = _c.registerUser("davidalvarado320@gmail.com");

            // Act
            Dictionary<string, string> _rec = _c.selectUserRecommendations(userId);

            // Assert
            Assert.Equal("black", _rec.GetValueOrDefault("color"));
            Assert.Equal("vizio", _rec.GetValueOrDefault("brand"));
            Assert.Equal("None", _rec.GetValueOrDefault("size"));
        }
    }
}