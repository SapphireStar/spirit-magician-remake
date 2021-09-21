
namespace Framework
{
    public interface ICommand:ICanSetArchitecture,IBelongToArchitecture,ICanGetSystem,ICanGetModel,ICanGetUtility,ICanSendEvent,ICanSendCommand
    {
        void Execute();
    }
    public abstract class AbstractCommand : ICommand
    {

        private IArchitecture mArchitecture;
        void ICommand.Execute()
        {
            OnExecute();
        }

        IArchitecture IBelongToArchitecture.getArchitecture()
        {
            return mArchitecture;
        }

        void ICanSetArchitecture.setArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }
        protected abstract void OnExecute();

    }
}