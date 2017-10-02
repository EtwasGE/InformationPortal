using System.Configuration;
using System.Web.Mvc;

namespace Portal.Web.Helpers
{
    public static class TagBuilderHelper
    {
        // <i class="iconCssClass"></i>
        // <span class="spanHeaderCssClass">linkText</span> 
        // <span class="spanControlCssClass">
        //    <i class="iconControlCssClass"></i>
        // </span>
        public static string Tree(string linkText, string iconCssClass, string iconControlCssClass, 
            string spanHeaderCssClass, string spanControlCssClass)
        {
            var tagIcon = new TagBuilder("i");
            tagIcon.AddCssClass(iconCssClass);

            var tagSpanHeader = new TagBuilder("span");
            tagSpanHeader.SetInnerText(linkText);

            if (!string.IsNullOrEmpty(spanHeaderCssClass))
            {
                tagSpanHeader.AddCssClass(spanHeaderCssClass);
            }

            var tagIconControl = new TagBuilder("i");
            tagIconControl.AddCssClass(iconControlCssClass);

            var tagSpanControl = new TagBuilder("span");

            if (!string.IsNullOrEmpty(spanControlCssClass))
            {
                tagSpanControl.AddCssClass(spanControlCssClass);
            }

            tagSpanControl.InnerHtml = tagIconControl.ToString(TagRenderMode.Normal);

            var tagIconString = tagIcon.ToString(TagRenderMode.Normal);
            var tagSpanHeaderString = tagSpanHeader.ToString(TagRenderMode.Normal);
            var tagSpanControlString = tagSpanControl.ToString(TagRenderMode.Normal);
            return $"{tagIconString} {tagSpanHeaderString} {tagSpanControlString}";
        }

        // <i class="iconCssClass"></i>
        // <span class="spanCssClass">linkText</span>
        public static string IconText(string linkText, string iconCssClass, string spanCssClass)
        {
            var tagIcon = new TagBuilder("i");
            tagIcon.AddCssClass(iconCssClass);

            var tagSpan = new TagBuilder("span");
            tagSpan.SetInnerText(linkText);

            if (!string.IsNullOrEmpty(spanCssClass))
            {
                tagSpan.AddCssClass(spanCssClass);
            }

            var tagIconString = tagIcon.ToString(TagRenderMode.Normal);
            var tagSpanString = tagSpan.ToString(TagRenderMode.Normal);
            return $"{tagIconString} {tagSpanString}";
        }

        // <i class="iconCssClass"></i>
        public static string Icon(string iconCssClass)
        {
            var tagIcon = new TagBuilder("i");
            tagIcon.AddCssClass(iconCssClass);

            var tagIconString = tagIcon.ToString(TagRenderMode.Normal);
            return tagIconString;
        }

        // <img src="imgUrl" class="imgCssClass"/></img>
        public static string Img(string imgUrl, string imgCssClass)
        {
            var tagImg = new TagBuilder("img");

            if (!string.IsNullOrEmpty(imgCssClass))
            {
                tagImg.AddCssClass(imgCssClass);
            }

            tagImg.MergeAttribute("src", imgUrl);
            tagImg.MergeAttribute("onerror", $"this.src='{ConfigurationManager.AppSettings["DefaultImg"]}'");
            tagImg.MergeAttribute("alt", "image not found");

            var tagImgString = tagImg.ToString(TagRenderMode.Normal);
            return tagImgString;
        }
    }
}
