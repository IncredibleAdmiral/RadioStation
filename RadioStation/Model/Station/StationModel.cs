using RadioStation.Model.Station.StationBlock;
using RadioStation.Model.Station.StationControlElements;
using System.Collections.Generic;
using System.Linq;

namespace RadioStation.Model.Station
{
    class StationModel
    {
       static  List<Block> blocksList = new List<Block>();

        static public void ResetAllToDefault()
        {
            for (int i = 0; i < blocksList.Count(); i++)
            {
                var blockElements = blocksList[i].elementsList.elements;
                for (int j = 0; j < blockElements.Count(); j++)
                {
                    blockElements[j].Reset();
                }
            }
        }
        static public Block GetStationBlock(string blockName)
        {
            return blocksList.Single(x => x.name == blockName);
        }


        static public string FrequencyCount(TransmitterOrReciever kindOfBlock)
        {
                 return blocksList.Single(x=> x.name == kindOfBlock.ToString()).elementsList.elements.Select(x=> x as PositionalSwitch).
                Aggregate("", (acc, x) => acc + x.conditionList[x.currentPosition].value );
                 
        }

        public static void InitStationsBlocks()
        {
            InitAmplifier();
            InitB328Transmitter();
            InitB328Receiver();
            InitLeftPNR();
            InitReceiver();
            InitRightPNR();
            InitTPP13H();
            InitTPP55();
            TransmitterFreqInit();
            ReceiverFreqInit();
            InitTransmitter();         
        }

        static void InitAmplifier()
        {
            blocksList.Add(new Block("Amplifier",

                new FixingButton("armOff", false),

                new FixingButton("check", false)));
        }

        static void InitB328Transmitter()
        {
            blocksList.Add(new Block("B328Transmitter",

                new PositionalSwitch("Voltage", new List<ValueAndPosition>(){ new ValueAndPosition("off", 0), new ValueAndPosition("6.38VCT", 35), new ValueAndPosition("-12.6VCT",60) ,
            new ValueAndPosition("+12.6VCT",90), new ValueAndPosition("-27VCT",110), new ValueAndPosition("-27VCONTR",140), new ValueAndPosition("Power",180), new ValueAndPosition("-27V",222),
            new ValueAndPosition("+60V",248), new ValueAndPosition("-60V", 270), new ValueAndPosition("+-60V", 297), new ValueAndPosition("+-60V|", 322)
             })

             , new Toogle("power", false)));
        }

        static void InitB328Receiver()
        {
            blocksList.Add(new Block("B328Receiver",

                new PositionalSwitch("Voltage", new List<ValueAndPosition>(){ new ValueAndPosition("off", 0), new ValueAndPosition("6.38VCT", 35), new ValueAndPosition("-12.6VCT",60) ,
            new ValueAndPosition("+12.6VCT",90), new ValueAndPosition("-27VCT",110), new ValueAndPosition("-27VCONTR",140), new ValueAndPosition("Power",180), new ValueAndPosition("-27V",222),
            new ValueAndPosition("+60V",248), new ValueAndPosition("-60V", 270), new ValueAndPosition("+-60V", 297), new ValueAndPosition("+-60V|", 322)
             })

             , new Toogle("power", false)));
        }

