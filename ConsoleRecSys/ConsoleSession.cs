using RecommendationSystemInterface;

namespace ConsoleRecSys
{
    public class ConsoleSession : Session
    {
        public ConsoleSession(Viewer viewer) : base(viewer)
        {

        }
    }

    class ConsoleSubjectSession : ConsoleSession
    {
        public ConsoleSubjectSession(Viewer viewer) : base(viewer)
        {

        }

        public void AddFavourite() // tady spis ne, nebo ne?
        {

        }
    }
}
