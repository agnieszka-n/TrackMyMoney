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
    public class Integration_ManageCostsTest : TextFixtureBase
    {
        [Test]
        public void Can_Add_Cost()
        {
            void Test(IKernel kernel)
            {
                // Arrange
                var mainVm = kernel.Get<ICostsListViewModel>();
                mainVm.ShowManageCategoriesCommand.Execute(null);
                AddCategory(mainVm.ManageCategoriesViewModel, "Category");
                var category = mainVm.Categories.First();

                mainVm.ShowAddCostCommand.Execute(null);
                var addCostFormVm = mainVm.AddCostFormViewModel;

                addCostFormVm.NewCost.Subject = "Pizza";
                addCostFormVm.NewCost.Date = new DateTime(2000, 1, 15);
                addCostFormVm.NewCost.Amount = 12;
                addCostFormVm.NewCost.Category = category;

                // Act
                addCostFormVm.SaveCommand.Execute(null);

                // Assert
                var costs = mainVm.Costs;
                Assert.AreEqual(1, costs.Count);
                Assert.AreEqual("Pizza", costs.First().Subject);
            }

            RunTest(Test);
        }

        [Test]
        public void Can_Delete_Cost()
        {
            void Test(IKernel kernel)
            {
                // Arrange
                var mainVm = kernel.Get<ICostsListViewModel>();
                mainVm.ShowManageCategoriesCommand.Execute(null);
                AddCategory(mainVm.ManageCategoriesViewModel, "Category");
                var category = mainVm.Categories.First();

                mainVm.ShowAddCostCommand.Execute(null);
                var addCostFormVm = mainVm.AddCostFormViewModel;

                addCostFormVm.NewCost.Subject = "Pizza";
                addCostFormVm.NewCost.Date = new DateTime(2000, 1, 15);
                addCostFormVm.NewCost.Amount = 12;
                addCostFormVm.NewCost.Category = category;
                addCostFormVm.SaveCommand.Execute(null);

                // Act
                mainVm.DeleteCostCommand.Execute(mainVm.Costs.First());

                // Assert
                Assert.AreEqual(0, mainVm.Costs.Count);
            }

            RunTest(Test);
        }

        private void AddCategory(IManageCategoriesViewModel manageCategoriesVm, string categoryName)
        {
            manageCategoriesVm.ShowAddCommand.Execute(null);
            manageCategoriesVm.NewCategoryName = categoryName;
            manageCategoriesVm.SaveAddCommand.Execute(null);
        }
    }
}
