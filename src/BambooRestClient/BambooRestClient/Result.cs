namespace BambooRestClient
{
    public class Result
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string State { get; set; }
        public string LifeCycleState { get; set; }
        public Plan Plan { get; set; }
        public Link Link { get; set; }

        public bool IsFor(string planKey)
        {
            return Plan.Key.Contains(planKey);
        }

        public bool WasSuccessful()
        {
            return State.Equals("Successful");
        }
        public bool Finished()
        {
            return LifeCycleState.Equals("Finished");
        }
    }
}