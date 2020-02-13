namespace RadioStation.Model.Station.StationControlElements
{
   abstract class StationElement 
    {
        public readonly string name;

        public abstract void Reset();

        protected StationElement(string name)
        {
            this.name = name;
        }
    }
}
