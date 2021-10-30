using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DREM_API.Model.Comm
{
    /// <summary>
    /// Picture metadata for specific project
    /// </summary>
    public class ImageMetaBase
    {
        /// <summary>
        /// Project.Id
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// Name of the picture
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description of the picture
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Time when picture has been taken 
        /// </summary>
        public DateTimeOffset DateOfPicture { get; set; }
        /// <summary>
        /// Full picture resolution
        /// </summary>
        public string Src { get; set; }
        /// <summary>
        /// Small picture 
        /// </summary>
        public string Thumbnail { get; set; }

    }
}
