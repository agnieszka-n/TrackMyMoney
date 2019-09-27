using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TrackMyMoney.Common;
using TrackMyMoney.Models;
using TrackMyMoney.Services.Contracts;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.ViewModels.Tests
{
    [TestFixture]
    public class AddCostFormViewModelTest
    {
        [Test]
        public void Can_Cancel_Clear_New_Cost()
        {
            // Arrange
            AddCostFormViewModel vm = GetViewModel();
            vm.NewCost.Amount = 123;

            // Act
            vm.CancelCommand.Execute(null);

            // Assert
            Assert.IsNull(vm.NewCost.Amount);
        }

        [Test]
        public void Can_Raise_Cancel_Event()
        {
            // Arrange
            AddCostFormViewModel vm = GetViewModel();
            vm.NewCost.Amount = 123;

            bool eventRaised = false;
            vm.CostCancelled += () => eventRaised = true;

            // Act
            vm.CancelCommand.Execute(null);

            // Assert
            Assert.AreEqual(true, eventRaised);
        }

        [Test]
        public void Can_Raise_Save_Event()
        {
            // Arrange
            var mockCostsManager = new Mock<ICostsManager>();
            mockCostsManager
                .Setup(x => x.SaveCost(It.IsAny<Cost>()))
                .Returns(new OperationResult());

            AddCostFormViewModel vm = GetViewModel(mockCostsManager);
            FillNewCostValues(vm);

            bool eventRaised = false;
            vm.CostSaved += () => eventRaised = true;

            // Act
            vm.SaveCommand.Execute(null);

            // Assert
            Assert.AreEqual(true, eventRaised);
        }

        [Test]
        public void Can_Save_Cost()
        {
            // Arrange
            Cost newCost = null;
            var costsInDatabase = new List<Cost>();

            var mockCostsManager = new Mock<ICostsManager>();
            mockCostsManager
                .Setup(x => x.GetCosts())
                .Returns(new OperationResult<List<Cost>>(costsInDatabase));
            mockCostsManager
                .Setup(x => x.SaveCost(It.IsAny<Cost>()))
                .Callback<Cost>(cost => newCost = cost)
                .Returns(new OperationResult());

            AddCostFormViewModel vm = GetViewModel(mockCostsManager);
            FillNewCostValues(vm);

            // Act
            vm.SaveCommand.Execute(null);

            // Assert
            mockCostsManager.Verify(x => x.SaveCost(It.IsAny<Cost>()), Times.Once(), "Should call saving on a cost.");

            Assert.AreEqual(1, newCost.CategoryId);
            Assert.AreEqual("subject", newCost.Subject);
            Assert.AreEqual(new DateTime(2000, 1, 1), newCost.Date);
            Assert.AreEqual(123, newCost.Amount);
        }

        [Test]
        public void Can_Save_Only_Valid_Cost()
        {
            // Arrange
            var mockCostsManager = new Mock<ICostsManager>();
            var costsInDatabase = new List<Cost>();
            mockCostsManager.Setup(x => x.GetCosts()).Returns(new OperationResult<List<Cost>>(costsInDatabase));

            AddCostFormViewModel vm = GetViewModel(mockCostsManager);

            // Act
            vm.SaveCommand.Execute(null);

            // Assert
            mockCostsManager.Verify(x => x.SaveCost(It.IsAny<Cost>()), Times.Never, "Should never call saving on an invalid cost.");
        }

        [Test]
        public void Can_Save_Cost_Clear_Form()
        {
            // Arrange
            var mockCostsManager = new Mock<ICostsManager>();
            var costsInDatabase = new List<Cost>();
            mockCostsManager.Setup(x => x.GetCosts()).Returns(new OperationResult<List<Cost>>(costsInDatabase));
            mockCostsManager.Setup(x => x.SaveCost(It.IsAny<Cost>())).Returns(new OperationResult());

            AddCostFormViewModel vm = GetViewModel(mockCostsManager);
            FillNewCostValues(vm);

            // Act
            vm.SaveCommand.Execute(null);

            // Assert
            Assert.AreEqual(null, vm.NewCost.Subject);
        }

        private AddCostFormViewModel GetViewModel(Mock<ICostsManager> mockCostsManager = null)
        {
            if (mockCostsManager == null)
            {
                return new AddCostFormViewModel(new Mock<ICostsManager>().Object);
            }
            return new AddCostFormViewModel(mockCostsManager.Object);
        }

        private static void FillNewCostValues(AddCostFormViewModel vm)
        {
            var mockCostCategory = new Mock<ICostCategoryViewModel>();
            mockCostCategory.Setup(x => x.Id).Returns(1);

            vm.NewCost.Date = new DateTime(2000, 1, 1);
            vm.NewCost.Category = mockCostCategory.Object;
            vm.NewCost.Subject = "subject";
            vm.NewCost.Amount = 123;
        }
    }
}
