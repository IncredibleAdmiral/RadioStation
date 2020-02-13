using System;


namespace RadioStation.Model.Station.StationControlElements
{
    class ValueAndPosition
    {
        public string value { get; private set; }
        public int degreeOfPosition { get; private set; }

        public DateTime WhenUsed;

        public ValueAndPosition(string value, int degreeOfPosition)
        {
            this.degreeOfPosition = degreeOfPosition;
            this.value = value;

        }
                public void TakeUsedTime()
        {
            WhenUsed = DateTime.Now;
        }
    }
}
