namespace RadioStation.Model.Validation.Templates
{
    class TwoPositionsElementTemplate : Template
    {
        public bool nessPosition { get; private set; }

        public TwoPositionsElementTemplate(string blockName, string elementName, bool nessPosition, bool obligatory) : base(blockName, elementName, obligatory)
        {
            this.nessPosition = nessPosition;

        }
    }
}
