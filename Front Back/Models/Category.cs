using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Front_Back.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Nagarersen Aye?"),StringLength(50,ErrorMessage ="Harua catmadi?")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public  Nullable<DateTime> Deleted { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
