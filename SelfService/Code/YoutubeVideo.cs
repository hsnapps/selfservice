using System.Collections.Generic;

namespace SelfService.Code
{
    class YoutubeRespone
    {
        public string Error { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public List<Link> Links { get; set; }
    }

    class Link
    {
        public Link(YoutubeVideoType type, string url) {
            Type = type;
            URL = url;
        }

        public YoutubeVideoType Type { get; set; }
        public string URL { get; set; }
    }

    class YoutubeVideoType
    {
        public YoutubeVideoType(string format, string quality) {
            Format = format;
            Quality = quality;
        }

        public string Format { get; set; }
        public string Quality { get; set; }
    }
}
