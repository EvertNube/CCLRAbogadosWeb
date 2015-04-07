using PortalUARM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PortalUARM.Helpers.Razor
{
    public static class HtmlExtensions
    {
       
        public static MvcHtmlString getHighlightStyles(this HtmlHelper htmlHelper, IDictionary<string, Highlight> list) {
            StringBuilder sb = new StringBuilder();
            foreach (string path in list.Keys)
            {
                var highlight = list[path];
                if (!String.IsNullOrEmpty(highlight.Image))
                {
                    var clase = highlight.Css.Remove(0, highlight.Css.IndexOf(' ') + 1);
                    sb.Append(".section-highlight."); sb.Append(clase); 
                    sb.Append(":hover .section-highlight-block{background-image:url("); sb.Append(highlight.Image); sb.Append(");}");
                    sb.AppendLine();
                    sb.Append(".section-highlight."); sb.Append(clase); 
                    sb.Append(":hover .triangle-topleft{background-color:#222;}");
                    sb.AppendLine();
                }
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        private static string showHighlightsVLogic(IDictionary<string, Highlight> list, int columns) {
            StringBuilder sb = new StringBuilder();
            string colcss = "";
            string offset = "";
            switch (columns)
            {
                case 1: colcss = "4"; break;
                case 2: colcss = "5"; offset = "col-sm-offset-1"; break;
                case 3: colcss = "4"; break;
                case 4: colcss = "3"; break;
                default:
                    {
                        double col = CONSTANTES.NRO_COLUMNAS / columns;
                        colcss = Math.Round(col).ToString();
                        break;
                    }
            }
            int cont = 0;
            foreach (string path in list.Keys)
            {
                cont++;
                var highlight = list[path];
                if (columns != 2 || cont % 2 == 0)
                    sb.AppendFormat("<div class='col-sm-{0}'><div class='section-highlight {1}'>", colcss, highlight.Css);
                else
                {
                    sb.AppendFormat("<div class='col-sm-{0} {1}'><div class='section-highlight {2}'>", colcss, offset, highlight.Css);
                }
                sb.AppendFormat("<div class='section-highlight-block'><a href='{0}'><p>{1}</p></a>", path, highlight.Descripcion);
                sb.AppendFormat("<a href='{0}' class='section-highlight-name'>{1}</a></div><div class='triangle-topleft'></div>", path, highlight.Nombre);
                sb.AppendFormat("<a href='{0}'></a></div></div>", path);
            }
            return sb.ToString();
        }

        public static MvcHtmlString showHighlightsV(this HtmlHelper htmlHelper, IDictionary<string, Highlight> list, int columns = 3) {
            string result = showHighlightsVLogic(list, columns);
            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString showHighlightsH(this HtmlHelper htmlHelper, IDictionary<string, Highlight> list, bool showOffset = true, int columns = 1, bool description = false)
        {
            StringBuilder sb = new StringBuilder();
            string colcss = "";
            string offset = "";
            switch (columns)
            {
                case 1: { colcss = "10";
                          if (showOffset) offset = "col-sm-offset-1";
                          if (description) colcss = "3"; 
                          break; }
                case 2:
                    {
                        if (showOffset)
                        {
                            colcss = "5";
                            offset = "col-sm-offset-1";
                        }
                        else {
                            colcss = "6";
                        }
                        break;
                    }
                case 3: colcss = "4"; break;
                case 4: colcss = "3"; break;
                default:
                    {
                        double col = CONSTANTES.NRO_COLUMNAS / columns;
                        colcss = Math.Round(col).ToString();
                        break;
                    }
            }
            int cont = 0;
            foreach (string path in list.Keys)
            {
                cont++;
                var highlight = list[path];
                if (columns > 2 || (columns == 2 && cont % 2 == 0) || !showOffset ) sb.AppendFormat("<div class='col-sm-{0}'><div class='section-highlight block-title-left {1}'>", colcss, highlight.Css);
                else { 
                    sb.AppendFormat("<div class='col-sm-{0} {1}'><div class='section-highlight block-title-left {2}'>", colcss, offset, highlight.Css); 
                }
                sb.AppendFormat("<div class='section-highlight-block'><a href='{0}' class='section-highlight-name'>{1}</a></div></div></div>", path, highlight.Nombre);
                if (description) sb.AppendFormat("<div class='col-sm-7 block-content-left'><article>{0}</article></div>", highlight.Resumen);
            }
            return MvcHtmlString.Create(sb.ToString());
        }
        public static MvcHtmlString showGroupedHighlights(this HtmlHelper htmlHelper, IDictionary<CategoriaPagina, Dictionary<string, Highlight>> list, int columns = 3) {
            StringBuilder sb = new StringBuilder();
            foreach (CategoriaPagina categoria in list.Keys) {
                sb.AppendFormat("<h3>{0}</h3>", categoria.Nombre);
                sb.Append("<div class='row section-highlights section-faculty'>");
                sb.Append(showHighlightsVLogic(list[categoria], columns));
                sb.Append("</div>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
