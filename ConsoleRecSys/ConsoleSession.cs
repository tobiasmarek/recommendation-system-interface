using RecommendationSystemInterface;

namespace ConsoleRecSys
{
    public class ConsoleSession : Session
    {
        public ConsoleSession()
        {
            Controller = new ConsoleController { Session = this };
            Viewer = new ConsoleViewer();

            Controller.TakeInput();
        }
    }

    class ConsoleSubjectSession : ConsoleSession
    {
        public void AddFavourite() // tady spis ne, nebo ne?
        {

        }
    }
}
