using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Entities
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string? Text { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();//Many To Many-> bir tag birden fazla yorumda da olabilir.Coka cok iliski
    }
}
