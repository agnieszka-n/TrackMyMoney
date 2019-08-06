using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Models;
using Moq;
using Services.Contracts;

namespace ViewModels.Tests
{
    [TestFixture]
    public class CostsListViewModelTest
    {
        [Test]
        public void Can_Add_Cost_Show_New_Cost_Panel()
        {
            // Arrange
            CostsListViewModel vm = GetSimpleObjectUnderTest();

            // Act
            vm.AddCostCommand.Execute(null);

            // Assert
            Assert.AreEqual(true, vm.IsAddingCost);
        }

        [Test]
        public void Can_Cancel_Hide_New_Cost_Panel()
        {
            // Arrange
            CostsListViewModel vm = GetSimpleObjectUnderTest();
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
            CostsListViewModel vm = GetSimpleObjectUnderTest();
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
            CostsListViewModel vm = GetSimpleObjectUnderTest();
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

        [Test]
        public void Can_Load_Categories()
        {
            // Arrange
            var sampleCategories = new List<CostCategory>()
            {
                new CostCategory() { Id = 1, Name = "111" },
                new CostCategory() { Id = 1, Name = "222" }
            };

            var mockManager = new Mock<ICategoriesManager>();
            mockManager.Setup(x => x.GetCategories()).Returns(new OperationResult<List<CostCategory>>(sampleCategories));
            var vm = new CostsListViewModel(mockManager.Object);

            // Act
            vm.AddCostCommand.Execute(null);

            // Assert
            Assert.IsNotNull(vm.Categories);
            Assert.AreEqual(2, vm.Categories.Count);
            Assert.AreEqual(1, vm.Categories[0].Id);
            Assert.AreEqual("111", vm.Categories[0].Name);
        }

        [Test]
        public void Can_Load_Categories_Only_Once()
        {
            // Arrange
            var mockManager = new Mock<ICategoriesManager>();
            mockManager.Setup(x => x.GetCategories()).Returns(new OperationResult<List<CostCategory>>(new List<CostCategory>()));
            var vm = new CostsListViewModel(mockManager.Object);

            // Act
            vm.AddCostCommand.Execute(null);
            vm.AddCostCommand.Execute(null);

            // Assert
            Assert.DoesNotThrow(() => mockManager.Verify(x => x.GetCategories(), Times.Once));
        }

        private CostsListViewModel GetSimpleObjectUnderTest()
        {
            var mockManager = new Mock<ICategoriesManager>();
            mockManager.Setup(x => x.GetCategories()).Returns(new OperationResult<List<CostCategory>>(new List<CostCategory>()));
            return new CostsListViewModel(mockManager.Object);
        }
    }
}
