using System;
namespace ITQuan.Models
{
    /// <summary>
    /// Article.
    /// </summary>
    public class Article
    {
        public Article()
        {
        }

        public int ArticleId{
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 帖子标题前的标签，经验、讨论、杂谈、手机等
        /// </summary>
        /// <value>The tag.</value>
        public string Tag
        {
            get;
            set;
        }

        public string Author
        {
            get;
            set;
        }

        /// <summary>
        /// 最后评论者
        /// </summary>
        /// <value>The last post.</value>
        public string LastPost
        {
            get;
            set;
        }


        public string PublishTime
        {
            get;
            set;
        }

        public string Column
        {
            get;
            set;
        }

        public int ViewTimes
        {
            get;
            set;
        }

        public int CommentTimes{
            get;
            set;
        }

        public string Avatar{
            get;
            set;
        }

        public string Link{
            get;
            set;
        }

    }
}
