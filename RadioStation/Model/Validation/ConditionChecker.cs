using RadioStation.Model.Station;
using RadioStation.Model.Station.StationControlElements;
using RadioStation.Model.Validation.Templates;
using System.Collections.Generic;
using System.Linq;

namespace RadioStation.Model.Validation
{
   partial class ConditionChecker
    {

        static List<Template> scriptsList = new List<Template>();
        public static bool obligatory { get; private set; }

        static List<Template> GetOblygatoryScripts()
        {
            return scriptsList.Where(x => x.isThisScriptObligatory).ToList();
        }

        static public List<string> GetMistakes()
        {
            var mistakes = new List<string>();
            var obl = GetOblygatoryScripts().Where(x => x.isScriptCompCondition == false).ToList();

            if (obl.Count() > 0)
            {
                for (int i = 0; i < obl.Count(); i++)
                {
                    string mistake = "на блоке  ";
                    if (obl[i] is PositionalSwitchConditionTemplate)
                    {
                        var bufScr = obl[i] as PositionalSwitchConditionTemplate;
                        mistake += Translator.TranslateBlockName(bufScr.blockName) + " переключатель " + Translator.TranslateElementName(bufScr.elementName)
                            + " не переведен в позицию     " + Translator.TransletePositionName(bufScr.necessaryPosition) + "\n";
                        mistakes.Add(mistake);
                    }
                    else if (obl[i] is TwoPositionsElementTemplate)
                    {
                        var bufScr = obl[i] as TwoPositionsElementTemplate;
                        mistake += Translator.TranslateBlockName(bufScr.blockName) + "    " + Translator.TranslateElementName(bufScr.elementName) + "    Не включен" + "\n";
                        mistakes.Add(mistake);
                    }
                }
            }
            return mistakes;
        }

        static public void InitialCheck()
        {
            var obligatoryScripts = GetOblygatoryScripts();
            for (int i = 0; i < obligatoryScripts.Count(); i++)
            {
                var blockBuff = StationModel.GetStationBlock(obligatoryScripts[i].blockName);
                var elementBuf = blockBuff.elementsList.GetItemByName(obligatoryScripts[i].elementName);
                CompareConditions(blockBuff.name, elementBuf.name, elementBuf);
            }
        }

        static private void AddMutually()
        {
            scriptsList.Add(new MutuallyExclusiveTemplate("RightPNR", "transmUKVpiece", true, "RightPNR", "receptUKVPiece"));
            scriptsList.Add(new MutuallyExclusiveTemplate("RightPNR", "transmUKVLPA", true, "RightPNR", "receptUKVLPA"));

        }

        static private void AddNessPostionSwitch()
        {
            //// позиции позиционных свитчей, которые должны быть установлены
            scriptsList.Add(new PositionalSwitchConditionTemplate("LeftPNR", "l", "BP", true));
            scriptsList.Add(new PositionalSwitchConditionTemplate("Transmitter", "tgwork", "a1", true));
            scriptsList.Add(new PositionalSwitchConditionTemplate("RightPNR", "a1", "pnr", true));
            scriptsList.Add(new PositionalSwitchConditionTemplate("Receiver", "rodOFwork", "TLF", true));
            scriptsList.Add(new PositionalSwitchConditionTemplate("Receiver", "kindofrlu", "rru", true));
            scriptsList.Add(new PositionalSwitchConditionTemplate("Receiver", "kindofcontrol", "mest", true));
            scriptsList.Add(new PositionalSwitchConditionTemplate("Receiver", "listeningReception", "a1-u", true));
            scriptsList.Add(new PositionalSwitchConditionTemplate("Receiver", "throughCircuit", "work", true));
            scriptsList.Add(new PositionalSwitchConditionTemplate("Transmitter", "Management", "Mestn", true));
            scriptsList.Add(new PositionalSwitchConditionTemplate("Transmitter", "kindOfWork", "1ktf", true));
            scriptsList.Add(new PositionalSwitchConditionTemplate("Transmitter", "control", "linea1", true));
            scriptsList.Add(new PositionalSwitchConditionTemplate("RightPNR", "ca", "off", true));
            scriptsList.Add(new PositionalSwitchConditionTemplate("RightPNR", "regime", "dupl", true));
            scriptsList.Add(new PositionalSwitchConditionTemplate("RightPNR", "kindOfMangement", "local", true));
        }

