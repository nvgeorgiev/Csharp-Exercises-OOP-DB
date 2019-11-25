namespace WorkflowEngine
{
    using System;

    public class SendEmail : ITask
    {
        public void Execute()
        {
            Console.WriteLine("Sending e-mail...");
        }
    }
}
