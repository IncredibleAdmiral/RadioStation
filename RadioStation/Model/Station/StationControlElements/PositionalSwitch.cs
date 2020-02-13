using System.Collections.Generic;
using System.Linq;


namespace RadioStation.Model.Station.StationControlElements
{
    class PositionalSwitch : Switch
    {
        public int currentPosition { get; private set; }
        int firstPositionAngle;
        bool isItfirstUse;
      
        public List<ValueAndPosition> conditionList { get; private set; }
        public override void Reset()
        {
            currentPosition = 0;
            isItfirstUse = true;
        }
        public PositionalSwitch(string name, List<ValueAndPosition> list) : base(name)
        {
            isItfirstUse = true;

            conditionList = list;
            currentPosition = 0;

            if (!(conditionList[0].degreeOfPosition == 0))
            {
                firstPositionAngle = conditionList[0].degreeOfPosition;
                var buf = conditionList.Select(x => x.degreeOfPosition - firstPositionAngle);
            }
        }

        public ValueAndPosition GetValueAndPositionByName(string postionName)
        {
            return conditionList.Single(x => x.value == postionName);
        }

        public override int ChangeSwitchPosition(ChangeDirection changeDirection)
        {
            int bufAngle;

            if (changeDirection == ChangeDirection.Up)
            {
                if (currentPosition + 1 == conditionList.Count) { return 0; }
                else
                {
                    bufAngle = conditionList[currentPosition + 1].degreeOfPosition - conditionList[currentPosition].degreeOfPosition;
                    currentPosition += 1;
                    conditionList[currentPosition].TakeUsedTime();
                    if (isItfirstUse)
                    {
                        isItfirstUse = false;
                        return bufAngle + firstPositionAngle;
                    }
                    else { return bufAngle; }
                }
            }
            else if (currentPosition - 1 < 0) { if (isItfirstUse) { isItfirstUse = false; return firstPositionAngle; } else return 0; }
            {
                bufAngle = conditionList[currentPosition - 1].degreeOfPosition - conditionList[currentPosition].degreeOfPosition;
                currentPosition -= 1;
                return bufAngle;
            }

        }
    }
}
