namespace BambooRestClient.Resources
{
    public class Result
    {
        public int Id { get; private set; }
        public string Key { get; private set; }
        public int Number { get; private set; }
        public string State { get; private set; }
        public string LifeCycleState { get; private set; }
        public Plan Plan { get; private set; }
        public Link Link { get; private set; }

        public Result(
            int id, 
            string key,
            int number, 
            string state, 
            string lifeCycleState, 
            Plan plan, 
            Link link)
        {
            Id = id;
            Key = key;
            Number = number;
            State = state;
            LifeCycleState = lifeCycleState;
            Plan = plan;
            Link = link;
        }

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

        public override string ToString()
        {
            return string.Format("Key: {0} Result: {1}", Key, State);
        }
    }
}