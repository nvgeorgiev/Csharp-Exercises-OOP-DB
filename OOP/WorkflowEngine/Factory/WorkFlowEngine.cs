namespace WorkflowEngine
{
    public class WorkFlowEngine
    {
        public void Run(IWorkFlow workFlow)
        {
            foreach (var task in workFlow.GetTasks())
            {
                task.Execute();
            }
        }
    }
}
