namespace Vlindos.Common.Notifications
{
    public interface INotifyManagerFactory
    {
        INotifyManager GetNotifyManager();
    }

    public interface INotifyManager : INotifiable
    {
        void Attach(INotifiable notifiable);
        void Deattach(INotifiable notifiable);
    }

    public class NotifyManager : INotifyManager
    {
        public void Inform()
        {
            throw new System.NotImplementedException();
        }

        public void Attach(INotifiable notifiable)
        {
            throw new System.NotImplementedException();
        }

        public void Deattach(INotifiable notifiable)
        {
            throw new System.NotImplementedException();
        }
    }
}