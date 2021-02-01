using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkstationPreparer.Models;

namespace WorkstationPreparer
{
    public partial class Form : System.Windows.Forms.Form
    {
        private List<KeyboardHook> keyboardHooks = new List<KeyboardHook>();
        public Form()
        {
            InitializeComponent();

            Settings settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("settings.json"));
            foreach (Preset preset in settings.Presets)
                ProcessPreset(preset);
        }

        private void ProcessPreset(Preset preset)
        {
            KeyboardHook keyboardHook = new KeyboardHook();
            keyboardHook.KeyPressed += new EventHandler<KeyPressedEventArgs>((o, e) =>
            {
                foreach (Models.Action action in preset.Actions)
                    ExecuteAction(action);
            });

            keyboardHook.RegisterHotKey(preset.ModifierKey, preset.HotKey);
            keyboardHooks.Add(keyboardHook);
        }

        private void ExecuteAction(Models.Action action)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.FileName = action.File;
            startInfo.Arguments = action.Arguments;
            process.StartInfo = startInfo;
            process.Start();
        }
    }

}
