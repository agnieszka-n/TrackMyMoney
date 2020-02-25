using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.ViewModels.Tests
{
    [TestFixture]
    public class MessagesViewModelTest
    {
        [Test]
        public void Can_Add_Messages()
        {
            // Arrange
            MessagesViewModel vm = new MessagesViewModel();

            // Act
            vm.AddMessage("Message 1", MessageTypes.SUCCESS);
            vm.AddMessage("Message 2", MessageTypes.SUCCESS);

            // Assert
            Assert.AreEqual(2, vm.Messages.Count);
            Assert.AreEqual(2, vm.CurrentMessageNumber);
            Assert.AreEqual("Message 2", vm.CurrentMessage.Text);
        }

        [Test]
        public void Can_Scroll_Messages_Forward()
        {
            // Arrange
            MessagesViewModel vm = new MessagesViewModel();
            vm.AddMessage("Message 1", MessageTypes.SUCCESS);
            vm.AddMessage("Message 2", MessageTypes.SUCCESS);

            // Act
            vm.GoToPreviousMessageCommand.Execute(null);
            vm.GoToNextMessageCommand.Execute(null);

            // Assert
            Assert.AreEqual(2, vm.CurrentMessageNumber);
            Assert.AreEqual("Message 2", vm.CurrentMessage.Text);
        }

        [Test]
        public void Can_Scroll_Messages_Backward()
        {
            // Arrange
            MessagesViewModel vm = new MessagesViewModel();
            vm.AddMessage("Message 1", MessageTypes.SUCCESS);
            vm.AddMessage("Message 2", MessageTypes.SUCCESS);

            // Act
            vm.GoToPreviousMessageCommand.Execute(null);

            // Assert
            Assert.AreEqual(1, vm.CurrentMessageNumber);
            Assert.AreEqual("Message 1", vm.CurrentMessage.Text);
        }

        [Test]
        public void Can_Scroll_Extreme_Messages_Forward()
        {
            // Arrange
            MessagesViewModel vm = new MessagesViewModel();

            vm.AddMessage("Message 1", MessageTypes.SUCCESS);
            vm.AddMessage("Message 2", MessageTypes.SUCCESS);
            vm.AddMessage("Message 3", MessageTypes.SUCCESS);

            // Act
            vm.GoToNextMessageCommand.Execute(null);

            // Assert
            Assert.AreEqual(1, vm.CurrentMessageNumber);
            Assert.AreEqual("Message 1", vm.CurrentMessage.Text);
        }

        [Test]
        public void Can_Scroll_Extreme_Messages_Backward()
        {
            // Arrange
            MessagesViewModel vm = new MessagesViewModel();

            vm.AddMessage("Message 1", MessageTypes.SUCCESS);
            vm.AddMessage("Message 2", MessageTypes.SUCCESS);
            vm.AddMessage("Message 3", MessageTypes.SUCCESS);

            // Act
            vm.GoToPreviousMessageCommand.Execute(null);
            vm.GoToPreviousMessageCommand.Execute(null);
            vm.GoToPreviousMessageCommand.Execute(null);

            // Assert
            Assert.AreEqual(3, vm.CurrentMessageNumber);
            Assert.AreEqual("Message 3", vm.CurrentMessage.Text);
        }

        [Test]
        public void Can_Dismiss_Newest_Message()
        {
            // Arrange
            MessagesViewModel vm = new MessagesViewModel();

            vm.AddMessage("Message 1", MessageTypes.SUCCESS);
            vm.AddMessage("Message 2", MessageTypes.SUCCESS);

            // Act
            vm.DismissCurrentMessageCommand.Execute(null);

            // Assert
            Assert.AreEqual(1, vm.Messages.Count);
            Assert.AreEqual(1, vm.CurrentMessageNumber);
            Assert.AreEqual("Message 1", vm.CurrentMessage.Text);
        }

        [Test]
        public void Can_Dismiss_Not_Newest_Message()
        {
            // Arrange
            MessagesViewModel vm = new MessagesViewModel();

            vm.AddMessage("Message 1", MessageTypes.SUCCESS);
            vm.AddMessage("Message 2", MessageTypes.SUCCESS);
            vm.AddMessage("Message 3", MessageTypes.SUCCESS);

            // Act
            vm.GoToPreviousMessageCommand.Execute(null);
            vm.DismissCurrentMessageCommand.Execute(null);

            // Assert
            Assert.AreEqual(2, vm.Messages.Count);
            Assert.AreEqual(2, vm.CurrentMessageNumber);
            Assert.AreEqual("Message 3", vm.CurrentMessage.Text);
        }

        [Test]
        public void Can_Set_Current_Message_To_Null_When_Last_Message_Dismissed()
        {
            // Arrange
            MessagesViewModel vm = new MessagesViewModel();
            vm.AddMessage("Message 1", MessageTypes.SUCCESS);

            // Act
            vm.DismissCurrentMessageCommand.Execute(null);

            // Assert
            Assert.AreEqual(0, vm.Messages.Count);
            Assert.IsNull(vm.CurrentMessage);
            Assert.IsNull(vm.CurrentMessageNumber);
        }

        [Test]
        public void Can_Update_Current_Message_Number_When_Newest_Message_Dismissed()
        {
            // Arrange
            MessagesViewModel vm = new MessagesViewModel();
            vm.AddMessage("Message 1", MessageTypes.SUCCESS);
            vm.AddMessage("Message 2", MessageTypes.SUCCESS);

            // Act
            vm.DismissCurrentMessageCommand.Execute(null);

            // Assert
            Assert.AreEqual(1, vm.CurrentMessageNumber);
        }

        [Test]
        public void Can_Keep_Current_Message_Number_When_Not_Newest_Message_Dismissed()
        {
            // Arrange
            MessagesViewModel vm = new MessagesViewModel();
            vm.AddMessage("Message 1", MessageTypes.SUCCESS);
            vm.AddMessage("Message 2", MessageTypes.SUCCESS);
            vm.AddMessage("Message 3", MessageTypes.SUCCESS);

            // Act
            vm.GoToPreviousMessageCommand.Execute(null);
            vm.DismissCurrentMessageCommand.Execute(null);

            // Assert
            Assert.AreEqual(2, vm.CurrentMessageNumber);
        }
        
        [Test]
        public void Can_Set_Current_Message_To_Null_When_All_Messages_Dismissed()
        {
            // Arrange
            MessagesViewModel vm = new MessagesViewModel();
            vm.AddMessage("Message 1", MessageTypes.SUCCESS);
            vm.AddMessage("Message 2", MessageTypes.SUCCESS);

            // Act
            vm.DismissSuccessMessagesCommand.Execute(null);

            // Assert
            Assert.IsNull(vm.CurrentMessage);
            Assert.IsNull(vm.CurrentMessageNumber);
        }

        [Test]
        public void Can_Dismiss_All_Success_Messages_When_Displaying_Success_Message_And_Error_Is_Left()
        {
            // Arrange
            MessagesViewModel vm = new MessagesViewModel();

            vm.AddMessage("Message 1", MessageTypes.SUCCESS);
            vm.AddMessage("Message 2", MessageTypes.ERROR);
            vm.AddMessage("Message 3", MessageTypes.SUCCESS);

            // Act
            vm.DismissSuccessMessagesCommand.Execute(null);

            // Assert
            Assert.AreEqual(1, vm.Messages.Count);
            Assert.AreEqual(1, vm.CurrentMessageNumber);
            Assert.AreEqual("Message 2", vm.CurrentMessage.Text);
        }

        [Test]
        public void Can_Dismiss_All_Success_Messages_When_Displaying_Error_Message()
        {
            // Arrange
            MessagesViewModel vm = new MessagesViewModel();

            vm.AddMessage("Message 1", MessageTypes.SUCCESS);
            vm.AddMessage("Message 2", MessageTypes.ERROR);
            vm.AddMessage("Message 3", MessageTypes.SUCCESS);

            // Act
            vm.GoToPreviousMessageCommand.Execute(null);
            vm.DismissSuccessMessagesCommand.Execute(null);

            // Assert
            Assert.AreEqual(1, vm.Messages.Count);
            Assert.AreEqual(1, vm.CurrentMessageNumber);
            Assert.AreEqual("Message 2", vm.CurrentMessage.Text);
        }
    }
}
