using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMyMoney.Common;
using TrackMyMoney.Services.Contracts;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.ViewModels.Tests
{
    [TestFixture]
    public class ManageCategoriesViewModelTest
    {
        [Test]
        public void Can_Show_Rename_Panel()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            var vm = new ManageCategoriesViewModel(mockCategoriesManager.Object)
            {
                SelectedCategory = new CostCategoryViewModel() { Name = "SelectedCategory" }
            };

            // Act
            vm.ShowRenameCommand.Execute(null);

            // Assert
            Assert.AreEqual(ManageCategoriesMenuState.RENAME, vm.MenuState);
        }

        [Test]
        public void Can_Cancel_Any_Action()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            var vm = new ManageCategoriesViewModel(mockCategoriesManager.Object)
            {
                SelectedCategory = new CostCategoryViewModel()
            };
            vm.ShowRenameCommand.Execute(null);

            // Act
            vm.CancelActionCommand.Execute(null);

            // Assert
            Assert.AreEqual(ManageCategoriesMenuState.DEFAULT, vm.MenuState);
        }

        [Test]
        public void Can_Rename_Category()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.RenameCategory(It.IsAny<int>(), It.IsAny<string>())).Returns(new OperationResult());

            var mockCategory = new Mock<ICostCategoryViewModel>();
            mockCategory.Setup(x => x.Id).Returns(1);

            var vm = new ManageCategoriesViewModel(mockCategoriesManager.Object);
            vm.RenamedCategoryNewName = "New name";
            vm.SelectedCategory = mockCategory.Object;

            var isRenamedInvoked = false;
            vm.Renamed += () => isRenamedInvoked = true;

            // Act
            vm.SaveRenameCommand.Execute(null);

            // Assert
            Assert.AreEqual(true, isRenamedInvoked);
            Assert.IsNull(vm.RenamedCategoryNewName);
            mockCategoriesManager.Verify(x => x.GetCategories(), Times.Never, "Should not reload categories after renaming one of them.");
        }
    }
}
