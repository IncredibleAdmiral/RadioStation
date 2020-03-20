namespace RadioStation.Model.Validation.PartnerReflection
{
    class ReflectionConditons
    {
  
     public  readonly string blockName;
     public  readonly string elementName;
     public           string positionName;

        public ReflectionConditons(string blockName, string elementName)
        {
            this.blockName = blockName;
            this.elementName = elementName;
        }
    }
    
}
