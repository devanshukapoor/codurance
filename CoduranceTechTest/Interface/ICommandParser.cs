
namespace CoduranceTechTest.Interface
{
    public interface ICommandParser
    {
        ICommand Command { get; }
        void Parse(string userInput);
    }
}
