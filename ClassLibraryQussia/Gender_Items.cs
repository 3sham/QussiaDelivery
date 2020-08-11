using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryQussia
{
    class Gender_Items
    {
        [Key]
        public int Item_ID { get; set; }
        [Key]
        public int Gender_ID { get; set; }
    }
}
