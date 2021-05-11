using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Lowcode.Domain.Services.DynamicFormFacade
{
    public class DynamicFormModel
    {

        public Int32 Id { get; set; }

        [MaxLength(250, ErrorMessage = "Name không được vượt quá  250 ký tự.")]
        public string Name { get; set; }

        [MaxLength(450, ErrorMessage = "Description không được vượt quá  450 ký tự.")]
        public string Description { get; set; }

        [MaxLength(250, ErrorMessage = "TableName không được vượt quá  250 ký tự.")]
        public string TableName { get; set; }


        public string Content { get; set; }

        [MaxLength(250, ErrorMessage = "Path không được vượt quá  250 ký tự.")]
        public string Path { get; set; }

        public string ApiInfo { get; set; }
        //public List<DynamicFormApiModel> DynamicFormApis { get; set; }

    }
}
