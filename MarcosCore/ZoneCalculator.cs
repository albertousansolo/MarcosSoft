using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcosCore
{
    public sealed class ZoneCalculator
    {
        public class ZoneCalculatorModel
        {
            public string Description { get; set; }
            public int FTPNetInWatts { get; set; } = 330;
            public int HBAvg { get; set; } = 172;
            public double Weight { get; set; } = 71;


            public int ZoneFatMax { get; set; }
            public string ZoneFatMaxStr => $"Frecuencia cardíaca deseable para consumo máximo de grasa {ZoneFatMax}";
            public int ZoneFHZona { get; set; }
            public string ZoneFHZonaStr => $"Frecuencia cardíaca deseable para trabajo de Base {ZoneFHZona}";

            public List<ZoneRange> ZoneRangesPower { get; set; }
            public List<ZoneRange> ZoneRangesHR { get; set; }
        }

        public class ZoneRange
        {
            public int From { get; set; }
            public int To { get; set; }
            public int FromPer { get; set; }
            public int ToPer { get; set; }
            public string Description { get; set; }
           
        }

        public void Calculate(ZoneCalculatorModel zoneCalculatorModel)
        {
            try
            {
                zoneCalculatorModel.ZoneRangesPower = new List<ZoneRange>();
                zoneCalculatorModel.ZoneRangesPower.Add(new ZoneRange() { Description = "Z1", FromPer = 0, ToPer = 54 });
                zoneCalculatorModel.ZoneRangesPower.Add(new ZoneRange() { Description = "Z2", FromPer = 55, ToPer = 75 });
                zoneCalculatorModel.ZoneRangesPower.Add(new ZoneRange() { Description = "Z3", FromPer = 76, ToPer = 90 });
                zoneCalculatorModel.ZoneRangesPower.Add(new ZoneRange() { Description = "Sweet Spot", FromPer = 88, ToPer = 93 });
                zoneCalculatorModel.ZoneRangesPower.Add(new ZoneRange() { Description = "Z4", FromPer = 91, ToPer = 105 });
                zoneCalculatorModel.ZoneRangesPower.Add(new ZoneRange() { Description = "Z5", FromPer = 106, ToPer = 120 });
                zoneCalculatorModel.ZoneRangesPower.Add(new ZoneRange() { Description = "Z6", FromPer = 121, ToPer = 150 });
                zoneCalculatorModel.ZoneRangesPower.Add(new ZoneRange() { Description = "Z7", FromPer = 150, ToPer = 300 });
                foreach (var item in zoneCalculatorModel.ZoneRangesPower)
                {
                    item.From = zoneCalculatorModel.FTPNetInWatts * item.FromPer / 100;
                    item.To = zoneCalculatorModel.FTPNetInWatts * item.ToPer / 100;
                }


                zoneCalculatorModel.ZoneRangesHR = new List<ZoneRange>();
                zoneCalculatorModel.ZoneRangesHR.Add(new ZoneRange() { Description = "Z1", FromPer = 0, ToPer = 68 });
                zoneCalculatorModel.ZoneRangesHR.Add(new ZoneRange() { Description = "Z2", FromPer = 69, ToPer = 83 });
                zoneCalculatorModel.ZoneRangesHR.Add(new ZoneRange() { Description = "Z3", FromPer = 84, ToPer = 94 });
                zoneCalculatorModel.ZoneRangesHR.Add(new ZoneRange() { Description = "Z4", FromPer = 95, ToPer = 105 });
                zoneCalculatorModel.ZoneRangesHR.Add(new ZoneRange() { Description = "Z5", FromPer = 106, ToPer = 150 });
                foreach (var item in zoneCalculatorModel.ZoneRangesHR)
                {
                    item.From = zoneCalculatorModel.HBAvg * item.FromPer / 100;
                    item.To = zoneCalculatorModel.HBAvg * item.ToPer / 100;
                }
                zoneCalculatorModel.ZoneFatMax =(int)( zoneCalculatorModel.HBAvg * 0.65);
                zoneCalculatorModel.ZoneFHZona =(int)( zoneCalculatorModel.HBAvg * 0.8);
                zoneCalculatorModel.Description = $"Cálculo Ok";
            }
            catch (Exception exc)
            {
                zoneCalculatorModel.Description = $"Error en el cálculo, revisa los datos {exc}";
            }
        }
    }
}
