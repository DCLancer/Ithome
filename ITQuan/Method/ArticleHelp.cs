using System;
using System.Collections.Generic;
using System.Net;
using ITQuan.Models;
using Tiexue.UI.Common;
using System.IO;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace ITQuan
{
    public static class ArticleHelp
    {

        public static List<Article> GetArticleList()
        {

            return null;

        }

		/// <summary>
		/// Gets the single article.
		/// </summary>
		/// <returns>The single article.</returns>
		/// <param name="articleLiNode">Article li.</param>
		public static Article GetSingleArticle(HtmlNode articleLiNode)
        {
            if(articleLiNode ==null)
            {
                return new Article(){
                    Title="Source is null"
                };
            }
            Article ar = new Article(){
                Avatar = Regex.Match(articleLiNode.InnerHtml, "img +src *= *\"(.+?)\"").Groups[1].Value,
                Title=Regex.Match(articleLiNode.InnerHtml, "t_cate_title\">(.+)\\s*</a>").Groups[1].Value,
                Tag=Regex.Match(articleLiNode.InnerHtml,"t_cate\">(\\[\\w+\\])").Groups[1].Value,
                Author=Regex.Match(articleLiNode.InnerHtml, "user/\\d+/\">(\\w+)<").Groups[1].Value,
                Link=string.Concat(
                    "http://quan.ithome.com",
                    Regex.Match(articleLiNode.InnerHtml, "href=\"(/\\d+/\\d+/\\d+\\.htm)\"").Groups[1].Value),
                Column=Regex.Match(articleLiNode.InnerHtml, "回复(.+)<span").Groups[1].Value.Replace("&nbsp;",string.Empty),

                ViewTimes=Regex.Match(articleLiNode.InnerHtml, "view.png\">(\\d+)<img").Groups[1].Value.ToSimpleT(0),
                CommentTimes=Regex.Match(articleLiNode.InnerHtml, "reply.png\">(\\d+)<").Groups[1].Value.ToSimpleT(0)
            };


            return ar;
        }

        /// <summary>
        /// Gets the article list part from html.
        /// </summary>
        /// <returns>The article list part from html.</returns>
        /// <param name="homePageHtml">Home page html.</param>
        public static HtmlNode GetArticleListNodeFromHtml(string homePageHtml)
        {
            if(homePageHtml.IsNullOrWhiteSpace())
            {
                return null;
            }
            HtmlNode hdArticleUl=null;
            HtmlDocument hd = new HtmlDocument();//.LoadHtml(homePageHtml);
            try
            {
                hd.LoadHtml(homePageHtml.Replace("\r\n", string.Empty));
                hdArticleUl =hd.DocumentNode.SelectSingleNode(@"//div[@class='thread'][2]/div[@class='t_list']/ul[1]");

                return hdArticleUl;
            }
            catch(Exception ex)
            {
                File.AppendAllText("/Users/X-Man/Desktop/Error.txt", string.Concat(ex.Message,"\r\n"));
            }
;
            return hdArticleUl;
        }



        /// <summary>
        /// Gets the html.
        /// </summary>
        /// <returns>The html.</returns>
        /// <param name="url">URL.</param>
        public static string GetHtml(string url)
        {
            if (url.IsNullOrWhiteSpace() || !url.SafeContains("http", StringComparison.InvariantCultureIgnoreCase))
            {
                return "url address error";
            }
            string strHtml = String.Empty;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    strHtml = wc.DownloadString(url);

                }
            }
            catch(Exception ex)
            {
                File.AppendAllText("/Users/X-Man/Desktop/Error.txt", string.Concat(ex.Message, "\r\n"));
            }

            return strHtml;
        }
    }
}