        static void InitLeftPNR()
        {
            blocksList.Add(new Block("LeftPNR", new PositionalSwitch("l", new List<ValueAndPosition>() { new ValueAndPosition("BP", 140), new ValueAndPosition("Inform", 180), new ValueAndPosition("UMFAP", 215) }),
            // Свитчи левого ПНР
            new PositionalSwitch("ll", new List<ValueAndPosition>() {new ValueAndPosition("off",30), new ValueAndPosition("1",52), new ValueAndPosition("2",90)
        , new ValueAndPosition("3",120), new ValueAndPosition("4",150), new ValueAndPosition("5",180), new ValueAndPosition("6",210), new ValueAndPosition("7",245), new ValueAndPosition("8",270)
        , new ValueAndPosition("9",295), new ValueAndPosition("10",345)}),

            new PositionalSwitch("tChCSelected", new List<ValueAndPosition>() { new ValueAndPosition("off",90), new ValueAndPosition("1200",120), new ValueAndPosition("1400",150)
        , new ValueAndPosition("1600",180), new ValueAndPosition("1800",210), new ValueAndPosition("2000",240), new ValueAndPosition("2300",270)}),

            new SmoothSwitch("level", 1, 110, 250),
            // кнопки левого ПНР   
            new Toogle("IP", false),
            new Toogle("PSh", false),
            new FixingButton("pps2k", false),
            new FixingButton("sl1", false),
            new FixingButton("sl2", false),
            new FixingButton("dop", false),
            new FixingButton("rs", false)
                ));
        }
        static void InitReceiver()
        {
            blocksList.Add(new Block("Receiver",
                new PositionalSwitch("kindofcontrol", new List<ValueAndPosition>() { new ValueAndPosition("mest", 137), new ValueAndPosition("md", 180), new ValueAndPosition("dist", 230) }),

               new PositionalSwitch("listeningReception", new List<ValueAndPosition>() {new ValueAndPosition("off",58), new ValueAndPosition("a1-u",92), new ValueAndPosition("a1-sh",120),
                   new ValueAndPosition("a3",152), new ValueAndPosition("f3",180), new ValueAndPosition("a1",220), new ValueAndPosition("b1",245), new ValueAndPosition("f1-chanel",270),
                   new ValueAndPosition("f2-chanel",305)}),

                new PositionalSwitch("kindofrlu", new List<ValueAndPosition>() { new ValueAndPosition("rru",132), new ValueAndPosition("01",164), new ValueAndPosition("10",202),
                    new ValueAndPosition("50",232) }),

               new PositionalSwitch("kindOfWorkTLG", new List<ValueAndPosition>() {new ValueAndPosition("f1-125",51), new ValueAndPosition("f1-200",80), new ValueAndPosition("f1-500",113),
                    new ValueAndPosition("f-1000",140), new ValueAndPosition("f-6000",160), new ValueAndPosition("f6-200",210), new ValueAndPosition("f6-500",225), new ValueAndPosition("f6-1000",240),
                   new ValueAndPosition("f9-300",280),new ValueAndPosition("f9-500",310)}),

               new PositionalSwitch("rodOFwork", new List<ValueAndPosition>() { new ValueAndPosition("TLG", 140), new ValueAndPosition("off", 180), new ValueAndPosition("TLF", 225) }),

                new PositionalSwitch("kindOfWorkTLF", new List<ValueAndPosition>() { new ValueAndPosition("A3H-A1",66), new ValueAndPosition("A3A-A",102), new ValueAndPosition("A3J-A1",135),
                    new ValueAndPosition("A3H-B1",158) , new ValueAndPosition("A3A-B1",214), new ValueAndPosition("A31-B1",238),
                    new ValueAndPosition("AZV-WEAKENED",265), new ValueAndPosition("AZV-SUPPRESSED",295)}),

                new SmoothSwitch("tone", 1, 126, 240),
                new SmoothSwitch("a1a2f3", 1, 126, 240),
                new SmoothSwitch("b1", 1, 126, 240),
                new SmoothSwitch("a1", 1, 126, 240),
                new PositionalSwitch("throughCircuit", new List<ValueAndPosition>() { new ValueAndPosition("gsh",101), new ValueAndPosition("noise",133),
                    new ValueAndPosition("work",160),new ValueAndPosition("harm",197), new ValueAndPosition("tone",230), new ValueAndPosition("tchk",258)}),
                new PositionalSwitch("attenD6", new List<ValueAndPosition>() {new ValueAndPosition("0",138), new ValueAndPosition("-10",163), new ValueAndPosition("-20",197),
                    new ValueAndPosition("-30",225)}),
                new PositionalSwitch("control", new List<ValueAndPosition>() {new ValueAndPosition("power",41), new ValueAndPosition("og",55), new ValueAndPosition("sint",70),
                    new ValueAndPosition("get1",83), new ValueAndPosition("get2",100), new ValueAndPosition("get3",115), new ValueAndPosition("mn",132), new ValueAndPosition("oprch",148),
                new ValueAndPosition("rch",164), new ValueAndPosition("pch",180), new ValueAndPosition("frequencyMatching",196),new ValueAndPosition("linea1a2f3",212),
                    new ValueAndPosition("linea1",228),new ValueAndPosition("lineb1",241), new ValueAndPosition("linef1chanel",258), new ValueAndPosition("linef2chanel",270),
                    new ValueAndPosition("lineadapt",289),new ValueAndPosition("current1chanel",304),new ValueAndPosition("current2chanel",319) }),
                new Toogle("og", false),
                new Toogle("power", false)
                ));
        }

