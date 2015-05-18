using CCLRAbogados.Core.BL;
using CCLRAbogados.Core.DTO;
using CCLRAbogados.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CCLRAbogados.Helpers.Razor
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString hidePortrait(this HtmlHelper htmlHelper, bool classOnly = true)
        {
            var hide = "hidePortrait";
            if (!classOnly) hide = "class='hidePortrait'";
            return MvcHtmlString.Create(hide);
        }

        public static MvcHtmlString hideXS(this HtmlHelper htmlHelper, bool classOnly = true)
        {
            var hide = "hidden-xs";
            if (!classOnly) hide = "class='hidden-xs'";
            return MvcHtmlString.Create(hide);
        }

        public static MvcHtmlString showAlertMessage(this HtmlHelper htmlHelper, string status_field, string message = "")
        {
            if (status_field != null && !String.IsNullOrWhiteSpace(status_field))
            {
                StringBuilder sb = new StringBuilder();
                if (status_field == CONSTANTES.SUCCESS)
                {
                    sb.Append("<div class='alert alert-success'>");
                    sb.Append("<button type='button' class='close' data-dismiss='alert'>×</button>");
                    sb.AppendFormat("<i class='fa fa-ok-sign'></i> ");
                    if (message != null && !String.IsNullOrWhiteSpace(message)) { sb.Append(message); }
                    else { sb.Append(CONSTANTES.SUCCESS_MESSAGE); }
                    sb.Append("</div>");
                }
                else if (status_field == CONSTANTES.ERROR)
                {
                    sb.Append("<div class='alert alert-danger'>");
                    sb.Append("<button type='button' class='close' data-dismiss='alert'>×</button>");
                    sb.AppendFormat("<i class='fa fa-ban-circle'></i> ");
                    if (message != null && !String.IsNullOrWhiteSpace(message)) { sb.Append(message); }
                    else { sb.Append(CONSTANTES.ERROR_MESSAGE); }
                    sb.Append("</div>");
                }
                return MvcHtmlString.Create(sb.ToString());
            }
            return MvcHtmlString.Empty;
        }

        public static string printNavLogic(string URI, string URI_PADRE = null)
        {
            NavigationBL Nav = new NavigationBL();
            var links = Nav.getNavigationLinks(URI, URI_PADRE);
            if (links != null)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                var target = "";
                foreach (EnlaceDTO enlace in links)
                {
                    if (enlace.EsEnlaceExterno && enlace.Target) target = "target='_blank'";
                    else target = "";
                    sb.AppendFormat("<li><a href='{0}' {1}>{2}</a></li>", enlace.Url, target, enlace.Titulo);
                }
                return sb.ToString();
            }
            return "";
        }

        public static string printGroupedNavLogic(string URI)
        {
            NavigationBL Nav = new NavigationBL();
            var groupedList = Nav.getGroupedNavigationLinks(URI);
            if (groupedList != null)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (CategoriaPagina cat in groupedList.Keys)
                {
                    sb.AppendFormat("<li class='dropdown-header'><span>{0}</span></li>", cat.Nombre);
                    var target = "";
                    foreach (CCLRAbogados.Core.DTO.EnlaceDTO enlace in groupedList[cat])
                    {
                        if (enlace.EsEnlaceExterno && enlace.Target) target = "target='_blank'";
                        else target = "";
                        sb.AppendFormat("<li><a href='{0}' {1}>{2}</a></li>", enlace.Url, target, enlace.Titulo);
                    }
                }
                return sb.ToString();
            }
            return "";

        }

        public static string ToCapitalize(this string cadena)
        {
            char[] a = cadena.ToLower().ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static MvcHtmlString printNav(this HtmlHelper htmlHelper, string URI, bool grouped = false, string URI_PADRE = null)
        {
            string nav = "";
            if (!grouped) nav = printNavLogic(URI, URI_PADRE);
            else nav = printGroupedNavLogic(URI);
            if (!String.IsNullOrEmpty(nav))
            {
                return MvcHtmlString.Create(nav);
            }
            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString showHighlightsAdmision(this HtmlHelper htmlHelper, IDictionary<string, HighLight> list)
        {
            UrlHelper helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            StringBuilder sb = new StringBuilder();
            int cont = 0;
            foreach (string path in list.Keys)
            {
                cont++;
                var highlight = list[path];
                if (cont == 1 || cont % 4 == 0) sb.Append("<div class='row'>");
                sb.AppendFormat("<div class='col-md-4 col-sm-6 col-xs-12 col-admision'><div class='block-content {0}'><div class='block-img'>", highlight.Css);
                sb.AppendFormat("<a href='{0}'><img src='{1}'></a>", path, highlight.Image);
                sb.AppendFormat("<a href='{0}' class='block-name'>{1}</a>", path, highlight.Nombre);
                sb.Append("<div class='triangle-topleft'></div></div>");
                sb.Append("<div class='block-btn'><div class='subnav-arw'></div>");
                sb.AppendFormat("<a href='{0}'>{1}</a></div>", helper.Action("Postula", "Admision", new { id = highlight.Descripcion }, HttpContext.Current.Request.Url.Scheme), highlight.Descripcion.Equals(CONSTANTES.URI_PREGRADO.ToLower()) ? "Postula" : "Inscríbete");
                sb.Append("<ul>");
                sb.Append(printNavLogic(highlight.Descripcion));
                sb.Append("</ul></div></div>");
                if (cont % 3 == 0) sb.Append("</div>");
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString getHighlightStyles(this HtmlHelper htmlHelper, IDictionary<string, HighLight> list)
        {
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

        private static string showHighlightsVLogic(IDictionary<string, HighLight> list, int columns, bool longDesc = false)
        {
            StringBuilder sb = new StringBuilder();
            string colcss = "";
            string offset = "";
            switch (columns)
            {
                case 1: colcss = "12"; break;
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
                    sb.AppendFormat("<div class='col-md-{0} col-sm-6'><div class='section-highlight {1}'>", colcss, highlight.Css);
                else
                {
                    sb.AppendFormat("<div class='col-sm-{0} {1}'><div class='section-highlight {2}'>", colcss, offset, highlight.Css);
                }
                if (!longDesc) sb.AppendFormat("<div class='section-highlight-block'><a href='{0}'><p>{1}</p></a>", path, highlight.Descripcion);
                else sb.AppendFormat("<div class='section-highlight-block'><a href='{0}'>{1}</a>", path, highlight.Resumen);
                sb.AppendFormat("<a href='{0}' class='section-highlight-name'>{1}</a></div><div class='triangle-topleft'></div>", path, highlight.Nombre);
                sb.AppendFormat("<a href='{0}'></a></div></div>", path);
            }
            return sb.ToString();
        }

        public static MvcHtmlString showHighlightsV(this HtmlHelper htmlHelper, IDictionary<string, HighLight> list, int columns = 3, bool longDesc = false)
        {
            string result = showHighlightsVLogic(list, columns, longDesc);
            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString showHighlightsH(this HtmlHelper htmlHelper, IDictionary<string, HighLight> list, bool showOffset = true, int columns = 1, bool description = false)
        {
            StringBuilder sb = new StringBuilder();
            string colcss = "";
            string offset = "";
            switch (columns)
            {
                case 1:
                    {
                        colcss = "10";
                        if (showOffset) offset = "col-sm-offset-1";
                        if (description) colcss = "3";
                        break;
                    }
                case 2:
                    {
                        if (showOffset)
                        {
                            colcss = "5";
                            offset = "col-sm-offset-1";
                        }
                        else
                        {
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
                if (columns == 1 || (columns == 2 && cont % 2 != 0)) sb.Append("<div class='row'>");
                var highlight = list[path];
                if (columns > 2 || (columns == 2 && cont % 2 == 0) || !showOffset) sb.AppendFormat("<div class='col-sm-{0}'><div class='section-highlight block-title-left {1}'>", colcss, highlight.Css);
                else
                {
                    sb.AppendFormat("<div class='col-sm-{0} {1}'><div class='section-highlight block-title-left {2}'>", colcss, offset, highlight.Css);
                }
                sb.AppendFormat("<div class='section-highlight-block'><a href='{0}' class='section-highlight-name'>{1}</a></div></div></div>", path, highlight.Nombre);
                if (description) sb.AppendFormat("<div class='col-sm-7 block-content-left'><article>{0}</article></div>", highlight.Resumen);
                if (columns == 1 || (columns == 2 && cont % 2 == 0)) sb.Append("</div>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }
        public static MvcHtmlString showGroupedHighlights(this HtmlHelper htmlHelper, IDictionary<CategoriaPagina, Dictionary<string, HighLight>> list, int columns = 3)
        {
            StringBuilder sb = new StringBuilder();
            foreach (CategoriaPagina categoria in list.Keys)
            {
                sb.AppendFormat("<h3>{0}</h3>", categoria.Nombre);
                sb.Append("<div class='row section-highlights section-faculty'>");
                sb.Append(showHighlightsVLogic(list[categoria], columns));
                sb.Append("</div>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
