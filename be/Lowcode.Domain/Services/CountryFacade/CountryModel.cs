using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Lowcode.Domain.Services.CountryFacade
{
    public class CountryModel
    {

                     public Int32 Id { get; set; }
                 
         			[MaxLength(50, ErrorMessage ="CountryName không được vượt quá  50 ký tự.")]	
                     public string CountryName { get; set; }
                 
         			[MaxLength(450, ErrorMessage ="Description không được vượt quá  450 ký tự.")]	
                     public string Description { get; set; }
                  
    }
}