        private static void InitRightPNR()
        {
            blocksList.Add(new Block("RightPNR",

                new PositionalSwitch("tg1", new List<ValueAndPosition>() {new ValueAndPosition("off",131), new ValueAndPosition("pnr",157)
                ,new ValueAndPosition("pro",192),new ValueAndPosition("out",225)}),

                new PositionalSwitch("tg2", new List<ValueAndPosition>() {new ValueAndPosition("off",131), new ValueAndPosition("pnr",157)
                ,new ValueAndPosition("pro",192),new ValueAndPosition("out",225)}),

                new PositionalSwitch("a1", new List<ValueAndPosition>() {new ValueAndPosition("off",65), new ValueAndPosition("pnr",95),
                    new ValueAndPosition("pk",130),new ValueAndPosition("sa",164),new ValueAndPosition("out1",200),new ValueAndPosition("out2",233),
                    new ValueAndPosition("additionally",264),new ValueAndPosition("P-321",292)}),

                new PositionalSwitch("b1", new List<ValueAndPosition>() {new ValueAndPosition("off",65), new ValueAndPosition("pnr",95),
                    new ValueAndPosition("pk",130),new ValueAndPosition("sa",164),new ValueAndPosition("out1",200),new ValueAndPosition("out2",233),
                    new ValueAndPosition("additionally",264),new ValueAndPosition("P-321",292) }),

                new PositionalSwitch("ca", new List<ValueAndPosition>() {new ValueAndPosition("off",131), new ValueAndPosition("kontrpnr",157)
                ,new ValueAndPosition("pnr",192),new ValueAndPosition("pk",225) }),

                new PositionalSwitch("transmissionWaves", new List<ValueAndPosition>() { new ValueAndPosition("1",43), new ValueAndPosition("2",77), new ValueAndPosition("3",110),
                new ValueAndPosition("4",140),new ValueAndPosition("5",166),new ValueAndPosition("6",196),new ValueAndPosition("7",223),new ValueAndPosition("8",253),new ValueAndPosition("9",278)
                ,new ValueAndPosition("10",310)}),

                new PositionalSwitch("receptionWaves", new List<ValueAndPosition>() { new ValueAndPosition("1",43), new ValueAndPosition("2",77), new ValueAndPosition("3",110),
                new ValueAndPosition("4",140),new ValueAndPosition("5",166),new ValueAndPosition("6",196),new ValueAndPosition("7",223),new ValueAndPosition("8",253),new ValueAndPosition("9",278)
                ,new ValueAndPosition("10",310)}),

                new PositionalSwitch("kindOfMangement", new List<ValueAndPosition>() { new ValueAndPosition("local",150), new ValueAndPosition("localadapt",180),
                    new ValueAndPosition("dist",210)}),

                new PositionalSwitch("tutc", new List<ValueAndPosition>() { new ValueAndPosition("tch", 167), new ValueAndPosition("tg", 197) }),

                new PositionalSwitch("regime", new List<ValueAndPosition>() { new ValueAndPosition("dupl", 147), new ValueAndPosition("simpl2pr", 180), new ValueAndPosition("simpl4pr", 209) }),

                 new PositionalSwitch("previous", new List<ValueAndPosition>() { new ValueAndPosition("0",0), new ValueAndPosition("1",27), new ValueAndPosition("2",57),
                 new ValueAndPosition("3",88),new ValueAndPosition("4",120),new ValueAndPosition("5",152),new ValueAndPosition("6",180),new ValueAndPosition("7",210),new ValueAndPosition("8",235)
                 ,new ValueAndPosition("9",270),new ValueAndPosition("10",298),new ValueAndPosition("11",330)}),

                 new PositionalSwitch("approximately", new List<ValueAndPosition>() {new ValueAndPosition("0",70), new ValueAndPosition("1",100),
                 new ValueAndPosition("2",133),new ValueAndPosition("3",163),new ValueAndPosition("4",195),new ValueAndPosition("5",225),new ValueAndPosition("6",255),new ValueAndPosition("7",290)}),

                new PositionalSwitch("precisely", new List<ValueAndPosition>() { new ValueAndPosition("0",70), new ValueAndPosition("1",100),
                 new ValueAndPosition("2",133),new ValueAndPosition("3",163),new ValueAndPosition("4",195),new ValueAndPosition("5",225),new ValueAndPosition("6",255),new ValueAndPosition("7",290)}),

                new SmoothSwitch("volume", 3, 116, 238),

                new Toogle("pr", false),

                new Toogle("ukv", false),

                new Toogle("azi", false),

                new Toogle("gr", false),

                new FixingButton("BoardNetwork", false),

                new FixingButton("heightMan", false),

                new FixingButton("nakal2Man", false),

                new FixingButton("PowerMan", false),

                new FixingButton("transmUKVpiece", false),

                new FixingButton("transmKVpiece", false),

                new FixingButton("transmUKVLPA", false),

                new FixingButton("transmKVfap", false),

                new FixingButton("transmKVAlpha", false),

                new FixingButton("transmKVD", false),

                new FixingButton("transmKVV", false),

                new FixingButton("setSimplUKV", false),

                new FixingButton("setSimplKV", false),

                new FixingButton("receptUKVPiece", false),

                new FixingButton("receptUKVLPA", false),

                new FixingButton("receptUKVSHDA", false),

                new FixingButton("receptKVPiece", false),

                new FixingButton("receptKVAlpha", false),

                new FixingButton("receptKVT13", false),

                new FixingButton("receptKVt40", false),

                 new FixingButton("receptKVd40", false),

                new FixingButton("receptKVazi", false),

                new FixingButton("receptKVV", false),

                new FixingButton("receptKVd13", false),

                new FixingButton("positionvert", false),

                new FixingButton("positionnakl", false),

                new FixingButton("positionTransp", false),

                new FixingButton("voicesluhcontr", false),

                new FixingButton("fapsettingssettings", false)));
        }

