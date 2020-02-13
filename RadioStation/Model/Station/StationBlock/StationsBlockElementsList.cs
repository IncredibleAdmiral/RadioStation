using RadioStation.Model.Station.StationControlElements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RadioStation.Model.Station.StationBlock
{
    class StationsBlockElementsList
    {
        public List<StationElement> elements { get; private set; }

        public StationsBlockElementsList()
        {
            elements = new List<StationElement>();
        }

        public void Add(StationElement el)
        {
            var buf = elements.Where(x => x.name == el.name);
            if (buf.Count() == 0)
                elements.Add(el);
            else throw new Exception("NotUniqueName");
        }

        public StationElement GetItemByName(string name)
        {
            return elements.Single(x => x.name.ToLower() == name.ToLower());
        }
    }
}
