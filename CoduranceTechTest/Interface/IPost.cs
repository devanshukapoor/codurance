using System;

namespace CoduranceTechTest.Interface
{
    public interface IPost
    {
        string Message { get; set; }
        DateTime PostDateTime { get; }
    }
}
