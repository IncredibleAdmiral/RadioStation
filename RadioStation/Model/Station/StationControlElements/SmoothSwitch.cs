namespace RadioStation.Model.Station.StationControlElements
{
    class SmoothSwitch : Switch
    {
        int minValue;
        int stepValue;
        int value;
        int maxValue;
        bool firstUse;

        public SmoothSwitch(string name, int stepValue, int minValue, int maxValue) : base(name)
        {
            firstUse = true;
            this.minValue = minValue;
            this.stepValue = stepValue;
            this.maxValue = maxValue;
            value = minValue;
        }

        public override void Reset()
        {
            firstUse = true;
            value = minValue;
        }
        public override int ChangeSwitchPosition(ChangeDirection changeDirection)
        {
            if (changeDirection == ChangeDirection.Up)
            {
                if (value + 1 < maxValue)
                {
                    if (firstUse) { firstUse = false; return minValue; }
                    else
                    {
                        value += stepValue;
                        return stepValue;
                    }
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                if (value - 1 < minValue)
                {
                    if (firstUse)
                    {
                        firstUse = false;
                        return minValue;
                    }
                    return 0;
                }
                else
                {
                    value -= stepValue;
                    return -stepValue;
                }

            }
        }
    }
}
