namespace lab4_5.Models
{

    public interface ILoader
    {
        event EventHandler Loaded;
        BolidStatus ReturnCarIntoTheRace();
    }

    public enum BolidStatus
    {
        Normal,
        Crash,
        TiresWornOff
        
    }

    public class Bolid
    {
        private int maxDistance = 300;
        private int distanceNow = 0;
        private int distanceTires = 0;
        private int distanceInSecond = 10;
        
        private Mechanic mechanic = new Mechanic();
        private Loader loader = new Loader();
        private BolidStatus status = BolidStatus.Normal;
        
        public event EventHandler<int> DistanceCovered;
        public event EventHandler<BolidStatus> StatusChanged;

        private Random random = new Random();

        public void StartRace()
        {
            while (true)
            {
                
                OnDistanceCover(distanceNow);
                if (random.Next(0, 100) < 5)
                {
                    status = BolidStatus.Crash;
                    OnStatusChanged(status);
                    distanceInSecond = 0;
                    Thread.Sleep(4000);
                    status = loader.ReturnCarIntoTheRace();
                    OnStatusChanged(status);
                    distanceInSecond = 10;
                    continue;
                }
                
                if (distanceTires >= maxDistance/2)
                {
                    status = BolidStatus.TiresWornOff;
                    OnStatusChanged(status);
                    distanceInSecond = 0;
                    Thread.Sleep(2000);
                    status = mechanic.changeTires();
                    OnStatusChanged(status);
                    distanceInSecond = 10;
                    distanceTires = 0;
                }
                distanceNow += distanceInSecond;
                distanceTires += distanceInSecond;
                Thread.Sleep(1000);
            }
        }

        public int getDistanceCovered()
        {
            return distanceNow;
        }                

        protected virtual void OnDistanceCover(int oilVolume)
        {
            DistanceCovered?.Invoke(this, oilVolume);
        }

        protected virtual void OnStatusChanged(BolidStatus newStatus)
        {
            StatusChanged?.Invoke(this, newStatus);
        }
    }
}