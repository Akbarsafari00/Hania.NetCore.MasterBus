namespace Hania.NetCore.MasterBus.Core.Commands
{
    public class CommandResult
    {
        
    }
    
    public class CommandResult<TData>: CommandResult
    {
        internal TData _data;
        public TData Data
        {
            get
            {
                return _data;
            }
        }   
    }
}