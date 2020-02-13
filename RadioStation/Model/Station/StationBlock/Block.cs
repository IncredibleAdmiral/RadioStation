using RadioStation.Model.Station.StationControlElements;
using System.Linq;

namespace RadioStation.Model.Station.StationBlock
{
    class Block
    {
        public readonly string name;
        public StationsBlockElementsList elementsList { get; private set; }

        public Block(string name, params StationElement[] addElemets)
        {
            elementsList = new StationsBlockElementsList();
            this.name = name;
            for (int i = 0; i < addElemets.Count(); i++)
                elementsList.Add(addElemets[i]);
        }
    }
}