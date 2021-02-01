using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkstationPreparer.Models
{
    public class Preset
    {
        public Keys HotKey { get; set; }
        public global::ModifierKeys ModifierKey { get; set; }
        public IList<Action> Actions;
    }
}
