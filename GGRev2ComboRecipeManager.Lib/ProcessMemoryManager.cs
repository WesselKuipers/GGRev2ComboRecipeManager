using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GGRev2ComboRecipeManager.Lib
{
    public static class ProcessMemoryManager
    {
        private const int PROCESS_WM_READ = 0x0010;
        private const int PROCESS_VM_WRITE = 0x0020;
        private const int PROCESS_VM_OPERATION = 0x0008;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        public static byte[] ReadProcessMemory(string processName, int dataOffset, int length, bool offsetIsPointer, int additionalOffset = 0)
        {
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length < 1)
            {
                return null;
            }

            var process = processes[0];
            var processHandle = OpenProcess(PROCESS_WM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, process.Id);

            var bytesRead = 0;
            var data = new byte[length];

            var offset = offsetIsPointer ? GetOffsetFromPointer(process, dataOffset) : dataOffset;

            ReadProcessMemory((int)processHandle, offset + additionalOffset, data, data.Length, ref bytesRead);

            return data;
        }

        public static void WriteProcessMemory(string processName, int dataOffset, byte[] data, bool offsetIsPointer, int additionalOffset = 0)
        {
            var process = Process.GetProcessesByName(processName)?[0];
            if (process == null)
            {
                return;
            }

            var processHandle = OpenProcess(PROCESS_WM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, process.Id);
            var bytesWritten = 0;

            var offset = offsetIsPointer ? GetOffsetFromPointer(process, dataOffset) : dataOffset;

            WriteProcessMemory((int)processHandle, offset + additionalOffset, data, data.Length, ref bytesWritten);
        }

        private static int GetOffsetFromPointer(Process process, int pointerOffset)
        {
            var processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

            var bytesRead = 0;
            var bufferAddress = new byte[4];

            var ptr = IntPtr.Add(process.MainModule.BaseAddress, pointerOffset);
            ReadProcessMemory((int)processHandle, (int)ptr, bufferAddress, 4, ref bytesRead);

            return BitConverter.ToInt32(bufferAddress, 0);
        }
    }
}
