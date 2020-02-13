namespace RadioStation.Model.Validation
{
    class Translator
    {
        static public string TranslateBlockName(string blockNameInEnglish)
        {
            switch (blockNameInEnglish)
            {
                case "B328Transmitter": return " питания передатчика";
                case "B328Receiver": return " питаня приемника";
                case "LeftPNR": return " левое ПНР";
                case "Receiver": return " приемника";
                case "RightPNR": return " Правого ПНР";
                case "TPP13H": return " ТПП 13";
                case "Transmitter": return " Передатчика";
                case "Amplifier": return " усилителя";
            }
            return blockNameInEnglish;
        }

        static public string TranslateElementName(string elmentNameInEnglish)
        {
            switch (elmentNameInEnglish)
            {
                case "l": return elmentNameInEnglish;
                case "tgwork": return "Работа ТГ";
                case "a1": return "A1";
                case "rodOFwork": return "Род Работы";
                case "kindofrlu": return "Вид РЛУ";
                case "kindofcontrol": return "Вид Контроля";
                case "listeningReception": return "Слух Прием";
                case "throughCircuit": return "Сквозной Контроль";
                case "Management": return "Управления";
                case "kindOfWork": return "Вид Работы";
                case "control": return "контроль";
                case "ca": return "CA";
                case "regime": return "Режим";
                case "kindOfManagement": return "Вид Управления";
                case "power": return "Питание";
                case "PowerMan": return "Управление Питание";
                case "og": return "ОГ";
                case "heightMan": return "Управление Высокое";
            }

            return elmentNameInEnglish;
        }

        static public string TransletePositionName(string positionNameInEnglish)
        {
            switch (positionNameInEnglish)
            {
                case "BP": return "БП";
                case "pnr": return "пнр";
                case "TLF": return "ТЛФ";
                case "rru": return "РРУ";
                case "mest": return "местн";
                case "a1-u": return "А1-у";
                case "work": return "Работа";
                case "Mestn": return "Местн";
                case "1ktf": return "1 КТФ";
                case "linea1": return "Линия A1";
            }
            return positionNameInEnglish;
        }
    }
}
