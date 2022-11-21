using Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTests.Models
{
    [TestClass]
    public class SkillsetTests
    {
        private Skill skill1 = new Skill()
        {
            Id = 0,
            Name = "HAYIAAA",
            Description = "AAAAAA",
            Cooldown = 8,
            Cost = 100,
            Power = 1,
            Recoil = 1
        };

        private Skill skill2 = new Skill()
        {
            Id = 0,
            Name = "HÚ",
            Description = "RÉ",
            Cooldown = 3,
            Cost = 10,
            Power = 1,
            Recoil = 1
        };
        
        [TestMethod]
        public void SkillAdds()
        {
            //Arrange
            var skillset = new Skillset(2);

            //Act
            skillset.Add(skill1);
            var result = skillset.Values.First();

            //Assert
            Assert.AreSame(skill1, result);
        }

        [TestMethod]
        public void DefaultSkillIsTheFirst()
        {
            //Arrange
            var skillset = new Skillset(2);

            //Act
            skillset.Add(skill1);
            skillset.Add(skill2);
            var result = skillset.InUse;

            //Assert
            Assert.AreSame(skill1, result);
        }

        [TestMethod]
        public void SkillChanges()
        {
            //Arrange
            var skillset = new Skillset(2);

            //Act
            skillset.Add(skill1);
            skillset.Add(skill2);
            skillset.SwitchTo(1);
            var result = skillset.InUse;

            //Assert
            Assert.AreSame(skill2, result);
        }
    }
}
