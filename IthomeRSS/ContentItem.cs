using System;



namespace IthomeRSS
{
    public class ContentItem
    {
        public ContentItem()
        {
        }

        public string Title{
            get;
            set;
        }

        public string Link{
            get;
            set;
        }

        public string Description{
            get;
            set;
        }

        public string Thumbnail{
            get;
            set;
        }

        public  DateTime PubData{
            get;
            set;
        }


    }
}
