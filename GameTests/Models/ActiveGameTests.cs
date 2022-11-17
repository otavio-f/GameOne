using Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTests.Models
{
    [TestClass]
    public class ActiveGameTests
    {

        private Character p1;
        private Character p2;

        [TestInitialize]
        public void Init()
        {
            var config = new GameConfig()
            {
                DamageThreshold = 65535,
                Stamina = 65535
            };

            p1 = new Character()
            {
                Stats = new Stats(config),
                Attributes = new Attributes()
                {
                    Strength = 1.5,
                    Sensitivity = 1.5,
                    Dexterity = 1.5,
                    Effort = 1.23,
                    RecoverFactor = 0.078,
                    HealFactor = 0.019,
                    TakeoverTendency = 0.033
                },
                Item = new Item()
                {
                    Attributes = new Attributes()
                    {
                        Strength = 0.0,
                        Sensitivity = -0.155,
                        Dexterity = 0.02,
                        Effort = -0.45,
                        RecoverFactor = 0.0,
                        HealFactor = 0.0,
                        TakeoverTendency = 0.01
                    }
                },
                Hability = new Hability()
                {
                    Cooldown = 5,
                    Cost = 256,
                    Power = 1024,
                    Recoil = 64
                }
            };

            p2 = new Character()
            {
                Stats = new Stats(config),
                Attributes = new Attributes()
                {
                    Strength = 0.7777,
                    Sensitivity = 0.85,
                    Dexterity = 0.8,
                    Effort = 0.75,
                    RecoverFactor = 0.025,
                    HealFactor = 0.025,
                    TakeoverTendency = 0.25
                },
                Item = new Item()
                {
                    Attributes = new Attributes()
                    {
                        Strength = 0.01,
                        Sensitivity = -0.01,
                        Dexterity = 0.01,
                        Effort = -0.05,
                        RecoverFactor = 0.0,
                        HealFactor = 0.0,
                        TakeoverTendency = 0.05
                    }
                },
                Hability = new Hability()
                {
                    Cooldown = 2,
                    Cost = 256,
                    Power = 128,
                    Recoil = 128
                }
            };
        }

        [TestMethod]
        public void TurnCounts()
        {
            //Arrange
            var game = new ActiveGame(p1, p2);

            //Act
            game.Round();
            game.Round();
            game.Round();
            var expectedTurnNumber = 3;

            //Assert
            Assert.AreEqual(expectedTurnNumber, game.TurnCount);
        }

        [TestMethod]
        public void SwitchWorks()
        {
            //Arrange
            var game = new ActiveGame(p1, p2); //p1 is top, p2 is bottom

            //Act
            var oldTop = game.Top;
            var oldBottom = game.Bottom;
            game.Switch();
            var newTop = game.Top;
            var newBottom = game.Bottom;


            //Assert
            Assert.AreSame(oldTop, newBottom);
            Assert.AreSame(oldBottom, newTop);
        }

        [TestMethod]
        public void RoundDoesNothingIfTopHabilityIsNotReady()
        {
            //Arrange
            var game = new ActiveGame(p1, p2); //p1 is top, p2 is bottom

            //Act
            game.Round();
            var expectedTopDamage = 0;
            var expectedTopFatigue = 0;
            var expectedBottomDamage = 0;
            var expectedBottomFatigue = 0;

            var currentTopDamage = game.Top.Stats.Damage;
            var currentTopFatigue = game.Top.Stats.Fatigue;
            var currentBottomDamage = game.Bottom.Stats.Damage;
            var currentBottomFatigue = game.Bottom.Stats.Fatigue;


            //Assert
            Assert.AreEqual(expectedTopDamage, currentTopDamage);
            Assert.AreEqual(expectedTopFatigue, currentTopFatigue);
            Assert.AreEqual(expectedBottomDamage, currentBottomDamage);
            Assert.AreEqual(expectedBottomFatigue, currentBottomFatigue);
        }

        [TestMethod]
        public void HabilityCountIncreasesIfHabilityIsNotReady()
        {
            //Arrange
            var game = new ActiveGame(p1, p2); //p1 is top, p2 is bottom
            game.Top.Hability.Cooldown = int.MaxValue; //doesn't reach

            //Act
            game.Round();
            var expectedHabilityCount = 1;
            var currentHabilityCount = game.Top.Hability.Count;

            //Assert
            Assert.AreEqual(expectedHabilityCount, currentHabilityCount);
        }

        [TestMethod]
        public void RoundUsesTopHabilityCorrectly()
        {
            //Arrange
            var game = new ActiveGame(p1, p2);
            game.Top.Hability.Cooldown = 1;

            //Act
            game.Round();
            var expectedTopDamage = 86;
            var expectedTopFatigue = 200;
            var expectedBottomDamage = 1307;
            var expectedBottomFatigue = 0;

            var currentTopDamage = game.Top.Stats.Damage;
            var currentTopFatigue = game.Top.Stats.Fatigue;
            var currentBottomDamage = game.Bottom.Stats.Damage;
            var currentBottomFatigue = game.Bottom.Stats.Fatigue;


            //Assert
            Assert.AreEqual(expectedTopDamage, currentTopDamage);
            Assert.AreEqual(expectedTopFatigue, currentTopFatigue);
            Assert.AreEqual(expectedBottomDamage, currentBottomDamage);
            Assert.AreEqual(expectedBottomFatigue, currentBottomFatigue);
        }

        [TestMethod]
        public void ResetsCountIfHabilityWasUsed()
        {
            //Arrange
            var game = new ActiveGame(p1, p2); //p1 is top, p2 is bottom
            game.Top.Hability.Cooldown = 3; //reaches soon

            //Act
            game.Round();
            game.Round();
            game.Round();
            game.Round();
            var expectedHabilityCount = 1;
            var currentHabilityCount = game.Top.Hability.Count;

            //Assert
            Assert.AreEqual(expectedHabilityCount, currentHabilityCount);
        }

        [TestMethod]
        public void RoundDoesNothingIfTopCannotUseHability()
        {
            //Arrange
            var game = new ActiveGame(p1, p2); //p1 is top, p2 is bottom
            game.Top.Stats.Fatigue = int.MaxValue;
            game.Top.Hability.Cooldown = 0;

            //Act
            game.Round();
            var expectedTopDamage = 0;
            var expectedTopFatigue = int.MaxValue;
            var expectedBottomDamage = 0;
            var expectedBottomFatigue = 0;

            var currentTopDamage = game.Top.Stats.Damage;
            var currentTopFatigue = game.Top.Stats.Fatigue;
            var currentBottomDamage = game.Bottom.Stats.Damage;
            var currentBottomFatigue = game.Bottom.Stats.Fatigue;


            //Assert
            Assert.AreEqual(expectedTopDamage, currentTopDamage);
            Assert.AreEqual(expectedTopFatigue, currentTopFatigue);
            Assert.AreEqual(expectedBottomDamage, currentBottomDamage);
            Assert.AreEqual(expectedBottomFatigue, currentBottomFatigue);
        }

        [TestMethod]
        public void HabilityCountDoesNotIncreaseIfTopCannotUseHability()
        {
            //Arrange
            var game = new ActiveGame(p1, p2); //p1 is top, p2 is bottom
            game.Top.Stats.Fatigue = int.MaxValue;

            //Act
            game.Round();
            var expectedHabilityCount = 0;
            var currentHabilityCount = game.Top.Hability.Count;

            //Assert
            Assert.AreEqual(expectedHabilityCount, currentHabilityCount);
        }


        [TestMethod]
        public void DecidesOneWinnerCorrectly()
        {
            //Arrange
            var game = new ActiveGame(p1, p2); //p1 is top, p2 is bottom
            // setup: one touch kill, the flash-maximum damage
            game.Top.Attributes.Dexterity = 1_000_000.0;
            game.Top.Hability.Cooldown = 0;

            //Act
            game.Round();
            game.Round();
            var expectedWinner = game.Top;
            var (gameEnded, currentWinner) = game.CheckForWinner();

            //Assert
            Assert.IsTrue(gameEnded);
            Assert.AreSame(expectedWinner, currentWinner);
        }

        [TestMethod]
        public void DecidesBothLoseCorrectly()
        {
            // Arrange
            var game = new ActiveGame(p1, p2);
            // setup: glass cannon, the flash, maximum damage while receiving maximum recoil
            game.Top.Attributes.Dexterity = 1_000_000.0;
            game.Top.Attributes.Sensitivity = 1_000_000.0;
            game.Top.Hability.Cooldown = 0; 
            
            //Act
            game.Round();
            game.Round();
            var (gameEnded, currentWinner) = game.CheckForWinner();

            //Assert
            Assert.IsTrue(gameEnded);
            Assert.IsNull(currentWinner);
        }


        [TestMethod]
        public void DecidesNoOneWonYetCorrectly()
        {
            //Arrange
            var game = new ActiveGame(p1, p2); //p1 is top, p2 is bottom

            //Act
            game.Round();
            game.Round();
            game.Round();
            game.Round();
            game.Round();
            game.Round();
            var (gameEnded, currentWinner) = game.CheckForWinner();

            //Assert
            Assert.IsFalse(gameEnded);
            Assert.IsNull(currentWinner);
        }
    }
}
