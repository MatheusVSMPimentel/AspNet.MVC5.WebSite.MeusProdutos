using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatheusVSMP.AppMvc.MeusProdutos.Extensions
{
    public static class RazorExtension
    {
        public static string FormatarDocumento(this WebViewPage page, int tipoPessoa, string doc) => tipoPessoa == 1 ? string.Format("{0:000\\.000\\.000-00}", Convert.ToUInt64(doc)) : string.Format("{0:00\\.000\\.000/0000-00}", Convert.ToUInt64(doc));
        public static bool ExibirNaURL(this WebViewPage page, Guid Id)
        {
            var urlEmUso = HttpContext.Current.Request.Path;
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);


                return urlEmUso == urlHelper.Action("Edit", "Fornecedores", new { id = Id }) || urlEmUso == urlHelper.Action("ObterEndereco", "Fornecedores", new { id = Id });
        }

    }

}