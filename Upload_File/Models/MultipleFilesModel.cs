using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Upload_File.Models
{
    public class MultipleFilesModel : ResponseModel
    {
        [Required(ErrorMessage = "Please select files")]
        public List<IFormFile> Files { get; set; }
    }
}
