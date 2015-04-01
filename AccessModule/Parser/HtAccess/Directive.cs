namespace AccessModule.Parser.HtAccess
{
    public class Directive : ICommand
    {
        public string Name { get; set; }

        public object[] Values;
    }
}