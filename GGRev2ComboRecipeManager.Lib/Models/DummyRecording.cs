using System;
using System.Runtime.InteropServices;
using GGRev2ComboRecipeManager.Lib.Extensions;

namespace GGRev2ComboRecipeManager.Lib.Models
{
    public class DummyRecording
    {
        public DummyRecordingData Data { get; set; }

        public DummyRecording(DummyRecordingData data)
        {
            Data = data;
        }
    }

    public struct DummyRecordingData
    {
        public int Side;
        public int Length;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4800)]
        public byte[] RecordingData;

        public byte[] ToByteArray()
        {
            return ByteArrayExtensions.Combine(BitConverter.GetBytes(Side), BitConverter.GetBytes(Length), RecordingData);
        }

        public static int Size => 4808;

        public static DummyRecordingData FromByteArray(byte[] data)
        {
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var drd = (DummyRecordingData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(DummyRecordingData));
            handle.Free();

            return drd;
        }
    }
}