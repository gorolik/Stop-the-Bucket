namespace Sources.Infrastructure.PersistentProgress.Structure
{
    public class CompletedLevel
    {
        public int Id;
        public int Stars;

        public CompletedLevel(int id, int stars)
        {
            Id = id;
            Stars = stars;
        }
    }
}