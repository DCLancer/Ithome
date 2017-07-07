using System;
using System.Collections.Generic;
using System.Net;
using ITQuan.Models;
using Tiexue.UI.Common;
using System.IO;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Linq;

namespace ITQuan
{
    public static class ArticleHelp
    {

        public static List<Article> GetArticleList(string url)
        {
            if(url.IsNullOrWhiteSpace())
            {
                return new List<Article>(0);
            }

            List<Article> lstArticle = new List<Article>(32);

            foreach(HtmlNode n in 
            GetArticleListNodeFromHtml(GetHtml(url)).ChildNodes.Where(a => !a.Name.Equals("#text")))
            {
                lstArticle.Add(GetSingleArticle(n));
            }

            return lstArticle;

        }

		/// <summary>
		/// Gets the single article.
		/// </summary>
		/// <returns>The single article.</returns>
		/// <param name="articleLiNode">Article li.</param>
        private static Article GetSingleArticle(HtmlNode articleLiNode)
        {
            if(articleLiNode ==null)
            {
                return new Article(){
                    Title="Source is null"
                };
            }
            Article ar = new Article(){
                Avatar = Regex.Match(articleLiNode.InnerHtml, "img +src *= *\"(.+?)\"").Groups[1].Value,
                Title=Regex.Match(articleLiNode.InnerHtml, "t_cate_title\">(.+?)\\s*</a>").Groups[1].Value,
                Tag=Regex.Match(articleLiNode.InnerHtml,"t_cate\">(\\[\\w+\\])").Groups[1].Value,
                Author=Regex.Match(articleLiNode.InnerHtml, "user/\\d+/\">[^<](.+)</a> ").Groups[1].Value,
                Link=string.Concat(
                    "http://quan.ithome.com",
                    Regex.Match(articleLiNode.InnerHtml, "href=\"(/\\d+/\\d+/\\d+\\.htm)\"").Groups[1].Value),
                Column=Regex.Match(articleLiNode.InnerHtml, "回复(.+)<span").Groups[1].Value.Replace("&nbsp;",string.Empty),
                LastPost=Regex.Match(articleLiNode.InnerHtml, "bf\">(.+?)</span>").Groups[1].Value,
                PublishTime=Regex.Match(articleLiNode.InnerHtml, "user/\\d+/\">[^<](.+)</a>\\s+(.+发表)").Groups[1].Value,
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
        private static HtmlNode GetArticleListNodeFromHtml(string homePageHtml)
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
        private static string GetHtml(string url)
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

            return ConvertEncoding(Encoding.GetEncoding("GB2312"),Encoding.UTF8,strHtml);
        }


        private static string ConvertEncoding(Encoding src,Encoding des,string content)
        {
            string str=
             des.GetString(Encoding.Convert(src,des,src.GetBytes(content)));
            return str;
        }

    }
}
