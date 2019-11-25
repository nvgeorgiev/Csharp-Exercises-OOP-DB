namespace WorkflowEngine
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var workFlow = new WorkFlow();

            workFlow.Add(new VideoUploader());
            var callWebService = new CallWebService();
            workFlow.Add(callWebService);
            workFlow.Add(new SendEmail());
            workFlow.Add(new ChangeStatus());

            var engine = new WorkFlowEngine();

            engine.Run(workFlow);

            Console.WriteLine();

            workFlow.Remove(callWebService);
           
            engine.Run(workFlow);
        }
    }
}
