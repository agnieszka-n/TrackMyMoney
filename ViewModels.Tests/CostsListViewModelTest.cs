using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using TrackMyMoney.Common;
using TrackMyMoney.Models;
using Moq;
using TrackMyMoney.Services.Contracts;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.ViewModels.Tests
{
    [TestFixture]
    public class CostsListViewModelTest
    {
        [Test]
        public void Can_Add_Cost_Click_Show_Panel()
        {
            // Arrange
            CostsListViewModel vm = GetCostsListViewModelWithoutDatabaseReadings();

            // Act
            vm.ShowAddCostCommand.Execute(null);

            // Assert
            Assert.AreEqual(CostsListMenuState.ADD_COST, vm.MenuState);
        }

        [Test]
        public void Can_Cancel_Add_Cost_Hide_Panel()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.GetCategories()).Returns(new OperationResult<List<CostCategory>>(new List<CostCategory>()));
            var mockCostsManager = new Mock<ICostsManager>();
            mockCostsManager.Setup(x => x.GetCosts()).Returns(new OperationResult<List<Cost>>(new List<Cost>()));
            var mockAddCostFormViewModel = new Mock<IAddCostFormViewModel>();

            var vm = new CostsListViewModel(mockCategoriesManager.Object, mockCostsManager.Object, mockAddCostFormViewModel.Object);
            vm.ShowAddCostCommand.Execute(null);

            // Act
            mockAddCostFormViewModel.Raise(x => x.CostCancelled += null);

            // Assert
            Assert.AreEqual(CostsListMenuState.DEFAULT, vm.MenuState);
        }

        [Test]
        public void Can_Manage_Categories_Click_Show_Panel()
        {
            // Arrange
            CostsListViewModel vm = GetCostsListViewModelWithoutDatabaseReadings();

            // Act
            vm.ShowManageCategoriesCommand.Execute(null);

            // Assert
            Assert.AreEqual(CostsListMenuState.MANAGE_CATEGORIES, vm.MenuState);
        }
        [Test]
        public void Can_Save_Cost_Refresh_List()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.GetCategories()).Returns(new OperationResult<List<CostCategory>>(new List<CostCategory>()));
            var mockCostsManager = new Mock<ICostsManager>();
            int loadingCostsCount = 0;
            mockCostsManager
                .Setup(x => x.GetCosts())
                .Callback(() => { loadingCostsCount++; })
                .Returns(new OperationResult<List<Cost>>(new List<Cost>()));
            var mockAddCostFormViewModel = new Mock<IAddCostFormViewModel>();

            var vm = new CostsListViewModel(mockCategoriesManager.Object, mockCostsManager.Object, mockAddCostFormViewModel.Object);
            vm.ShowAddCostCommand.Execute(null);
            int loadingCostsCountBefore = loadingCostsCount;

            // Act
            mockAddCostFormViewModel.Raise(x => x.CostSaved += null);

            // Assert
            mockCostsManager.Verify(x => x.GetCosts(), Times.Exactly(loadingCostsCountBefore + 1), "Should load costs after saving a new one.");
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
            var mockAddCostFormViewModel = new Mock<IAddCostFormViewModel>();

            // Act
            var vm = new CostsListViewModel(mockCategoriesManager.Object, mockCostsManager.Object, mockAddCostFormViewModel.Object);

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
            var mockAddCostFormViewModel = new Mock<IAddCostFormViewModel>();

            // Act
            var vm = new CostsListViewModel(mockCategoriesManager.Object, mockCostsManager.Object, mockAddCostFormViewModel.Object);

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
            var mockAddCostFormViewModel = new Mock<IAddCostFormViewModel>();

            // Act
            var vm = new CostsListViewModel(mockCategoriesManager.Object, mockCostsManager.Object, mockAddCostFormViewModel.Object);

            // Assert
            mockCostsManager.Verify(x => x.GetCosts(), Times.Never, "Should never get costs when there are no categories loaded.");
            Assert.IsNull(vm.Costs);
        }

        private CostsListViewModel GetCostsListViewModelWithoutDatabaseReadings()
        {
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.GetCategories()).Returns(new OperationResult<List<CostCategory>>(new List<CostCategory>()));
            var mockCostsManager = new Mock<ICostsManager>();
            mockCostsManager.Setup(x => x.GetCosts()).Returns(new OperationResult<List<Cost>>(new List<Cost>()));
            var mockAddCostFormViewModel = new Mock<IAddCostFormViewModel>();
            return new CostsListViewModel(mockCategoriesManager.Object, mockCostsManager.Object, mockAddCostFormViewModel.Object);
        }
    }
}
