namespace Vlindos.Logging.Output.ConsoleOutput
{
    public class Output : IOutput
    {
        private readonly IOutputEngineFactory _engineFactory;

        public Output(IOutputEngineFactory engineFactory)
        {
            _engineFactory = engineFactory;
        }

        public string Type 
        {
            get
            {
                return "console";
            }
        }

        public IOutputEngine GetEngine()
        {
            return _engineFactory.GetOutputEngine();
        }
    }
}
