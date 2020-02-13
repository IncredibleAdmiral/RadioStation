namespace RadioStation.Model.Station.StationControlElements
{
    class TwoPositionsElement : StationElement
    {
       public bool position { get; protected set; }
        protected TwoPositionsElement(string name, bool defoultPosition) : base(name)
        {
            position = defoultPosition;
        }

        public override void Reset()
        {
            position = false;
        }

        public void ReversePosition()
        {
            position = !position;
        }
    }
}
