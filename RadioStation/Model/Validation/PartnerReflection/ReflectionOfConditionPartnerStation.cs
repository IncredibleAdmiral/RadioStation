using System.Collections.Generic;
using System.Linq;

namespace RadioStation.Model.Validation.PartnerReflection
{
    class ReflectionOfConditionPartnerStation
    {
        static List<ReflectionConditons> elementConditonList;

        static string partnerTransmitterFrequence;

        static string partnerReceiverFrequence;

        public static void ElementListInit()
        {
            elementConditonList = new List<ReflectionConditons>() { new ReflectionConditons("Receiver", "listeningReception"), new ReflectionConditons("Receiver", "kindOfWorkTLF"),
            new ReflectionConditons("Transmitter","RodOfWork"),new ReflectionConditons("RightPNR","ukv")};
        }

        static  string[] PatseConditionString(string conditionString)
        {
            if (conditionString[0] == 'A')
            {
                conditionString = conditionString.Remove(0, 1);
            }
            return conditionString.Split(new char[] { '&','#','=' });
        }

        public static void SetCondition(string conditionString)
        {
            var splitConditionStr = PatseConditionString(conditionString);
            for (int i = 0; i < splitConditionStr.Length; i += 2)
            {

                var currentParameter = elementConditonList.SingleOrDefault(x => x.blockName == splitConditionStr[i] && x.elementName == splitConditionStr[i + 1]);
                if (currentParameter != null)
                {
                    currentParameter.positionName = splitConditionStr[i + 1];
                }
                else if (splitConditionStr[i] == "TransmitterFrequence")
                {
                    partnerTransmitterFrequence = splitConditionStr[i + 1];
                }
                else if (splitConditionStr[i] == "ReceiverFrequence")
                {
                    partnerReceiverFrequence = splitConditionStr[i + 1];
                };
            }          

        }
    }
}
