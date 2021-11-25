using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcosSoft
{
  public sealed  class TssCalculator
    {

       


        private class Rango
        {
            public int Puntos { get; private set; }
            public int Porcentaje { get; private set; }

            public Rango(int porc, int points)
            {
                Porcentaje = porc;
                Puntos = points;
            }
        }


        public string GetCalculation(string txtpulsacionesmedias,string txtfthr,string txtminutos)
        {

            try
            {
                var listaLimites = new List<Rango>();
                listaLimites.Add(new Rango(0, 20));
                listaLimites.Add(new Rango(61, 30));
                listaLimites.Add(new Rango(71, 40));
                listaLimites.Add(new Rango(81, 50));
                listaLimites.Add(new Rango(85, 60));
                listaLimites.Add(new Rango(90, 70));
                listaLimites.Add(new Rango(94, 80));
                listaLimites.Add(new Rango(100, 100));
                listaLimites.Add(new Rango(103, 120));
                listaLimites.Add(new Rango(106, 140));



                var listaDatos = new List<Rango>();
                var conta = 0;
                while (conta <= 100)
                {
                    if (conta < 60)
                    {
                        listaDatos.Add(new Rango(conta, 20));
                        conta++;
                        continue;
                    }
                    var mirango = listaLimites.Last(i => i.Porcentaje < conta);
                    var rangosuperior = listaLimites.First(i => i.Porcentaje > conta);
                    //hay que sacar el porcentaje
                    var diferenciaPuntos = rangosuperior.Puntos - mirango.Puntos;
                    var diferenciaPorc = rangosuperior.Porcentaje - mirango.Porcentaje;
                    var PuntoPorPorc = diferenciaPuntos / diferenciaPorc;

                    var miPorcentajeEnNivel = rangosuperior.Porcentaje - conta;
                    var añadoenNivel = miPorcentajeEnNivel * PuntoPorPorc;
                    var totalPorc = añadoenNivel + mirango.Puntos;

                    listaDatos.Add(new Rango(conta, totalPorc));

                    conta++;
                }

                var mifth = decimal.Parse(txtfthr);
                var mispulsacionesmedia = decimal.Parse(txtpulsacionesmedias);
                var miporcentaje = mispulsacionesmedia * 100 / mifth;
                var mirangoSeleccionado = listaDatos.First(i => i.Porcentaje == Math.Round(miporcentaje, 0, MidpointRounding.AwayFromZero));
                var misPuntos = mirangoSeleccionado.Puntos;
                var miPorcentaje = mirangoSeleccionado.Porcentaje;
                var mishoras = decimal.Round(decimal.Parse(txtminutos) / 60, 2);

                return $"{decimal.Round(miporcentaje, 1)} % -> {misPuntos} TSS * {mishoras} horas=> TOTAL {decimal.Round(misPuntos * mishoras, 2)} TSS en Golden Ch.";
            }
          catch(Exception exc)
            {
                return exc.ToString();
            }
        }
    }
}
