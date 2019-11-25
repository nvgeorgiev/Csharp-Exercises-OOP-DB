namespace WorkflowEngine
{
    using System;

    public class CallWebService : ITask
    {
        public void Execute()
        {
            Console.WriteLine("Calling web service...");
        }
    }
}
