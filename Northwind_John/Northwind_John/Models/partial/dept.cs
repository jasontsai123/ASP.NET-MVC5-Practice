using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Northwind_John.Models
{
   [MetadataType(typeof(deptMetadata))]
   public partial class dept
   {
      public class deptMetadata
      {
         public int dept_id { get; set; }
         [DisplayName("部門")]
         public string dept_name { get; set; }
      }
   }
}