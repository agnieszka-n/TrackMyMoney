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
using TrackMyMoney.Services.Contracts.Messages;
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

            var vm = GetCostsListViewModel(mockCategoriesManager, mockCostsManager, mockAddCostFormViewModel, null);
            vm.ShowAddCostCommand.Execute(null);

            // Act
            mockAddCostFormViewModel.Raise(x => x.Cancelled += null);

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
        public void Can_Cancel_Manage_Categories_Hide_Panel()
        {
            // Arrange
            var mockManageCategoriesViewModel = new Mock<IManageCategoriesViewModel>();

            var vm = GetCostsListViewModel(null, null, null, mockManageCategoriesViewModel);
            vm.ShowManageCategoriesCommand.Execute(null);

            // Act
            mockManageCategoriesViewModel.Raise(x => x.WentBack += null);

            // Assert
            Assert.AreEqual(CostsListMenuState.DEFAULT, vm.MenuState);
        }

        [Test]
        public void Can_Refresh_List_On_Cost_Added()
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

            var vm = GetCostsListViewModel(mockCategoriesManager, mockCostsManager, mockAddCostFormViewModel, null);
            vm.ShowAddCostCommand.Execute(null);
            int loadingCostsCountBefore = loadingCostsCount;

            // Act
            mockAddCostFormViewModel.Raise(x => x.Saved += null);

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

            // Act
            var vm = GetCostsListViewModel(mockCategoriesManager, mockCostsManager, null, null);
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
            var vm = GetCostsListViewModel(mockCategoriesManager, mockCostsManager, null, null);

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
            var vm = GetCostsListViewModel(mockCategoriesManager, mockCostsManager, null, null);

            // Assert
            mockCostsManager.Verify(x => x.GetCosts(), Times.Never, "Should never get costs when there are no categories loaded.");
            Assert.IsNull(vm.Costs);
        }

        [Test]
        public void Can_Refresh_Categories_On_Category_Deleted()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            var loadingCategoriesCount = 0;
            mockCategoriesManager
                .Setup(x => x.GetCategories())
                .Callback(() => { loadingCategoriesCount++; })
                .Returns(new OperationResult<List<CostCategory>>(new List<CostCategory>()));

            var mockCostsManager = new Mock<ICostsManager>();
            var loadingCostsCount = 0;
            mockCostsManager
                .Setup(x => x.GetCosts())
                .Callback(() => { loadingCostsCount++; })
                .Returns(new OperationResult<List<Cost>>(new List<Cost>()));

            var mockManageCategoriesViewModel = new Mock<IManageCategoriesViewModel>();
            var vm = GetCostsListViewModel(mockCategoriesManager, mockCostsManager, null, mockManageCategoriesViewModel);

            vm.ShowManageCategoriesCommand.Execute(null);

            var loadingCategoriesCountBefore = loadingCategoriesCount;
            var loadingCostsCountBefore = loadingCostsCount;

            // Act
            mockManageCategoriesViewModel.Raise(x => x.Deleted += null);

            // Assert
            Assert.AreEqual(CostsListMenuState.MANAGE_CATEGORIES, vm.MenuState);
            mockCategoriesManager.Verify(x => x.GetCategories(), Times.Exactly(loadingCategoriesCountBefore + 1), "Should reload categories after deleting one of them.");
            mockCostsManager.Verify(x => x.GetCosts(), Times.Exactly(loadingCostsCountBefore), "Should not reload costs after deleting a category.");
        }

        [Test]
        public void Can_Refresh_Categories_On_Category_Renamed()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            var loadingCategoriesCount = 0;
            mockCategoriesManager
                .Setup(x => x.GetCategories())
                .Callback(() => { loadingCategoriesCount++; })
                .Returns(new OperationResult<List<CostCategory>>(new List<CostCategory>()));

            var mockCostsManager = new Mock<ICostsManager>();
            var loadingCostsCount = 0;
            mockCostsManager
                .Setup(x => x.GetCosts())
                .Callback(() => { loadingCostsCount++; })
                .Returns(new OperationResult<List<Cost>>(new List<Cost>()));

            var mockManageCategoriesViewModel = new Mock<IManageCategoriesViewModel>();
            var vm = GetCostsListViewModel(mockCategoriesManager, mockCostsManager, null, mockManageCategoriesViewModel);

            vm.ShowManageCategoriesCommand.Execute(null);

            var loadingCategoriesCountBefore = loadingCategoriesCount;
            var loadingCostsCountBefore = loadingCostsCount;

            // Act
            mockManageCategoriesViewModel.Raise(x => x.Renamed += null);

            // Assert
            Assert.AreEqual(CostsListMenuState.MANAGE_CATEGORIES, vm.MenuState);
            mockCategoriesManager.Verify(x => x.GetCategories(), Times.Exactly(loadingCategoriesCountBefore + 1), "Should reload categories after renaming one of them.");
            mockCostsManager.Verify(x => x.GetCosts(), Times.Exactly(loadingCostsCountBefore), "Should not reload costs after renaming a category.");
        }

        [Test]
        public void Can_Refresh_Categories_On_Category_Added()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            var loadingCategoriesCount = 0;
            mockCategoriesManager
                .Setup(x => x.GetCategories())
                .Callback(() => { loadingCategoriesCount++; })
                .Returns(new OperationResult<List<CostCategory>>(new List<CostCategory>()));

            var mockCostsManager = new Mock<ICostsManager>();
            var loadingCostsCount = 0;
            mockCostsManager
                .Setup(x => x.GetCosts())
                .Callback(() => { loadingCostsCount++; })
                .Returns(new OperationResult<List<Cost>>(new List<Cost>()));

            var mockManageCategoriesViewModel = new Mock<IManageCategoriesViewModel>();
            var vm = GetCostsListViewModel(mockCategoriesManager, mockCostsManager, null, mockManageCategoriesViewModel);

            vm.ShowManageCategoriesCommand.Execute(null);

            int loadingCategoriesCountBefore = loadingCategoriesCount;
            int loadingCostsCountBefore = loadingCostsCount;

            // Act
            mockManageCategoriesViewModel.Raise(x => x.Added += null);

            // Assert
            mockCategoriesManager.Verify(x => x.GetCategories(), Times.Exactly(loadingCategoriesCountBefore + 1), "Should reload categories after adding one.");
            mockCostsManager.Verify(x => x.GetCosts(), Times.Exactly(loadingCostsCountBefore), "Should not reload costs after adding a category.");
        }

        private CostsListViewModel GetCostsListViewModelWithoutDatabaseReadings()
        {
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.GetCategories()).Returns(new OperationResult<List<CostCategory>>(new List<CostCategory>()));
            var mockCostsManager = new Mock<ICostsManager>();
            mockCostsManager.Setup(x => x.GetCosts()).Returns(new OperationResult<List<Cost>>(new List<Cost>()));

            return GetCostsListViewModel(mockCategoriesManager, mockCostsManager, null, null);
        }

        private CostsListViewModel GetCostsListViewModel(Mock<ICategoriesManager> mockCategoriesManager, Mock<ICostsManager> mockCostsManager, Mock<IAddCostFormViewModel> mockAddCostFormVm, Mock<IManageCategoriesViewModel> mockManageCategoriesVm)
        {
            if (mockCategoriesManager == null)
            {
                mockCategoriesManager = new Mock<ICategoriesManager>();
                mockCategoriesManager.Setup(x => x.GetCategories()).Returns(new OperationResult<List<CostCategory>>(new List<CostCategory>()));
            }

            if (mockCostsManager == null)
            {
                mockCostsManager = new Mock<ICostsManager>();
                mockCostsManager.Setup(x => x.GetCosts()).Returns(new OperationResult<List<Cost>>(new List<Cost>()));
            }

            mockAddCostFormVm = mockAddCostFormVm ?? new Mock<IAddCostFormViewModel>();
            mockManageCategoriesVm = mockManageCategoriesVm ?? new Mock<IManageCategoriesViewModel>();
            var mockMessagesService = new Mock<IMessagesService>();

            return new CostsListViewModel(mockCategoriesManager.Object, mockCostsManager.Object, mockMessagesService.Object, mockAddCostFormVm.Object, mockManageCategoriesVm.Object);
        }
    }
}
