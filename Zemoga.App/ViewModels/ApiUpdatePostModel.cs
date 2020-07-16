using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zemoga.App.ViewModels
{
    public class ApiUpdatePostModel
    {
        [Required(ErrorMessage = "PostId is Required")]
        [Range(1, 1000000, ErrorMessage = "Invalid value for postid")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "PostStatus is Required")]
        [Range(2, 3, ErrorMessage = "Invalid value for status. 2 Approved, 3 Rejected")]
        public short PostStatus { get; set; }
    }
}
