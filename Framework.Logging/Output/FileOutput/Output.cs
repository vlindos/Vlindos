namespace Vlindos.Logging.Output.FileOutput
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
                return "file";
            }
        }

        public IOutputEngine GetEngine()
        {
            return _engineFactory.GetOutputEngine();
        }
    }
}
