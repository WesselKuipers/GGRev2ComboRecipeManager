using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binarysharp.MemoryManagement;

namespace GGRev2ComboRecipeManager.Lib
{
    public class ModuleManager
    {
        const string PROCESS_NAME = "GuiltyGearXrd";
        public MemorySharp _sharp;

        public DummyRecordingManager DummyRecordingManager;
        public ComboRecipeManager ComboRecipeManager;

        public ModuleManager()
        {
            var process = Process.GetProcessesByName(PROCESS_NAME).FirstOrDefault();

            if (process == null)
            {
                throw new EntryPointNotFoundException("Guilty Gear process not found");
            }

            _sharp = new MemorySharp(process);
            DummyRecordingManager = new DummyRecordingManager(_sharp);
            ComboRecipeManager = new ComboRecipeManager(_sharp);

        }
    }
}
