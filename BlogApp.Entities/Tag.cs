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
        public string? Url { get; set; }
        public TacColors? Color { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();//Many To Many-> bir tag birden fazla yorumda da olabilir.Coka cok iliski
    }
    public enum TacColors
    {
        //Bootsrapt isimlerini kullnarak renk tanimlama yapiyoruz
        primary,danger,warning, success, sencondary,info
    }
}
