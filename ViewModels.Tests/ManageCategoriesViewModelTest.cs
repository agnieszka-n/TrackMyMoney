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
            var vm = GetViewModel(mockCategoriesManager);
            vm.SelectedCategory = new CostCategoryViewModel()
            {
                Name = "SelectedCategory"
            };

            // Act
            vm.ShowRenameCommand.Execute(null);

            // Assert
            Assert.AreEqual(ManageCategoriesMenuState.RENAME, vm.MenuState);
        }

        [Test]
        public void Can_Go_Back_When_Cancel_Rename_Category_Clicked()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            var vm = GetViewModel(mockCategoriesManager);
            vm.SelectedCategory = new CostCategoryViewModel();
            vm.ShowRenameCommand.Execute(null);

            // Act
            vm.CancelActionCommand.Execute(null);

            // Assert
            Assert.AreEqual(ManageCategoriesMenuState.DEFAULT, vm.MenuState);
        }

        [Test]
        public void Can_Clear_New_Category_Name_When_Renaming_Cancelled()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            var vm = GetViewModel(mockCategoriesManager);
            vm.SelectedCategory = new CostCategoryViewModel();
            vm.ShowRenameCommand.Execute(null);
            vm.RenamedCategoryNewName = "New name";

            // Act
            vm.CancelActionCommand.Execute(null);

            // Assert
            Assert.IsNull(vm.RenamedCategoryNewName);
        }

        [Test]
        public void Can_Rename_Category()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.RenameCategory(It.IsAny<int>(), It.IsAny<string>())).Returns(new OperationResult());

            var mockCategory = new Mock<ICostCategoryViewModel>();
            mockCategory.Setup(x => x.Id).Returns(1);

            var vm = GetViewModel(mockCategoriesManager);
            vm.ShowRenameCommand.Execute(null);
            vm.SelectedCategory = mockCategory.Object;
            vm.RenamedCategoryNewName = "New name";

            var isRenamedInvoked = false;
            vm.Renamed += () => isRenamedInvoked = true;

            // Act
            vm.SaveRenameCommand.Execute(null);

            // Assert
            Assert.AreEqual(true, isRenamedInvoked);
            Assert.AreEqual(ManageCategoriesMenuState.DEFAULT, vm.MenuState);
            Assert.IsNull(vm.RenamedCategoryNewName);
            mockCategoriesManager.Verify(x => x.GetCategories(), Times.Never, "Should not reload categories after renaming one of them.");
        }

        [Test]
        public void Can_Stay_In_Renaming_Category_When_Saving_Fails()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.RenameCategory(It.IsAny<int>(), It.IsAny<string>())).Returns(new OperationResult("Something went wrong!"));

            var mockCategory = new Mock<ICostCategoryViewModel>();
            mockCategory.Setup(x => x.Id).Returns(1);

            var vm = GetViewModel(mockCategoriesManager);
            vm.ShowRenameCommand.Execute(null);
            vm.SelectedCategory = mockCategory.Object;
            vm.RenamedCategoryNewName = "New name";

            var isRenamedInvoked = false;
            vm.Renamed += () => isRenamedInvoked = true;

            // Act
            vm.SaveRenameCommand.Execute(null);

            // Assert
            Assert.AreEqual(false, isRenamedInvoked);
            Assert.AreEqual(ManageCategoriesMenuState.RENAME, vm.MenuState);
            Assert.AreEqual("New name", vm.RenamedCategoryNewName);
            mockCategoriesManager.Verify(x => x.GetCategories(), Times.Never, "Should not reload categories when renaming fails.");
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Can_Prevent_Renaming_Category_When_No_Name_Provided(object categoryName)
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.RenameCategory(It.IsAny<int>(), It.IsAny<string>())).Returns(new OperationResult());

            var mockCategory = new Mock<ICostCategoryViewModel>();
            mockCategory.Setup(x => x.Id).Returns(1);

            var vm = GetViewModel(mockCategoriesManager);
            vm.ShowRenameCommand.Execute(null);
            vm.SelectedCategory = mockCategory.Object;
            vm.RenamedCategoryNewName = categoryName as string;

            var isRenamedInvoked = false;
            vm.Renamed += () => isRenamedInvoked = true;

            // Act
            vm.SaveRenameCommand.Execute(null);

            // Assert
            Assert.AreEqual(false, isRenamedInvoked);
            Assert.AreEqual(ManageCategoriesMenuState.RENAME, vm.MenuState);
            Assert.AreEqual(categoryName, vm.RenamedCategoryNewName);
            mockCategoriesManager.Verify(x => x.GetCategories(), Times.Never, "Should not reload categories when a category hasn't been renamed.");
        }

        [Test]
        public void Can_Add_Category()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.AddCategory(It.IsAny<string>())).Returns(new OperationResult());

            var vm = GetViewModel(mockCategoriesManager);
            vm.ShowAddCommand.Execute(null);
            vm.NewCategoryName = "New category";

            var isAddedInvoked = false;
            vm.Added += () => isAddedInvoked = true;

            // Act
            vm.SaveAddCommand.Execute(null);

            // Assert
            Assert.AreEqual(true, isAddedInvoked);
            Assert.AreEqual(ManageCategoriesMenuState.DEFAULT, vm.MenuState);
            Assert.IsNull(vm.NewCategoryName);
            mockCategoriesManager.Verify(x => x.GetCategories(), Times.Never, "Should not reload categories after adding one.");
        }

        [Test]
        public void Can_Clear_New_Category_Name_When_Adding_Cancelled()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            var vm = GetViewModel(mockCategoriesManager);
            vm.ShowAddCommand.Execute(null);
            vm.NewCategoryName = "New name";

            // Act
            vm.CancelActionCommand.Execute(null);

            // Assert
            Assert.IsNull(vm.NewCategoryName);
        }

        [Test]
        public void Can_Stay_In_Adding_Category_When_Saving_Fails()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.AddCategory(It.IsAny<string>())).Returns(new OperationResult("Something went wrong!"));

            var vm = GetViewModel(mockCategoriesManager);
            vm.ShowAddCommand.Execute(null);
            vm.NewCategoryName = "New category";

            var isAddedInvoked = false;
            vm.Added += () => isAddedInvoked = true;

            // Act
            vm.SaveAddCommand.Execute(null);

            // Assert
            Assert.AreEqual(false, isAddedInvoked);
            Assert.AreEqual(ManageCategoriesMenuState.ADD, vm.MenuState);
            Assert.AreEqual("New category", vm.NewCategoryName);
            mockCategoriesManager.Verify(x => x.GetCategories(), Times.Never, "Should not reload categories when adding fails.");
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Can_Prevent_Adding_Category_When_No_Name_Provided(object categoryName)
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.AddCategory(It.IsAny<string>())).Returns(new OperationResult());

            var vm = GetViewModel(mockCategoriesManager);
            vm.ShowAddCommand.Execute(null);
            vm.NewCategoryName = categoryName as string;

            var isAddedInvoked = false;
            vm.Added += () => isAddedInvoked = true;

            // Act
            vm.SaveAddCommand.Execute(null);

            // Assert
            Assert.AreEqual(false, isAddedInvoked);
            Assert.AreEqual(ManageCategoriesMenuState.ADD, vm.MenuState);
            Assert.AreEqual(categoryName, vm.NewCategoryName);
            mockCategoriesManager.Verify(x => x.GetCategories(), Times.Never, "Should not reload categories when a category hasn't been added.");
        }

        [Test]
        public void Can_Delete_Category()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.DeleteCategory(It.IsAny<int>())).Returns(new OperationResult());

            var vm = GetViewModel(mockCategoriesManager);
            vm.SelectedCategory = new CostCategoryViewModel()
            {
                Id = 1
            };
            vm.ShowDeleteCommand.Execute(null);

            var isDeletedInvoked = false;
            vm.Deleted += () => isDeletedInvoked = true;

            // Act
            vm.ConfirmDeleteCommand.Execute(null);

            // Assert
            Assert.AreEqual(true, isDeletedInvoked);
            Assert.AreEqual(ManageCategoriesMenuState.DEFAULT, vm.MenuState);
            Assert.IsNull(vm.SelectedCategory);
            mockCategoriesManager.Verify(x => x.GetCategories(), Times.Never, "Should not reload categories after deleting one.");
        }

        [Test]
        public void Can_Stay_In_Deleting_Category_When_Saving_Fails()
        {
            // Arrange
            var mockCategoriesManager = new Mock<ICategoriesManager>();
            mockCategoriesManager.Setup(x => x.DeleteCategory(It.IsAny<int>())).Returns(new OperationResult("Something went wrong!"));

            var vm = GetViewModel(mockCategoriesManager);
            vm.SelectedCategory = new CostCategoryViewModel()
            {
                Id = 1
            };
            vm.ShowDeleteCommand.Execute(null);

            var isDeletedInvoked = false;
            vm.Deleted += () => isDeletedInvoked = true;

            // Act
            vm.ConfirmDeleteCommand.Execute(null);

            // Assert
            Assert.AreEqual(false, isDeletedInvoked);
            Assert.AreEqual(ManageCategoriesMenuState.DELETE, vm.MenuState);
            Assert.AreEqual(1, vm.SelectedCategory.Id);
            mockCategoriesManager.Verify(x => x.GetCategories(), Times.Never, "Should not reload categories when deleting fails.");
        }

        private ManageCategoriesViewModel GetViewModel(Mock<ICategoriesManager> mockCategoriesManager)
        {
            var mockMessagesVm = new Mock<IMessagesViewModel>();
            return new ManageCategoriesViewModel(mockCategoriesManager.Object, mockMessagesVm.Object);
        }
    }
}
