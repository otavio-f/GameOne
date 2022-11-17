using Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTests.Models
{
    [TestClass]
    public class AttributesTests
    {

        [TestMethod]
        public void SumsCorrectly()
        {
            //Arrange
            var attributes = new Attributes()
            {
                Strength = 255.0,
                Sensitivity = 1.0,
                Dexterity = 1.0,
                Effort = 1.0,
                RecoverFactor = 1.0,
                HealFactor = 1.0,
                TakeoverTendency = 0.0
            };

            var modifier = new Attributes()
            {
                Strength = 245.0,
                Sensitivity = 90.0,
                Dexterity = 45.0,
                Effort = 31.1,
                RecoverFactor = 12.3,
                HealFactor = -0.01,
                TakeoverTendency = 0.01
            };
            var expectedStr = 500.0;
            var expectedSst = 91.0;
            var expectedDex = 46.0;
            var expectedEff = 32.1;
            var expectedReF = 13.3;
            var expectedHeF = 0.99;
            var expectedToT = 0.01;

            //Act
            var result = attributes + modifier;

            //Assert
            Assert.AreEqual(expectedStr, result.Strength, 0.001);
            Assert.AreEqual(expectedSst, result.Sensitivity, 0.001);
            Assert.AreEqual(expectedDex, result.Dexterity, 0.001);
            Assert.AreEqual(expectedEff, result.Effort, 0.001);
            Assert.AreEqual(expectedReF, result.RecoverFactor, 0.001);
            Assert.AreEqual(expectedHeF, result.HealFactor, 0.001);
            Assert.AreEqual(expectedToT, result.TakeoverTendency, 0.001);
        }


        [TestMethod]
        public void MultipliesCorrectly()
        {
            //Arrange
            var attributes = new Attributes()
            {
                Strength = 255.0,
                Sensitivity = 1.0,
                Dexterity = 1.5,
                Effort = 2.0,
                RecoverFactor = 1.11,
                HealFactor = 0.001,
                TakeoverTendency = 0.25
            };

            var modifier = new Attributes()
            {
                Strength = 1.0,
                Sensitivity = 0.999,
                Dexterity = 1.01,
                Effort = 2.0,
                RecoverFactor = 3.0,
                HealFactor = 1.1,
                TakeoverTendency = 0.99
            };

            var expectedStr = 255.0;
            var expectedSst = 0.999;
            var expectedDex = 1.515;
            var expectedEff = 4.0;
            var expectedReF = 3.33;
            var expectedHeF = 0.0011;
            var expectedToT = 0.2475;

            //Act
            var result = attributes * modifier;

            //Assert
            Assert.AreEqual(expectedStr, result.Strength, 0.00001);
            Assert.AreEqual(expectedSst, result.Sensitivity, 0.00001);
            Assert.AreEqual(expectedDex, result.Dexterity, 0.00001);
            Assert.AreEqual(expectedEff, result.Effort, 0.00001);
            Assert.AreEqual(expectedReF, result.RecoverFactor, 0.0001);
            Assert.AreEqual(expectedHeF, result.HealFactor, 0.0001);
            Assert.AreEqual(expectedToT, result.TakeoverTendency, 0.0001);
        }

        [TestMethod]
        public void SumsWithNeutralCorrectly()
        {
            //Arrange
            var attributes = new Attributes()
            {
                Strength = 255.0,
                Sensitivity = 1.0,
                Dexterity = 1.0,
                Effort = 1.0,
                RecoverFactor = 1.0,
                HealFactor = 1.0,
                TakeoverTendency = 0.0
            };

            var neutral = new Attributes()
            {
                Strength = 0,
                Sensitivity = 0,
                Dexterity = 0,
                Effort = 0,
                RecoverFactor = 0,
                HealFactor = 0,
                TakeoverTendency = 0.0
            };

            //Act
            var result = attributes + neutral;

            //Assert
            Assert.AreEqual(attributes.Strength, result.Strength);
            Assert.AreEqual(attributes.Sensitivity, result.Sensitivity);
            Assert.AreEqual(attributes.Dexterity, result.Dexterity);
            Assert.AreEqual(attributes.Effort, result.Effort);
            Assert.AreEqual(attributes.RecoverFactor, result.RecoverFactor);
            Assert.AreEqual(attributes.HealFactor, result.HealFactor);
            Assert.AreEqual(attributes.TakeoverTendency, result.TakeoverTendency);
        }


        [TestMethod]
        public void ProductWithNeutralCorrectly()
        {
            //Arrange
            var attributes = new Attributes()
            {
                Strength = 255.0,
                Sensitivity = 1.0,
                Dexterity = 1.0,
                Effort = 1.0,
                RecoverFactor = 1.0,
                HealFactor = 1.0,
                TakeoverTendency = 0.0
            };

            var modifier = new Attributes()
            {
                Strength = 1.0,
                Sensitivity = 1.0,
                Dexterity = 1.0,
                Effort = 1.0,
                RecoverFactor = 1.0,
                HealFactor = 1.0,
                TakeoverTendency = 1.0
            };

            //Act
            var result = attributes * modifier;

            //Assert
            Assert.AreEqual(attributes.Strength, result.Strength);
            Assert.AreEqual(attributes.Sensitivity, result.Sensitivity);
            Assert.AreEqual(attributes.Dexterity, result.Dexterity);
            Assert.AreEqual(attributes.Effort, result.Effort);
            Assert.AreEqual(attributes.RecoverFactor, result.RecoverFactor);
            Assert.AreEqual(attributes.HealFactor, result.HealFactor);
            Assert.AreEqual(attributes.TakeoverTendency, result.TakeoverTendency);
        }


        [TestMethod]
        public void SumsWithItselfCorrectly()
        {
            //Arrange
            var attributes = new Attributes()
            {
                Strength = 255.0,
                Sensitivity = 1.0,
                Dexterity = 1.0,
                Effort = 1.0,
                RecoverFactor = 1.0,
                HealFactor = 1.0,
                TakeoverTendency = 0.0
            };

            var expectedStr = 510.0;
            var expectedSst = 2.0;
            var expectedDex = 2.0;
            var expectedEff = 2.0;
            var expectedReF = 2.0;
            var expectedHeF = 2.0;
            var expectedToT = 0.0;

            //Act
            var result = attributes + attributes;

            //Assert
            Assert.AreEqual(expectedStr, result.Strength);
            Assert.AreEqual(expectedSst, result.Sensitivity);
            Assert.AreEqual(expectedDex, result.Dexterity);
            Assert.AreEqual(expectedEff, result.Effort);
            Assert.AreEqual(expectedReF, result.RecoverFactor);
            Assert.AreEqual(expectedHeF, result.HealFactor);
            Assert.AreEqual(expectedToT, result.TakeoverTendency);
        }


        [TestMethod]
        public void MultipliesWithItselfCorrectly()
        {
            //Arrange
            var attributes = new Attributes()
            {
                Strength = 255.0,
                Sensitivity = 1.0,
                Dexterity = 1.0,
                Effort = 1.0,
                RecoverFactor = 1.0,
                HealFactor = 1.0,
                TakeoverTendency = 0.0
            };

            var expectedStr = 65025;
            var expectedSst = 1.0;
            var expectedDex = 1.0;
            var expectedEff = 1.0;
            var expectedReF = 1.0;
            var expectedHeF = 1.0;
            var expectedToT = 0.0;

            //Act
            var result = attributes * attributes;

            //Assert
            Assert.AreEqual(expectedStr, result.Strength);
            Assert.AreEqual(expectedSst, result.Sensitivity);
            Assert.AreEqual(expectedDex, result.Dexterity);
            Assert.AreEqual(expectedEff, result.Effort);
            Assert.AreEqual(expectedReF, result.RecoverFactor);
            Assert.AreEqual(expectedHeF, result.HealFactor);
            Assert.AreEqual(expectedToT, result.TakeoverTendency);
        }
    }

}
