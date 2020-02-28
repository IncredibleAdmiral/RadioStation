using RadioStation.Model.Station;
using RadioStation.Model.Station.StationControlElements;

namespace RadioStation.Model.Validation
{
    class ConditionStringMaker
    {
        static string GetConditionString()
        {
            var listeningReceptionSwitch = StationModel.GetStationBlock("Receiver").elementsList.GetItemByName("listeningReception") as PositionalSwitch;

            var receiverkindOfWorkTLFSwitch = StationModel.GetStationBlock("Receiver").elementsList.GetItemByName("kindOfWorkTLF") as PositionalSwitch;

            var transmitterRodOfWork = StationModel.GetStationBlock("Transmitter").elementsList.GetItemByName("kindOfWork") as PositionalSwitch;

            var ukvRightPNR = StationModel.GetStationBlock("RightPNR").elementsList.GetItemByName("ukv") as Toogle;

             return "&" + "Receiver#listeningReception" + "=" + listeningReceptionSwitch.conditionList[listeningReceptionSwitch.currentPosition].value +

                    "&" + "Receiver#kindOfWorkTLF" + "=" + receiverkindOfWorkTLFSwitch.conditionList[receiverkindOfWorkTLFSwitch.currentPosition].value +

                    "&" + "Transmitter#RodOfWork" + "=" + transmitterRodOfWork.conditionList[transmitterRodOfWork.currentPosition].value +

                    "&" + "ukv#RightPNR" + "=" + ukvRightPNR.position.ToString() + 

                    "&" + "TransmitterFrequence" + "=" + StationModel.FrequencyCount(TransmitterOrReciever.TransmitterFreq) +

                    "&" + "ReceiverFrequence" + "=" + StationModel.FrequencyCount(TransmitterOrReciever.RecieverFreq);
        }
    }
}
