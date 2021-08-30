using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.WebAPI.Database
{
    public class YogaVideo
    {
        public int Id { get; set; }
        [Display(Name ="Youtube URL")]
        public string YoutubeID{ get; set; }
        [Display(Name ="Video thumbnail")]
        public string ThumbnailFileName { get; set; }
    }
}
