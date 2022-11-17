using Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTests.Models
{
    [TestClass]
    public class CharacterTests
    {

        private GameConfig config;
        private Attributes attributes;
        private Item item;
        private Hability hability;

        [TestInitialize]
        public void Init()
        {
            config = new GameConfig()
            {
                DamageThreshold = 65535,
                Stamina = 65535
            };

            attributes = new Attributes()
            {
                Strength = 0.98765,
                Sensitivity = 1,
                Dexterity = 1,
                Effort = 1,
                RecoverFactor = 0.01,
                HealFactor = 0.01,
                TakeoverTendency = 0.5
            };

            hability = new Hability()
            {
                Cooldown = 2,
                Cost = 256,
                Power = 128,
                Recoil = 64
            };

            var itemAttributes = new Attributes()
            {
                Strength = 0.01235,
                Sensitivity = -0.11,
                Dexterity = 0.01,
                Effort = 0.25,
                RecoverFactor = 0.0,
                HealFactor = 0.0,
                TakeoverTendency = 0.05
            };

            item = new Item()
            {
                Attributes = itemAttributes
            };
        }

        [TestMethod]
        public void AttributesIncludesItemAttributes()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            var expectedSTR = 1.0;
            var expectedSST = 0.89;
            var expectedDEX = 1.01;
            var expectedEFF = 1.25;
            var expectedReF = 0.01;
            var expectedHeF = 0.01;
            var expectedToT = 0.55;
            var attrs = character.AttributesAfterItemUse;

            //Assert
            Assert.AreEqual(expectedSTR, attrs.Strength, 0.0001);
            Assert.AreEqual(expectedSST, attrs.Sensitivity, 0.0001);
            Assert.AreEqual(expectedDEX, attrs.Dexterity, 0.0001);
            Assert.AreEqual(expectedEFF, attrs.Effort, 0.0001);
            Assert.AreEqual(expectedReF, attrs.RecoverFactor, 0.0001);
            Assert.AreEqual(expectedHeF, attrs.HealFactor, 0.0001);
            Assert.AreEqual(expectedToT, attrs.TakeoverTendency, 0.0001);
        }

        [TestMethod]
        public void CanUseHabilityIfFatigueIsZero()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            
            //Assert
            Assert.IsTrue(character.CanUseHability);
        }

        [TestMethod]
        public void CanNotUseHabilityIfStaminaIsZero()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            character.Stats.Fatigue = 65535;

            //Assert
            Assert.IsFalse(character.CanUseHability);
        }

        [TestMethod]
        public void CanUseHabilityIfHasJustEnoughStamina()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            character.Stats.Fatigue = 65215;

            //Assert
            Assert.IsTrue(character.CanUseHability);
        }

        [TestMethod]
        public void CanNotUseHabilityIfHasNotEnoughStamina()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            character.Stats.Fatigue = 65216;

            //Assert
            Assert.IsFalse(character.CanUseHability);
        }

        [TestMethod]
        public void HealWorksAsExpected()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };
            character.Stats.Damage = 4096;

            //Act
            character.Heal();
            var expectedDamage = 3441;
            var currentDamage = character.Stats.Damage;

            //Assert
            Assert.AreEqual(expectedDamage, currentDamage);
        }

        [TestMethod]
        public void RecoverWorksAsExpected()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };
            character.Stats.Fatigue = 4096;

            //Act
            character.Recover();
            var expectedFatigue = 3441;
            var currentFatigue = character.Stats.Fatigue;

            //Assert
            Assert.AreEqual(expectedFatigue, currentFatigue);
        }

        [TestMethod]
        public void HealLowerLimitIsZero()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };
            character.Stats.Damage = 1;

            //Act
            character.Heal();
            var expectedDamage = 0;
            var currentDamage = character.Stats.Damage;

            //Assert
            Assert.AreEqual(expectedDamage, currentDamage);
        }

        [TestMethod]
        public void RecoverLowerLimitIsZero()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };
            character.Stats.Fatigue = 1;

            //Act
            character.Recover();
            var expectedFatigue = 0;
            var currentFatigue = character.Stats.Fatigue;

            //Assert
            Assert.AreEqual(expectedFatigue, currentFatigue);
        }

        [TestMethod]
        public void HabilityDamageIsCorrect()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            var expectedDamage = 129.28;

            //Assert
            Assert.AreEqual(expectedDamage, character.HabilityDamage, 0.0001);
        }


        [TestMethod]
        public void ReceivedDamageIsCorrect()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            character.ReceiveDamage(2560);
            var expectedDamage = 2278;
            var currentDamage = character.Stats.Damage;

            //Assert
            Assert.AreEqual(expectedDamage, currentDamage, 0.01);
            Assert.IsFalse(character.HasLost);
        }

        [TestMethod]
        public void DoesNotLoseWhenReceivingLowDamage()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            character.ReceiveDamage(1);
            var result = character.HasLost;

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DoesLoseOnlyWhenReceivingDamage()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            character.Stats.Damage = 1_000_000;
            var resultBefore = character.HasLost;
            character.ReceiveDamage(0);
            var resultAfter = character.HasLost;

            //Assert
            Assert.IsFalse(resultBefore);
            Assert.IsTrue(resultAfter);
        }

        [TestMethod]
        public void DoesLoseWhenReceivingEnoughDamage()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            var damage = 65535 / .89;
            character.ReceiveDamage(damage);
            var result = character.HasLost;

            //Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, character.Stats.Health);
        }

        [TestMethod]
        public void DoesLoseWhenReceivingHighDamage()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            character.ReceiveDamage(1_000_000);
            var result = character.HasLost;

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DoesNotUnloseWhenReceivingNegativeDamage()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            character.ReceiveDamage(1_000_000);
            character.ReceiveDamage(-1_000_000);
            var result = character.HasLost;

            //Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void AppliesRecoilCorrectly()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            character.ApplyHabilityRecoil();
            var expectedDamage = 57;
            var currentDamage = character.Stats.Damage;

            //Assert
            Assert.AreEqual(expectedDamage, currentDamage);
        }

        [TestMethod]
        public void DoesLoseOnlyAfterRecoil()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            character.Stats.Damage = 1_000_000;
            var resultBefore = character.HasLost;
            character.ApplyHabilityRecoil();
            var resultAfter = character.HasLost;

            //Assert
            Assert.IsFalse(resultBefore);
            Assert.IsTrue(resultAfter);
        }

        [TestMethod]
        public void AppliesHabilityCostCorrectly()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            //Act
            character.ApplyHabilityCost();
            var expectedFatigue = 320;
            var fatigue = character.Stats.Fatigue;

            //Assert
            Assert.AreEqual(expectedFatigue, fatigue);
        }


        [TestMethod]
        public void CalculatesTurnOverChanceCorrectly()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };

            var opponent = new Character()
            {
                Stats = new Stats(config),
                Attributes = new Attributes()
                {
                    Strength = 1.1,
                    Sensitivity = 1,
                    Dexterity = 1,
                    Effort = 1,
                    RecoverFactor = 0.01,
                    HealFactor = 0.01,
                    TakeoverTendency = 0.5
                },
                Item = new Item()
                {
                    Attributes = new Attributes()
                    {
                        Strength = 0.01,
                        Sensitivity = -0.11,
                        Dexterity = 0.01,
                        Effort = 0.25,
                        RecoverFactor = 0.0,
                        HealFactor = 0.0,
                        TakeoverTendency = 0.05
                    }
                }
            };

            //Act
            opponent.Stats.Fatigue = 128;
            var expectedOpponentToCharacter = 1.107831; //1.11/1.0 * 65407/65535
            var expectedCharacterToOpponent = 0.902663; //1/1.11 * 65535/65407
            var opponentToCharacter = opponent.TurnOverChance(character);
            var characterToOpponent = character.TurnOverChance(opponent);

            //Assert
            Assert.AreEqual(expectedOpponentToCharacter, opponentToCharacter, 0.000001);
            Assert.AreEqual(expectedCharacterToOpponent, characterToOpponent, 0.000001);
        }

        [TestMethod]
        public void CalculatesTurnOverChanceCorrectlyWhenEverythingIsZeroed()
        {
            //Arrange
            var character = new Character()
            {
                Stats = new Stats(config),
                Attributes = attributes,
                Hability = hability,
                Item = item
            };
            character.Stats.Damage = 65535;
            character.Attributes.Strength = 0.0;

            var opponent = new Character()
            {
                Stats = new Stats(config),
                Attributes = new Attributes()
                {
                    Strength = 1,
                    Sensitivity = 1,
                    Dexterity = 1,
                    Effort = 1,
                    RecoverFactor = 0.01,
                    HealFactor = 0.01,
                    TakeoverTendency = 0.5
                },
                Item = new Item()
                {
                    Attributes = new Attributes()
                    {
                        Strength = -0.01,
                        Sensitivity = -0.11,
                        Dexterity = 0.01,
                        Effort = 0.25,
                        RecoverFactor = 0.0,
                        HealFactor = 0.0,
                        TakeoverTendency = 0.05
                    }
                }
            };

            //Act
            opponent.Stats.Fatigue = config.Stamina;
            character.Stats.Fatigue = config.Stamina; //zero the current stamina
            character.Attributes.Strength = 0;
            opponent.Attributes.Strength = 0;
            character.Item.Attributes.Strength = 0;
            opponent.Item.Attributes.Strength = 0; //zero the strength
            var expectedOpponentToCharacter = 1;
            var expectedCharacterToOpponent = 1;
            var opponentToCharacter = opponent.TurnOverChance(character);
            var characterToOpponent = character.TurnOverChance(opponent);

            //Assert
            Assert.AreEqual(0, character.AttributesAfterItemUse.Strength);
            Assert.AreEqual(expectedOpponentToCharacter, opponentToCharacter, 0.000001);
            Assert.AreEqual(expectedCharacterToOpponent, characterToOpponent, 0.000001);
        }
    }
}
