using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Northwind_John.Models
{
   [MetadataType(typeof(userMetadata))]
   public partial class user
   {
      public class userMetadata
      {
         public int user_id { get; set; }
         [DisplayName("姓名")]
         public string user_name { get; set; }
         public int dept_id { get; set; }
         [DisplayName("更新時間")]
         public string update_date { get; set; }
         public virtual dept dept { get; set; }
      }
   }
}