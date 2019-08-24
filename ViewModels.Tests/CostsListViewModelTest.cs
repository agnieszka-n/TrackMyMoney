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
using ViewModels.Contracts;

namespace ViewModels.Tests
{
    [TestFixture]
    public class CostsListViewModelTest
    {
        [Test]
        public void Can_Add_Cost_Show_New_Cost_Panel()
        {
            // Arrange
            CostsListViewModel vm = GetCostsListViewModelWithoutDatabaseReadings();

            // Act
            vm.ShowAddCostCommand.Execute(null);

            // Assert
            Assert.AreEqual(true, vm.IsAddingCost);
        }

        [Test]
        public void Can_Cancel_Hide_New_Cost_Panel()
        {
            // Arrange
            CostsListViewModel vm = GetCostsListViewModelWithoutDatabaseReadings();
            vm.ShowAddCostCommand.Execute(null);

            // Act
            vm.CancelAddingCommand.Execute(null);

            // Assert
            Assert.AreEqual(false, vm.IsAddingCost);
        }

        [Test]
        public void Can_Cancelling_Clear_New_Cost()
        {
            // Arrange
            CostsListViewModel vm = GetCostsListViewModelWithoutDatabaseReadings();
            vm.ShowAddCostCommand.Execute(null);
            vm.NewCost.Amount = 123;

            // Act
            vm.CancelAddingCommand.Execute(null);
            vm.ShowAddCostCommand.Execute(null);

            // Assert
            Assert.IsNull(vm.NewCost.Amount);
        }

        [Test]
        public void Can_Save_Cost()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            var categoriesInDatabase = new List<CostCategory>() { new CostCategory() { Id = 1 } };
            mockCategoriesManager.Setup(x => x.GetCategories()).Returns(new OperationResult<List<CostCategory>>(categoriesInDatabase));

            var mockCostsManager = new Mock<ICostsManager>();
            var costsInDatabase = new List<Cost>();
            mockCostsManager.Setup(x => x.GetCosts()).Returns(new OperationResult<List<Cost>>(costsInDatabase));
            mockCostsManager
                .Setup(x => x.SaveCost(It.IsAny<Cost>()))
                .Callback<Cost>(cost => { costsInDatabase.Add(cost); })
                .Returns(new OperationResult());
            var vm = new CostsListViewModel(mockCategoriesManager.Object, mockCostsManager.Object);

            var costsCountBefore = vm.Costs.Count;

            vm.ShowAddCostCommand.Execute(null);

            var costCategoryMock = new Mock<ICostCategoryViewModel>();
            costCategoryMock.Setup(x => x.Id).Returns(1);

            vm.NewCost.Date = new DateTime(2000, 1, 1);
            vm.NewCost.Category = costCategoryMock.Object;
            vm.NewCost.Subject = "subject";
            vm.NewCost.Amount = 123;

            // Act
            vm.SaveCostCommand.Execute(null);

            // Assert
            Assert.AreEqual(costsCountBefore + 1, vm.Costs.Count);

            var lastCost = vm.Costs.Last();

            Assert.AreEqual(1, lastCost.Category.Id);
            Assert.AreEqual("subject", lastCost.Subject);
            Assert.AreEqual(new DateTime(2000, 1, 1), lastCost.Date);
            Assert.AreEqual(123, lastCost.Amount);
        }

        [Test]
        public void Can_Save_Only_Valid_Cost()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            var categoriesInDatabase = new List<CostCategory>();
            mockCategoriesManager.Setup(x => x.GetCategories()).Returns(new OperationResult<List<CostCategory>>(categoriesInDatabase));

            var mockCostsManager = new Mock<ICostsManager>();
            var costsInDatabase = new List<Cost>();
            mockCostsManager.Setup(x => x.GetCosts()).Returns(new OperationResult<List<Cost>>(costsInDatabase));

            var vm = new CostsListViewModel(mockCategoriesManager.Object, mockCostsManager.Object);
            vm.ShowAddCostCommand.Execute(null);

