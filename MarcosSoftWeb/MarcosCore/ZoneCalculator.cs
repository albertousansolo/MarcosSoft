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
            public int FTPNetInWatts { get; set; } = 325;
            public int HBAvg { get; set; } = 172;
            public decimal Weight { get; set; } = 72.5M;


            public int ZoneFatMax { get; set; }
            public string ZoneFatMaxStr { get; set; }
            public int ZoneFHZona { get; set; }
            public string ZoneFHZonaStr { get; set; }

            public List<ZoneRange> ZoneRangesPower { get; set; }
            public List<ZoneRange> ZoneRangesHR { get; set; }
            public List<ZoneWKg> ZoneWKg { get; set; }

            public string ResumenZonasPower { get; set; }
            public string ResumenZonasHR { get; set; }
            public string ResumenZonasKg { get; set; }
        }

        public class ZoneRange
        {
            public int From { get; set; }
            public int To { get; set; }
            public int FromPer { get; set; }
            public int ToPer { get; set; }
            public string Description { get; set; }
        }

        public class ZoneWKg
        {
            public decimal Peso { get; set; }
            public decimal WKg { get; set; }
            public int FTPDerivado { get; set; }
            public decimal WKgDerivado { get; set; }
            public bool MyWeight { get; set; }
        }

        public void Calculate(ZoneCalculatorModel par)
        {
            try
            {
                par.ZoneRangesPower = new List<ZoneRange>
                {
                    new ZoneRange() { Description = "Z1", FromPer = 0, ToPer = 54 },
                    new ZoneRange() { Description = "Z2", FromPer = 55, ToPer = 75 },
                    new ZoneRange() { Description = "Z3", FromPer = 76, ToPer = 90 },
                    new ZoneRange() { Description = "Sweet Spot", FromPer = 88, ToPer = 93 },
                    new ZoneRange() { Description = "Z4", FromPer = 91, ToPer = 105 },
                    new ZoneRange() { Description = "Z5", FromPer = 106, ToPer = 120 },
                    new ZoneRange() { Description = "Z6", FromPer = 121, ToPer = 150 },
                    new ZoneRange() { Description = "Z7", FromPer = 150, ToPer = 300 }
                };
                foreach (var item in par.ZoneRangesPower)
                {
                    item.From = par.FTPNetInWatts * item.FromPer / 100;
                    item.To = par.FTPNetInWatts * item.ToPer / 100;
                }


                par.ZoneRangesHR = new List<ZoneRange>
                {
                    new ZoneRange() { Description = "Z1", FromPer = 0, ToPer = 68 },
                    new ZoneRange() { Description = "Z2", FromPer = 69, ToPer = 83 },
                    new ZoneRange() { Description = "Z3", FromPer = 84, ToPer = 94 },
                    new ZoneRange() { Description = "Z4", FromPer = 95, ToPer = 105 },
                    new ZoneRange() { Description = "Z5", FromPer = 106, ToPer = 150 }
                };
                foreach (var item in par.ZoneRangesHR)
                {
                    item.From = par.HBAvg * item.FromPer / 100;
                    item.To = par.HBAvg * item.ToPer / 100;
                }
                par.ZoneFatMax = (int)(par.HBAvg * 0.65);
                par.ZoneFHZona = (int)(par.HBAvg * 0.8);
                par.Description = $"Cálculo Ok";

                par.ZoneWKg = new List<ZoneWKg>();
                var miPeso = par.Weight;
                var initweight = miPeso - 4;
                var endweight = initweight + 9;
                while (initweight < endweight)
                {
                    var WKg = decimal.Round(par.FTPNetInWatts / initweight, 2);
                    par.ZoneWKg.Add(new ZoneWKg() { Peso = initweight, WKg = WKg, FTPDerivado = (int)(WKg * miPeso) });
                    initweight += 0.5M;
                }
                //cual es mi fila de peso actual?
                var miFila = par.ZoneWKg.Last(i => i.Peso <= miPeso);
                miFila.MyWeight = true;
                foreach (var it in par.ZoneWKg)
                {
                    it.WKgDerivado = decimal.Round(it.FTPDerivado / it.Peso, 2);
                }

                par.ZoneFatMaxStr = $"Frecuencia cardíaca deseable para consumo máximo de grasa {par.ZoneFatMax}";
                par.ZoneFHZonaStr = $"Frecuencia cardíaca deseable para trabajo de Base {par.ZoneFHZona}";

                par.ResumenZonasPower = "Zonas por potencia";
                par.ResumenZonasHR = "Zonas por frecuencia cardíaca";
                par.ResumenZonasKg = $"CÁLCULOS DE W/KG<br/>Según tus FTP se calcula como sería tu w/kg con otro peso diferente al actual {miPeso}.<br/>Derivado FTP significa qué ftp tendrías manteniendo tu w/kg en cada fila." +
                    $"<br/>Derivado w/kg significa qué w/kg tendrías con el ftp derivado de esa fila";

            }
            catch (Exception exc)
            {
                par.Description = $"Error en el cálculo, revisa los datos {exc}";
            }
        }
    }
}
