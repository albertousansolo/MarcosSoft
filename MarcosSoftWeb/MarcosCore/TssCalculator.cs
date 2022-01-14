using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcosCore
{
  public sealed  class TssCalculator
    {
        public class TssModel
        {
            public string PulsacionesMedias { get; set; } = "140";
            public string FTHR { get; set; } = "172";
            public string Minutos { get; set; } = "90";

            public string Result { get; set; }
        }


        private class Rango
        {
            public decimal Puntos { get; private set; }
            public decimal Porcentaje { get; private set; }

            public Rango(decimal porc, decimal points)
            {
                Porcentaje = porc;
                Puntos = points;
            }
        }

        private List<Rango> GetRangos(int inicioPorc, decimal finPorc, decimal puntosMin, decimal puntosMax)
        {
            var lista = new List<Rango>();
            for (decimal i = inicioPorc; i <= finPorc; i += 0.1M)
            {
                var puntosADividir = puntosMax - puntosMin;
                var miPunto = i - inicioPorc;
                var miPorc = miPunto * 100 / (finPorc - inicioPorc);
                var miValor = miPorc * puntosADividir / 100;

                lista.Add(new Rango(i, miValor + puntosMin));

            }
            return lista;
        }


        public void GetCalculation(TssModel model)
        {

            try
            {
                var listaLimites = new List<Rango>();
                listaLimites.AddRange(GetRangos(1, 60.9M, 20, 30));
                listaLimites.AddRange(GetRangos(61, 70.9M, 30, 40));
                listaLimites.AddRange(GetRangos(71, 80.9M, 40, 50));
                listaLimites.AddRange(GetRangos(81, 84.9M, 50, 60));
                listaLimites.AddRange(GetRangos(85, 89.9M, 60, 70));
                listaLimites.AddRange(GetRangos(90, 93.9M, 70, 80));
                listaLimites.AddRange(GetRangos(94, 99.9M, 80, 100));
                listaLimites.AddRange(GetRangos(100, 102.9M, 100, 120));
                listaLimites.AddRange(GetRangos(103, 106.9M, 120, 140));
                listaLimites.AddRange(GetRangos(106, 150.9M, 140, 200));

                var mifth = decimal.Parse(model.FTHR);
                var mispulsacionesmedia = decimal.Parse(model.PulsacionesMedias);
                var miporcentaje = mispulsacionesmedia * 100 / mifth;
                var miProcentajeConUndecimal = Math.Round(miporcentaje, 1, MidpointRounding.AwayFromZero);
                var mirangoSeleccionado = listaLimites.First(i => i.Porcentaje == miProcentajeConUndecimal);
                var misPuntos = mirangoSeleccionado.Puntos;
                var miPorcentaje = mirangoSeleccionado.Porcentaje;
                var mishoras = decimal.Round(decimal.Parse(model.Minutos) / 60, 2);

                model.Result= $"{miProcentajeConUndecimal} % -> {decimal.Round(misPuntos,2)} TSS * {mishoras} horas=> TOTAL {decimal.Round(misPuntos * mishoras, 2)} TSS en Golden Ch.";
            }
          catch(Exception exc)
            {
                model.Result = exc.ToString();
            }
        }
    }
}