        static private void AddNessBuutonAndTooglePosition()
        {
            //нужное положение фиксированных кнопок и тумблеров
            scriptsList.Add(new TwoPositionsElementTemplate("B328Transmitter", "power", true, true));
            scriptsList.Add(new TwoPositionsElementTemplate("B328Receiver", "power", true, true));
            scriptsList.Add(new TwoPositionsElementTemplate("RightPNR", "PowerMan", true, true));
            scriptsList.Add(new TwoPositionsElementTemplate("Receiver", "og", true, true));
            scriptsList.Add(new TwoPositionsElementTemplate("Receiver", "power", true, true));
            scriptsList.Add(new TwoPositionsElementTemplate("RightPNR", "heightMan", true, true));
            //scriptsList.Add(new ToogleAndFixButtonScript("RightPNR", "heightMan",true));
            //scriptsList.Add(new ToogleAndFixButtonScript("TPP13H","высокое авар",true));
        }

        //static private void AddNessPositionSwitchValueUse()
        //{
        //    //позиции позиционных свитчей, которые должны быть заюзаны
        //    scriptsList.Add(new IsPostionalSwitchPositionUse("B328Transmitter", "Voltage", "+-60V|", false));
        //    scriptsList.Add(new IsPostionalSwitchPositionUse("B328Receiver", "Voltage", "+-60V|", false));
        //    scriptsList.Add(new IsPostionalSwitchPositionUse("LeftPNR", "ll", "8", false));
        //}

        static private void CheckAllScripts()
        {
            var obligatoryScripts = scriptsList.Where(x => x.isThisScriptObligatory == true).ToList();
            //var k = arentConformScripts.Where(x => x.isScriptCompCondition == false);
            var k = new List<Template>();
            for (int i = 0; i < obligatoryScripts.Count(); i++)
            {
                if (!obligatoryScripts[i].isScriptCompCondition)
                    k.Add(obligatoryScripts[i]);
            }

            if (k.Count() == 0)
            {
                obligatory = true;
            }
            else obligatory = false;
        }

        static public void InitScripts()
        {
            AddNessPostionSwitch();
            //AddNessPositionSwitchValueUse();
            AddNessBuutonAndTooglePosition();
            AddMutually();
            obligatory = false;
        }

        static public Template GetScriptByName(string blockName, string elementName)
        {
            return scriptsList.SingleOrDefault(x => x.blockName == blockName && x.elementName == elementName);
        }

        static public void CompareConditions(string blockName, string elementName, StationElement el)
        {
            var scr = GetScriptByName(blockName, elementName);
            if (scr == null)
            {
                var lst = scriptsList.Where(x => x is MutuallyExclusiveTemplate).Select(x => x as MutuallyExclusiveTemplate);

                scr = lst.SingleOrDefault(x => x.secondElementBlockName == blockName && x.secondElementName == elementName);
            }

            if (scr != null)
            {
                if (el is TwoPositionsElement)
                {
                    var element = el as TwoPositionsElement;
                    if (scr is TwoPositionsElementTemplate)
                    {
                        var script = scr as TwoPositionsElementTemplate;
                        if (element.position == script.nessPosition)
                        {
                            script.InstallScriptCondition(true);
                        }
                        else script.InstallScriptCondition(false);
                    }
                    else if (scr is MutuallyExclusiveTemplate)
                    {
                        var script = scr as MutuallyExclusiveTemplate;
                        var firstEl = StationModel.GetStationBlock(script.blockName).elementsList.GetItemByName(script.elementName) as TwoPositionsElement;
                        var secondEl = StationModel.GetStationBlock(script.secondElementBlockName).elementsList.GetItemByName(script.secondElementName) as TwoPositionsElement;
                        if (firstEl.position && secondEl.position)
                        {
                            script.InstallScriptCondition(false);
                        }
                        else
                        {
                            script.InstallScriptCondition(true);
                        }
                    }
                }
                //else

                //if (el is PositionalSwitchModel)
                //{
                //    var element = el as PositionalSwitchModel;

                //    if (scr is PositionalSwitchConditionTemplate)
                //    {
                //        var script = scr as PositionalSwitchConditionTemplate;
                //        if (script.necessaryPosition == element.conditionList[element.currentPosition].value)
                //        {
                //            script.InstallScriptCondition(true);
                //        }
                //        else script.InstallScriptCondition(false);
                //    }

                //    else if (scr is IsPostionalSwitchPositionUse)
                //    {
                //        var script = scr as IsPostionalSwitchPositionUse;

                //    }
                //}
                CheckAllScripts();
            }

        }
        static public void ResetScripts()
        {
            foreach (var i in scriptsList)
                i.Reset();
        }
    }
}
