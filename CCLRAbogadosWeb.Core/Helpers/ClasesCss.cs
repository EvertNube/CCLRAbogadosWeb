using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogadosWeb.Core.Helpers
{
    public static class ClasesCss
    {
        public static IList<string> getLista()
        {
            IList<string> lista = new List<string>();

            lista.Add("red");
            lista.Add("orange");
            lista.Add("yellow");
            lista.Add("green");
            lista.Add("lightbrown");
            lista.Add("brown");

            lista.Add("purple");
            lista.Add("pink");

            lista.Add("filosofia");
            lista.Add("educacion");
            lista.Add("periodismo");
            lista.Add("psicologia");
            lista.Add("cienciapolitica");
            lista.Add("turismosostenible");
            lista.Add("derecho");
            lista.Add("economiagestionambiental");
            lista.Add("administracion");
            lista.Add("ingenieriaindustrial");

            lista.Add("maestria-azul");
            lista.Add("maestria-rojo");
            lista.Add("maestria-amarillo");
            lista.Add("maestria-naranja");
            lista.Add("maestria-morado");
            lista.Add("maestria-verde");

            lista.Add("block-title-left");
            lista.Add("section-highlight-inverse");

            return lista;
        }
    }
}
