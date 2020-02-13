using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStation.Model.Validation.Templates
{
    class ExceptSwitchTemplate : Template
    {
        string exeptPosition;
        public ExceptSwitchTemplate(string blockName, string elementName, bool isThisScriptObligatory, string exeptPosition) : base(blockName, elementName, isThisScriptObligatory)
        {
            this.exeptPosition = exeptPosition;
        }
    }
}
