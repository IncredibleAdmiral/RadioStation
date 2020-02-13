namespace RadioStation.Model.Station.StationControlElements
{
    class FixingButton : TwoPositionsElement
    {
        public FixingButton(string name, bool defaultStatus) : base(name, defaultStatus)
        {
            position = defaultStatus;
        }
    }
}
