using Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTests.Models
{
    [TestClass]
    public class HabilityTests
    {

        [TestMethod]
        public void CountResetsAfterReset()
        {
            //Arrange
            var hability = new Hability()
            {
                Cooldown = 2
            };
            var expectedCount = 0;

            //Act
            hability.IncreaseCount();
            hability.ResetCount();

            //Assert
            Assert.AreEqual(expectedCount, hability.Count);
        }

        [TestMethod]
        public void IsReadyCorrectlyTriggers()
        {
            //Arrange
            var hability = new Hability()
            {
                Cooldown = 2
            };

            //Act
            hability.IncreaseCount();
            hability.IncreaseCount();

            //Assert
            Assert.IsTrue(hability.IsReady);
        }


        [TestMethod]
        public void IsNotReadyCorrectlyTriggersOnIncomplete()
        {
            //Arrange
            var hability = new Hability()
            {
                Cooldown = 3
            };

            //Act
            hability.IncreaseCount();
            hability.IncreaseCount();

            //Assert
            Assert.IsFalse(hability.IsReady);
        }

        [TestMethod]
        public void IsReadyCorrectlyTriggersOnExcess()
        {
            //Arrange
            var hability = new Hability()
            {
                Cooldown = 2
            };

            //Act
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();

            //Assert
            Assert.IsTrue(hability.IsReady);
        }


        [TestMethod]
        public void IsNotReadyOnReset()
        {
            //Arrange
            var hability = new Hability()
            {
                Cooldown = 2
            };

            //Act
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.IncreaseCount();
            hability.ResetCount();

            //Assert
            Assert.IsFalse(hability.IsReady);
        }
    }
}