        static void InitTPP13H()
        {
            blocksList.Add(new Block("TPP13H",
                new PositionalSwitch("voltage", new List<ValueAndPosition>() {new ValueAndPosition("+5V",31), new ValueAndPosition("-9V",61),
                new ValueAndPosition("~6.3V",86),new ValueAndPosition("~12.6V",113), new ValueAndPosition("+27V",145), new ValueAndPosition("-150V",180), new ValueAndPosition("+150V",220),
                new ValueAndPosition("+300V",247),new ValueAndPosition("±320V",270), new ValueAndPosition("600V",300), new ValueAndPosition("2000V",330)}),
            new FixingButton("power", false),
            new Toogle("hightOn", false)
                ));
        }

        static void InitTPP55()
        {
            blocksList.Add(new Block("TPP55", new SmoothSwitch("settings", 1, 116, 238), new SmoothSwitch("connection", 1, 116, 238))
                );
        }

        static void InitTransmitter()
        {
            blocksList.Add(new Block("Transmitter",

                new PositionalSwitch("Management", new List<ValueAndPosition>() {new ValueAndPosition("Mestn",153),new ValueAndPosition("Dist",180),
            new ValueAndPosition("MD",213)}),

            new PositionalSwitch("kindOfWork", new List<ValueAndPosition>() {new ValueAndPosition("1ktf",135),new ValueAndPosition("2ktf",156),
                new ValueAndPosition("tg",196), new ValueAndPosition("outInfo",225)}),

                new PositionalSwitch("tfwork", new List<ValueAndPosition>() {new ValueAndPosition("nes",50),
                    new ValueAndPosition("azn-f1",78),new ValueAndPosition("aza-a1",107), new ValueAndPosition("f3j-a1",128),new ValueAndPosition("f3h-b1",147), new ValueAndPosition("aza-v1",202),
                    new ValueAndPosition("a3j-b1",230),new ValueAndPosition("azvoslabl",263),new ValueAndPosition("azvpodavl",292),new ValueAndPosition("f3",315)}),

                new PositionalSwitch("tgwork", new List<ValueAndPosition>() {new ValueAndPosition("a1",17),new ValueAndPosition("f1-125",49),new ValueAndPosition("f1-200",71),
                    new ValueAndPosition("f1-250",107),new ValueAndPosition("f1-500",126),new ValueAndPosition("f1-1000",151),new ValueAndPosition("f1-6000",210),
                    new ValueAndPosition("f6-125",230),new ValueAndPosition("f6-2000",250),new ValueAndPosition("f6-500",282),new ValueAndPosition("f6-1000",307),new ValueAndPosition("f9",335)}),

                new PositionalSwitch("control", new List<ValueAndPosition>() {new ValueAndPosition("pit",55), new ValueAndPosition("og",72), new ValueAndPosition("correctionog",86)
                    , new ValueAndPosition("tg",104), new ValueAndPosition("tf",125), new ValueAndPosition("sint",142), new ValueAndPosition("get1",158), new ValueAndPosition("get3",172)
                    , new ValueAndPosition("get3",186), new ValueAndPosition("pch",202), new ValueAndPosition("urch",218), new ValueAndPosition("linea1",234), new ValueAndPosition("linev2",253)
                    , new ValueAndPosition("line1k",274), new ValueAndPosition("line2k",290), new ValueAndPosition("ustr",306)}),
                    new Toogle("power", false)
                ));
        }

