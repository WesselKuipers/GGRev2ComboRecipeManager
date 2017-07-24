using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GGRev2ComboRecipeManager.Lib
{
    public class ComboRecipeManager
    {
        const int PROCESS_WM_READ = 0x0010;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;

        const int SLOT1_OFFSET = 0x00BD40A4;
        public const int SLOT_DATA_SIZE = 1028;
        public const int SLOT_CHARCODE_SIZE = 4;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        public static ComboRecipe[] ReadComboRecipes()
        {
            var processes = Process.GetProcessesByName("GuiltyGearXrd");
            if (processes.Length < 1)
            {
                return null;
            }

            var process = processes[0];
            var processHandle = OpenProcess(PROCESS_WM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, process.Id);

            var bytesRead = 0;
            var comboRecipeData = new byte[SLOT_DATA_SIZE * 5];

            ReadProcessMemory((int)processHandle, GetSlot1Offset(process), comboRecipeData, comboRecipeData.Length, ref bytesRead);

            var combo1 = new ComboRecipe(comboRecipeData, 0);
            var combo2 = new ComboRecipe(comboRecipeData, 1);
            var combo3 = new ComboRecipe(comboRecipeData, 2);
            var combo4 = new ComboRecipe(comboRecipeData, 3);
            var combo5 = new ComboRecipe(comboRecipeData, 4);

            return new[] {combo1, combo2, combo3, combo4, combo5};
        }

        public static ComboRecipe ReadComboRecipe(int slotNr)
        {
            var process = Process.GetProcessesByName("GuiltyGearXrd")?[0];
            if (process == null)
            {
                return null;
            }

            var processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

            var bytesRead = 0;
            var buffer = new byte[SLOT_DATA_SIZE];

            ReadProcessMemory((int)processHandle, GetSlot1Offset(process) + SLOT1_OFFSET * slotNr, buffer, buffer.Length, ref bytesRead);

            return new ComboRecipe(buffer, slotNr);
        }

        public static void WriteRecipe(ComboRecipe recipe, int slotNr = 0)
        {
            var process = Process.GetProcessesByName("GuiltyGearXrd")?[0];
            if (process == null)
            {
                return;
            }

            var processHandle = OpenProcess(PROCESS_WM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, process.Id);

            var data = recipe.ToRecipeData();
            var bytesWritten = 0;

            WriteProcessMemory((int)processHandle, GetSlot1Offset(process) + (SLOT_DATA_SIZE * slotNr), data, data.Length, ref bytesWritten);
        }

        private static int GetSlot1Offset(Process process)
        {
            var processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

            var bytesRead = 0;
            var bufferAddress = new byte[4];

            var ptr = IntPtr.Add(process.MainModule.BaseAddress, SLOT1_OFFSET);
            ReadProcessMemory((int)processHandle, (int)ptr, bufferAddress, 4, ref bytesRead);

            return BitConverter.ToInt32(bufferAddress, 0);
        }
    }
}
