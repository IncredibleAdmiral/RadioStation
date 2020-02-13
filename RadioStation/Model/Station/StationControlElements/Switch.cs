namespace RadioStation.Model.Station.StationControlElements
{
   abstract class Switch : StationElement
    {
        protected Switch(string name) : base(name)
        {
        }

        public abstract int ChangeSwitchPosition(ChangeDirection changeDirection);
    }
}
