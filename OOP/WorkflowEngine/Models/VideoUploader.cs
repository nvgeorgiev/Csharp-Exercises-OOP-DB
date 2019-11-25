namespace WorkflowEngine
{
    using System;

    public class VideoUploader : ITask
    {
        public void Execute()
        {
            Console.WriteLine("Uploading video...");
        }
    }
}
