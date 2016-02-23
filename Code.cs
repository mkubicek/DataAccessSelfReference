using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessSelfReference
{
    public class Code
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IList<Code> RestrictedFor { get; set; }/* = new List<Code>();*/

        public virtual IList<Code> Restricts { get; set; }/* = new List<Code>();*/
    }
}
