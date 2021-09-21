

namespace Framework
{
    public interface ICanSendEvent:IBelongToArchitecture
    {

    }

    public static class CanSendEventExtension
    {
        public static void SendEvent<T>(this ICanSendEvent self) where T:new()
        {
            self.getArchitecture().SendEvent<T>();
        }

        public static void SendEvent<T>(this ICanSendEvent self,T e)
        {
            self.getArchitecture().SendEvent<T>(e);
        }
    }
}