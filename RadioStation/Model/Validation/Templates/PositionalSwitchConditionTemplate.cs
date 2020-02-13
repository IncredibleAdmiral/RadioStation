namespace RadioStation.Model.Validation.Templates
{
    class PositionalSwitchConditionTemplate : Template
    {
        public string necessaryPosition { get; private set; }

        public PositionalSwitchConditionTemplate(string blockName, string elementName, string necessaryPosition, bool obligatory) : base(blockName, elementName, obligatory)
        {
            this.necessaryPosition = necessaryPosition;
        }
    }
}
