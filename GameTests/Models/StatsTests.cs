using Game.Models;

namespace GameTests.Models
{
    [TestClass]
    public class StatsTests
    {
        private GameConfig config;

        [TestInitialize]
        public void Init()
        {
            config = new GameConfig()
            {
                DamageThreshold = 256,
                Stamina = 256
            };
        }

        [TestMethod]
        public void CheckHealth()
        {
            //Arrange
            int expectedHP = 128;
            int damage = 128;

            //Act
            Stats st = new Stats(config);
            st.Damage += damage;
           
            //Assert
            int currenthp = st.Health;
            Assert.AreEqual(expectedHP, currenthp);
        }

        [TestMethod]
        public void CheckStamina()
        {
            //Arrange
            int expectedSTA = 128;
            int fatigue = 128;

            //Act
            Stats st = new Stats(config);
            st.Fatigue += fatigue;

            //Assert
            int currentsta = st.CurrentStamina;
            Assert.AreEqual(expectedSTA, currentsta);
        }
    }
}