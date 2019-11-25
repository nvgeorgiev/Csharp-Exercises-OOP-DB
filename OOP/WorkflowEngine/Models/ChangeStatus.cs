namespace WorkflowEngine
{
    using System;

    public class ChangeStatus : ITask
    {
        public void Execute()
        {
            Console.WriteLine("Status changed...");
        }
    }
}
