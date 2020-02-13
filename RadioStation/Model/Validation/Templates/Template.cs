using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStation.Model.Validation.Templates
{
    class Template
    {
        public string blockName { get; private set; }
        public string elementName { get; private set; }
        public bool isScriptCompCondition { get; private set; }
        public bool isThisScriptObligatory { get; private set; }

        protected Template(string blockName, string elementName, bool isThisScriptObligatory)
        {
            this.blockName = blockName;
            this.elementName = elementName;
            isScriptCompCondition = false;
            this.isThisScriptObligatory = isThisScriptObligatory;
        }
        public void InstallScriptCondition(bool con)
        {
            isScriptCompCondition = con;
        }

        public void Reset()
        {
            isScriptCompCondition = false;
        }

    }
}
