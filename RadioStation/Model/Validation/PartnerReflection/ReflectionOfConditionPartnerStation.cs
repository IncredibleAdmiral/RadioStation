using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        static public void PatseConditionString(string conditionString)
        {
            var conditionList = conditionString.Split(new char[] { '&', '=' });
        }

        public static void SetCondition()
        {
            var elementConditonList.
        }
    }
}
