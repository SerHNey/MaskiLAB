using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maska
{
    public class CurrentList
    {
       public static MaskiLABEntities db;
       public static List<Product> products = new List<Product>();
       public static List<Material> materials = new List<Material>();
       public static Users user;
    }
}
