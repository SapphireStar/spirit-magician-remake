
namespace Framework
{
    public interface IController : IBelongToArchitecture,ICanGetModel,ICanGetSystem,ICanSendCommand,ICanRegisterEvent
    {

    }
}