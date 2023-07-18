using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;

namespace RecommendationSystem
{
    internal class Framework // udělat abstract a rozšířit?
    {
        public DataFrame Data { get; set; } // nebo jen prostě class která implementuje víc interfaces (do ktery spada DataFrame)
        public Approach Approach { get; set; }
        public Viewer Viewer { get; set; }
    }
}
