using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NUnit.Framework;
using TrackMyMoney.ViewModels;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.IntegrationTests
{
    [TestFixture]
    public class Integration_ManageCategoriesTest : TextFixtureBase
    {
        [Test]
        public void Can_Add_Category()
        {
            void Test(IKernel kernel)
            {
                // Arrange
                var mainVm = kernel.Get<ICostsListViewModel>();
                var manageCategoriesVm = mainVm.ManageCategoriesViewModel;

                // Act
                mainVm.ShowManageCategoriesCommand.Execute(null);
                AddCategory(manageCategoriesVm, "New category");

                // Assert
                var manageCategoriesCategories = manageCategoriesVm.Categories;
                Assert.AreEqual(1, manageCategoriesCategories.Count);
                Assert.AreEqual("New category", manageCategoriesCategories.First().Name);

                var costsListCategories = mainVm.Categories;
                Assert.AreEqual(1, costsListCategories.Count);
                Assert.AreEqual("New category", costsListCategories.First().Name);
            }

            RunTest(Test);
        }

        [Test]
        public void Can_Rename_Category()
        {
            void Test(IKernel kernel)
            {
                // Arrange
                var mainVm = kernel.Get<ICostsListViewModel>();
                var manageCategoriesVm = mainVm.ManageCategoriesViewModel;

                // Act
                mainVm.ShowManageCategoriesCommand.Execute(null);
                AddCategory(manageCategoriesVm, "New category");
                RenameCategory(manageCategoriesVm, manageCategoriesVm.Categories.First(), "Renamed category");

                // Assert
                var manageCategoriesCategories = manageCategoriesVm.Categories;
                Assert.AreEqual(1, manageCategoriesCategories.Count);
                Assert.AreEqual("Renamed category", manageCategoriesCategories.First().Name);

                var costsListCategories = mainVm.Categories;
                Assert.AreEqual(1, costsListCategories.Count);
                Assert.AreEqual("Renamed category", costsListCategories.First().Name);
            }

            RunTest(Test);
        }

        [Test]
        public void Can_Delete_Category()
        {
            void Test(IKernel kernel)
            {
                // Arrange
                var mainVm = kernel.Get<ICostsListViewModel>();
                var manageCategoriesVm = mainVm.ManageCategoriesViewModel;

                // Act
                mainVm.ShowManageCategoriesCommand.Execute(null);
                AddCategory(manageCategoriesVm, "New category");
                DeleteCategory(manageCategoriesVm, manageCategoriesVm.Categories.First());

                // Assert
                Assert.AreEqual(0, mainVm.Categories.Count);
                Assert.AreEqual(0, manageCategoriesVm.Categories.Count);
            }

            RunTest(Test);
        }

        [Test]
        public void Can_Prevent_Delete_Category_When_In_Use()
        {
            void Test(IKernel kernel)
            {
                // Arrange
                var mainVm = kernel.Get<ICostsListViewModel>();
                var manageCategoriesVm = mainVm.ManageCategoriesViewModel;

                mainVm.ShowManageCategoriesCommand.Execute(null);
                AddCategory(manageCategoriesVm, "New category");

                mainVm.ShowAddCostCommand.Execute(null);
                mainVm.AddCostFormViewModel.NewCost.Subject = "subject";
                mainVm.AddCostFormViewModel.NewCost.Amount = 12;
                mainVm.AddCostFormViewModel.NewCost.Category = mainVm.Categories.First();
                mainVm.AddCostFormViewModel.NewCost.Date = new DateTime(2000, 1, 15);

                mainVm.AddCostFormViewModel.SaveCommand.Execute(null);

                // Act
                DeleteCategory(manageCategoriesVm, manageCategoriesVm.Categories.First());

                // Assert
                Assert.AreEqual(1, mainVm.Categories.Count);
                Assert.AreEqual(1, manageCategoriesVm.Categories.Count);
            }

            RunTest(Test);
        }

        [Test]
        public void Can_Prevent_Add_Category_When_Name_Duplicated()
        {
            void Test(IKernel kernel)
            {
                // Arrange
                var mainVm = kernel.Get<ICostsListViewModel>();
                var manageCategoriesVm = mainVm.ManageCategoriesViewModel;

                // Act
                mainVm.ShowManageCategoriesCommand.Execute(null);
                AddCategory(manageCategoriesVm, "New category");
                AddCategory(manageCategoriesVm, "New category");

                // Assert
                Assert.AreEqual(1, mainVm.Categories.Count);
                Assert.AreEqual("New category", mainVm.Categories.First().Name);
                var manageCategoriesCategories = manageCategoriesVm.Categories;
                Assert.AreEqual(1, manageCategoriesCategories.Count);
                Assert.AreEqual("New category", manageCategoriesCategories.First().Name);
            }

            RunTest(Test);
        }

        [Test]
        public void Can_Prevent_Rename_Category_When_Name_Duplicated()
        {
            void Test(IKernel kernel)
            {
                // Arrange
                var mainVm = kernel.Get<ICostsListViewModel>();
                var manageCategoriesVm = mainVm.ManageCategoriesViewModel;

                // Act
                mainVm.ShowManageCategoriesCommand.Execute(null);
                AddCategory(manageCategoriesVm, "Category 1");
                AddCategory(manageCategoriesVm, "Category 2");
                RenameCategory(manageCategoriesVm, manageCategoriesVm.Categories.First(), "Category 2");

                // Assert
                Assert.AreEqual("Category 1", mainVm.Categories.First().Name);
                Assert.AreEqual("Category 1", manageCategoriesVm.Categories.First().Name);
            }

            RunTest(Test);
        }

        private static void AddCategory(IManageCategoriesViewModel manageCategoriesVm, string categoryName)
        {
            manageCategoriesVm.ShowAddCommand.Execute(null);
            manageCategoriesVm.NewCategoryName = categoryName;
            manageCategoriesVm.SaveAddCommand.Execute(null);
        }

        private static void RenameCategory(IManageCategoriesViewModel manageCategoriesVm, ICostCategoryViewModel category, string newCategoryName)
        {
            manageCategoriesVm.SelectedCategory = category;
            manageCategoriesVm.ShowRenameCommand.Execute(null);
            manageCategoriesVm.RenamedCategoryNewName = newCategoryName;
            manageCategoriesVm.SaveRenameCommand.Execute(null);
        }

        private static void DeleteCategory(IManageCategoriesViewModel manageCategoriesVm, ICostCategoryViewModel category)
        {
            manageCategoriesVm.SelectedCategory = category;
            manageCategoriesVm.ShowDeleteCommand.Execute(null);
            manageCategoriesVm.SaveDeleteCommand.Execute(null);
        }
    }
}
