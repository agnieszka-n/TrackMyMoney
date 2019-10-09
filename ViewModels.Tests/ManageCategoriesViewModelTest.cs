using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TrackMyMoney.ViewModels.Tests
{
    [TestFixture]
    public class ManageCategoriesViewModelTest
    {
        [Test]
        public void Can_Reset_On_Cancel()
        {
            // Arrange
            var vm = new ManageCategoriesViewModel();

            // Act
            vm.CancelCommand.Execute(null);
            
            // Assert
            Assert.IsNull(vm.SelectedCategory);
        }
    }
}