            // Act
            vm.SaveCostCommand.Execute(null);

            // Assert
            Assert.DoesNotThrow(() => mockCostsManager.Verify(x => x.SaveCost(It.IsAny<Cost>()), Times.Never()));
        }

        [Test]
        public void Can_Load_Categories()
        {
            // Arrange
            var sampleCategories = new List<CostCategory>()
            {
                new CostCategory() { Id = 1, Name = "111" },
                new CostCategory() { Id = 2, Name = "222" }
            };

            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.GetCategories()).Returns(new OperationResult<List<CostCategory>>(sampleCategories));
            var mockCostsManager = new Mock<ICostsManager>();
            mockCostsManager.Setup(x => x.GetCosts()).Returns(new OperationResult<List<Cost>>(new List<Cost>()));

            // Act
            var vm = new CostsListViewModel(mockCategoriesManager.Object, mockCostsManager.Object);

            // Assert
            Assert.IsNotNull(vm.Categories);
            Assert.AreEqual(2, vm.Categories.Count);
            Assert.AreEqual(1, vm.Categories[0].Id);
            Assert.AreEqual("111", vm.Categories[0].Name);
        }

        [Test]
        public void Can_Load_Costs()
        {
            // Arrange
            var sampleCategories = new List<CostCategory>()
            {
                new CostCategory() { Id = 1, Name = "Food" },
                new CostCategory() { Id = 2, Name = "Transport" }
            };

            var sampleCosts = new List<Cost>
            {
                new Cost() { Id = 1, Date = new DateTime(2000, 1, 1), CategoryId = 1, Subject = "Pasta", Amount = 10 },
                new Cost() { Id = 2, Date = new DateTime(2000, 1, 22), CategoryId = 2, Subject = "Bus", Amount = 10 }
            };

            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.GetCategories()).Returns(new OperationResult<List<CostCategory>>(sampleCategories));
            var mockCostsManager = new Mock<ICostsManager>();
            mockCostsManager.Setup(x => x.GetCosts()).Returns(new OperationResult<List<Cost>>(sampleCosts));

            // Act
            var vm = new CostsListViewModel(mockCategoriesManager.Object, mockCostsManager.Object);

            // Assert
            Assert.IsNotNull(vm.Costs);
            Assert.AreEqual(2, vm.Costs.Count);
            Assert.AreEqual(1, vm.Costs[0].Id);
            Assert.AreEqual("Food", vm.Costs[0].Category.Name);
        }

        [Test]
        public void Can_Load_Costs_Only_When_Categories_Available()
        {
            // Arrange
            var sampleCosts = new List<Cost>
            {
                new Cost() { Id = 1, Date = new DateTime(2000, 1, 1), CategoryId = 1, Subject = "Pasta", Amount = 10 },
                new Cost() { Id = 2, Date = new DateTime(2000, 1, 22), CategoryId = 2, Subject = "Bus", Amount = 10 }
            };

            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.GetCategories()).Returns(new OperationResult<List<CostCategory>>("Something went wrong!"));
            var mockCostsManager = new Mock<ICostsManager>();
            mockCostsManager.Setup(x => x.GetCosts()).Returns(new OperationResult<List<Cost>>(sampleCosts));

            // Act
            var vm = new CostsListViewModel(mockCategoriesManager.Object, mockCostsManager.Object);

            // Assert
            Assert.DoesNotThrow(() => mockCostsManager.Verify(x => x.GetCosts(), Times.Never));
            Assert.IsNull(vm.Costs);
        }

        private CostsListViewModel GetCostsListViewModelWithoutDatabaseReadings()
        {
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.GetCategories()).Returns(new OperationResult<List<CostCategory>>(new List<CostCategory>()));
            var mockCostsManager = new Mock<ICostsManager>();
            mockCostsManager.Setup(x => x.GetCosts()).Returns(new OperationResult<List<Cost>>(new List<Cost>()));
            return new CostsListViewModel(mockCategoriesManager.Object, mockCostsManager.Object);
        }
    }
}
