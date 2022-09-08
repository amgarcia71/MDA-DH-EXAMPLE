using System;

namespace Femsa.Lending.Entity.Models
{
    public class {{ name }}
    {

    {% for col in columns %}

         public {{col.getTypeString}} {{ col.name }} { get; set; }
  
    {% endfor %}  

    }

    //  to be continue ...
}
