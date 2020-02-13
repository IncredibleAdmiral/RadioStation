using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStation.Model.Validation.Templates
{
    class MutuallyExclusiveTemplate : Template
    {
        public string secondElementBlockName { get; private set; }
        public string secondElementName { get; private set; }

        public MutuallyExclusiveTemplate(string blockName, string elementName, bool isThisScriptObligatory, string secondElementBlockName, string secondElementName) : base(blockName, elementName, isThisScriptObligatory)
        {
            this.secondElementBlockName = secondElementBlockName;
            this.secondElementName = secondElementName;

        }
    }
}
