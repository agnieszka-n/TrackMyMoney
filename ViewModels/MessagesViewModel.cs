using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrackMyMoney.ViewModels.Contracts;

namespace TrackMyMoney.ViewModels
{
    public class MessagesViewModel : ViewModelBase, IMessagesViewModel
    {
        public ObservableCollection<Message> Messages { get; }

        public Message CurrentMessage
        {
            get
            {
                if (CurrentMessageNumber.HasValue && CurrentMessageNumber.Value > 0)
                {
                    return Messages[CurrentMessageNumber.Value - 1];
                }

                return null;
            }
        }

        public RelayCommand GoToNextMessageCommand { get; }
        public RelayCommand GoToPreviousMessageCommand { get; }
        public RelayCommand DismissCurrentMessageCommand { get; }
        public RelayCommand DismissSuccessMessagesCommand { get; }

        private int? currentMessageNumber;
        public int? CurrentMessageNumber
        {
            get => currentMessageNumber;
            set
            {
                if (Set(() => CurrentMessageNumber, ref currentMessageNumber, value))
                {
                    RaisePropertyChanged(() => CurrentMessage);
                }
            }
        }


        public MessagesViewModel()
        {
            Messages = new ObservableCollection<Message>();
            CurrentMessageNumber = null;

            GoToNextMessageCommand = new RelayCommand(GoToNextMessage);
            GoToPreviousMessageCommand = new RelayCommand(GoToPreviousMessage);
            DismissCurrentMessageCommand = new RelayCommand(DismissCurrentMessage);
            DismissSuccessMessagesCommand = new RelayCommand(DismissSuccessMessages);
        }

        public void AddMessage(string message, MessageTypes type)
        {
            Messages.Add(new Message(message, type));
            CurrentMessageNumber = Messages.Count;
        }

        private void GoToNextMessage()
        {
            CurrentMessageNumber = (CurrentMessageNumber % Messages.Count) + 1;
        }

        private void GoToPreviousMessage()
        {
            CurrentMessageNumber = ((CurrentMessageNumber + Messages.Count - 2) % Messages.Count) + 1;
        }

        private void DismissCurrentMessage()
        {
            Messages.Remove(CurrentMessage);
            UpdateCurrentMessageNumberAfterDismissing();
        }

        private void DismissSuccessMessages()
        {
            for (int i = Messages.Count - 1; i >= 0; i--)
            {
                if (Messages[i].Type == MessageTypes.SUCCESS)
                {
                    Messages.RemoveAt(i);
                    UpdateCurrentMessageNumberAfterDismissing();
                }
            }
        }

        private void UpdateCurrentMessageNumberAfterDismissing()
        {
            if (!Messages.Any())
            {
                CurrentMessageNumber = null;
                return;
            }

            if (CurrentMessageNumber > Messages.Count)
            {
                CurrentMessageNumber = Messages.Count;
                return;
            }

            // CurrentMessage is updated when CurrentMessageNumber changes, but here it didn't.
            RaisePropertyChanged(() => CurrentMessage);
        }
    }
}
