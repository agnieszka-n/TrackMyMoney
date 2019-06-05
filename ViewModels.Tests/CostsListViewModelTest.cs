using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Tests
{
    public class CostsListViewModelTest
    {
        private CostsListViewModel GetObjectUnderTest()
        {
            return new CostsListViewModel();
        }

        [Test]
        public void Can_Cancel_Adding_Cost()
        {
            var vm = GetObjectUnderTest();

            vm.AddCostCommand.Execute(null);
            vm.NewCost.Amount = 123;
            Assert.AreEqual(vm.IsAddingCost, true);

            vm.CancelAddingCommand.Execute(null);
            Assert.AreEqual(vm.IsAddingCost, false);

            vm.AddCostCommand.Execute(null);
            Assert.IsNull(vm.NewCost.Amount);
        }

        [Test]
        public void Can_Save_Cost()
        {
            var vm = GetObjectUnderTest();
            var costsCountBefore = vm.Costs.Count;

            vm.AddCostCommand.Execute(null);

            vm.NewCost.Date = new DateTime(2000, 1, 1);
            vm.NewCost.Category = "category";
            vm.NewCost.Subject = "subject";
            vm.NewCost.Amount = 123;

            vm.SaveCostCommand.Execute(null);

            Assert.AreEqual(costsCountBefore + 1, vm.Costs.Count);
            Assert.AreEqual("category", vm.Costs.Last().Category);
            Assert.AreEqual("subject", vm.Costs.Last().Subject);
            Assert.AreEqual(new DateTime(2000, 1, 1), vm.Costs.Last().Date);
            Assert.AreEqual(123, vm.Costs.Last().Amount);
        }
    }
}
