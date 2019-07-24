using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Tests
{
    [TestFixture]
    public class CostsListViewModelTest
    {
        private CostsListViewModel vm;

        [SetUp]
        public void ResetObjectUnderTest()
        {
            vm = new CostsListViewModel();
        }

        [Test]
        public void Can_Add_Cost_Show_New_Cost_Panel()
        {
            // Act
            vm.AddCostCommand.Execute(null);

            // Assert
            Assert.AreEqual(true, vm.IsAddingCost);
        }

        [Test]
        public void Can_Cancel_Hide_New_Cost_Panel()
        {
            // Arrange
            vm.AddCostCommand.Execute(null);

            // Act
            vm.CancelAddingCommand.Execute(null);

            // Assert
            Assert.AreEqual(false, vm.IsAddingCost);
        }

        [Test]
        public void Can_Cancelling_Clear_New_Cost()
        {
            // Arrange
            vm.AddCostCommand.Execute(null);
            vm.NewCost.Amount = 123;

            // Act
            vm.CancelAddingCommand.Execute(null);
            vm.AddCostCommand.Execute(null);

            // Assert
            Assert.IsNull(vm.NewCost.Amount);
        }

        [Test]
        public void Can_Save_Cost()
        {
            // Arrange
            var costsCountBefore = vm.Costs.Count;

            vm.AddCostCommand.Execute(null);

            vm.NewCost.Date = new DateTime(2000, 1, 1);
            vm.NewCost.Category = "category";
            vm.NewCost.Subject = "subject";
            vm.NewCost.Amount = 123;

            // Act
            vm.SaveCostCommand.Execute(null);

            // Assert
            Assert.AreEqual(costsCountBefore + 1, vm.Costs.Count);

            var lastCost = vm.Costs.Last();

            Assert.AreEqual("category", lastCost.Category);
            Assert.AreEqual("subject", lastCost.Subject);
            Assert.AreEqual(new DateTime(2000, 1, 1), lastCost.Date);
            Assert.AreEqual(123, lastCost.Amount);
        }
    }
}
