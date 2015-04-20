using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PreventXSS
{
    public static class SanitizeHTML_Core
    {

        private static List<string> AddTerminatingTagsToTagList(List<string> tags)
        {
            var x = new List<string>();

            foreach (var item in tags)
            {
                x.Add(item.ToLowerInvariant().Trim());
                x.Add("/" + item.ToLowerInvariant().Trim());
            }
            return x;
        }

        /// <summary>
        /// Array overload for Sanitize HTML 
        /// shortcircuits is array is null        
        /// </summary>
        /// <param name="source">string to sanitize</param>
        /// <param name="Originaltags">Whitelist of tags</param>
        /// <returns></returns>
        public static string SanitizeHTML(string source, string[] Originaltags)
        {
            if (Originaltags == null)
                return HttpUtility.HtmlEncode(source);

            return SanitizeHTML(source, Originaltags.ToList());
        }


        /// <summary>
        /// Sanitize HTML based upon whiite list of tags in Originaltags
        /// </summary>
        /// <param name="source">string to sanitize</param>
        /// <param name="Originaltags">Whitelist of tags</param>
        /// <returns></returns>
        public static string SanitizeHTML(string source, List<string> Originaltags)
        {
            // Short Circuit conditions 

            // Blank String
            if (source == null)
                return null;
            if (source == string.Empty)
                return source;

            // No White List
            if (Originaltags == null)
                return HttpUtility.HtmlEncode(source);
            if (Originaltags.Count == 0)
                return HttpUtility.HtmlEncode(source);

            if (!source.Contains("<") && !source.Contains(">")) //No Tags
                return source;

            var tags = AddTerminatingTagsToTagList(Originaltags);

            // Construct string using string builder and determining one of 4 states
            // < and not in tag
            // > and in tag
            // (Not < or > char)  
            // (Not < or > char)   

            StringBuilder sb = new StringBuilder();
            string tagContent = "";
            var inTag = false;

            foreach (var item in source)
            {
                if (item == '<' && !inTag)
                {
                    //Character is a start tag and I'm not already in the tag
                    tagContent = "";
                    inTag = true;
                }
                else if (item == '>' && inTag)
                {
                    // Character is a end tag and i'm already in the tag.

                    // Check the tag for white list before adding
                    // to determine if a tag or encoded tag
                    if (tags.Contains(tagContent.ToLowerInvariant().Trim()))
                    {
                        sb.Append("<" + tagContent + ">");
                    }
                    else
                    {
                        sb.Append("&lt" + tagContent + "&gt");
                    }

                    inTag = false;
                }
                else if (item == '<' && inTag)
                {
                    // Character is a start tag and I'm  in the tag
                    // source encode incode
                    tagContent = "&gt;";
                    inTag = true;
                }
                else if (!inTag)
                {
                    // Not in Tag so add to string
                    sb.Append(item);
                }
                else if (inTag)
                {
                    // In tag so add to tagContent
                    tagContent += item;
                }
            }

            // Required if last tag is not closed correctly so still thinks
            // its in the tag.
            if (inTag)
            {
                // We left the string with an unbounded tag
                sb.Append(tagContent);
            }

            return sb.ToString();
        }

    }
}