        static void ReceiverFreqInit()
        {
            blocksList.Add(new Block("ReceiverFreq", new PositionalSwitch("first", new List<ValueAndPosition>() { new ValueAndPosition("1",65),new ValueAndPosition("2",98)
                ,new ValueAndPosition("3",131),new ValueAndPosition("4",163),new ValueAndPosition("5",195)}),

                new PositionalSwitch("second", new List<ValueAndPosition>() { new ValueAndPosition("1",65),new ValueAndPosition("2",98)
                ,new ValueAndPosition("3",131),new ValueAndPosition("4",163),new ValueAndPosition("5",195),
                    new ValueAndPosition("6",230),new ValueAndPosition("7",262),new ValueAndPosition("8",295)
                ,new ValueAndPosition("9",320)}),

                new PositionalSwitch("third", new List<ValueAndPosition>() { new ValueAndPosition("1",65),new ValueAndPosition("2",98)
                ,new ValueAndPosition("3",131),new ValueAndPosition("4",163),new ValueAndPosition("5",195),
                    new ValueAndPosition("6",230),new ValueAndPosition("7",262),new ValueAndPosition("8",295)
                ,new ValueAndPosition("9",320)}),

                new PositionalSwitch("fourth", new List<ValueAndPosition>() { new ValueAndPosition("1",65),new ValueAndPosition("2",98)
                ,new ValueAndPosition("3",131),new ValueAndPosition("4",163),new ValueAndPosition("5",195),
                    new ValueAndPosition("6",230),new ValueAndPosition("7",262),new ValueAndPosition("8",295)
                ,new ValueAndPosition("9",320)}),

                new PositionalSwitch("fifth", new List<ValueAndPosition>() { new ValueAndPosition("1",65),new ValueAndPosition("2",98)
                ,new ValueAndPosition("3",131),new ValueAndPosition("4",163),new ValueAndPosition("5",195),
                    new ValueAndPosition("6",230),new ValueAndPosition("7",262),new ValueAndPosition("8",295)
                ,new ValueAndPosition("9",320)}),

                new PositionalSwitch("sixth", new List<ValueAndPosition>() { new ValueAndPosition("1",65),new ValueAndPosition("2",98)
                ,new ValueAndPosition("3",131),new ValueAndPosition("4",163),new ValueAndPosition("5",195),
                    new ValueAndPosition("6",230),new ValueAndPosition("7",262),new ValueAndPosition("8",295)
                ,new ValueAndPosition("9",320)}),

                new PositionalSwitch("seventh", new List<ValueAndPosition>() { new ValueAndPosition("1",65),new ValueAndPosition("2",98)
                ,new ValueAndPosition("3",131),new ValueAndPosition("4",163),new ValueAndPosition("5",195),
                    new ValueAndPosition("6",230),new ValueAndPosition("7",262),new ValueAndPosition("8",295)
                ,new ValueAndPosition("9",320)})));         
       }
        static void TransmitterFreqInit()
        {
            blocksList.Add(new Block("TransmitterFreq", new PositionalSwitch("first", new List<ValueAndPosition>() { new ValueAndPosition("1",65),new ValueAndPosition("2",98)
                ,new ValueAndPosition("3",131),new ValueAndPosition("4",163),new ValueAndPosition("5",195)}),

               new PositionalSwitch("second", new List<ValueAndPosition>() { new ValueAndPosition("1",65),new ValueAndPosition("2",98)
                ,new ValueAndPosition("3",131),new ValueAndPosition("4",163),new ValueAndPosition("5",195),
                    new ValueAndPosition("6",230),new ValueAndPosition("7",262),new ValueAndPosition("8",295)
                ,new ValueAndPosition("9",320)}),

               new PositionalSwitch("third", new List<ValueAndPosition>() { new ValueAndPosition("1",65),new ValueAndPosition("2",98)
                ,new ValueAndPosition("3",131),new ValueAndPosition("4",163),new ValueAndPosition("5",195),
                    new ValueAndPosition("6",230),new ValueAndPosition("7",262),new ValueAndPosition("8",295)
                ,new ValueAndPosition("9",320)}),

               new PositionalSwitch("fourth", new List<ValueAndPosition>() { new ValueAndPosition("1",65),new ValueAndPosition("2",98)
                ,new ValueAndPosition("3",131),new ValueAndPosition("4",163),new ValueAndPosition("5",195),
                    new ValueAndPosition("6",230),new ValueAndPosition("7",262),new ValueAndPosition("8",295)
                ,new ValueAndPosition("9",320)}),

               new PositionalSwitch("fifth", new List<ValueAndPosition>() { new ValueAndPosition("1",65),new ValueAndPosition("2",98)
                ,new ValueAndPosition("3",131),new ValueAndPosition("4",163),new ValueAndPosition("5",195),
                    new ValueAndPosition("6",230),new ValueAndPosition("7",262),new ValueAndPosition("8",295)
                ,new ValueAndPosition("9",320)}),

               new PositionalSwitch("sixth", new List<ValueAndPosition>() { new ValueAndPosition("1",65),new ValueAndPosition("2",98)
                ,new ValueAndPosition("3",131),new ValueAndPosition("4",163),new ValueAndPosition("5",195),
                    new ValueAndPosition("6",230),new ValueAndPosition("7",262),new ValueAndPosition("8",295)
                ,new ValueAndPosition("9",320)}),

               new PositionalSwitch("seventh", new List<ValueAndPosition>() { new ValueAndPosition("1",65),new ValueAndPosition("2",98)
                ,new ValueAndPosition("3",131),new ValueAndPosition("4",163),new ValueAndPosition("5",195),
                    new ValueAndPosition("6",230),new ValueAndPosition("7",262),new ValueAndPosition("8",295)
                ,new ValueAndPosition("9",320)})));
        }
    }
}
