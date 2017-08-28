using System;
using Binarysharp.MemoryManagement;
using GGRev2ComboRecipeManager.Lib.Models;
using static GGRev2ComboRecipeManager.Lib.Models.DummyRecording;

namespace GGRev2ComboRecipeManager.Lib
{
    public class DummyRecordingManager
    {
        private const int DUMMY_SLOT1_POINTER = 0x00BB02CC;
        private MemorySharp _sharp;

        public static readonly int DUMMY_RECORDING_SIZE = DummyRecordingData.Size;
        public IntPtr Slot1Address;

        public DummyRecordingManager(MemorySharp sharp)
        {
            _sharp = sharp;
            Slot1Address = sharp.Read<IntPtr>(new IntPtr(DUMMY_SLOT1_POINTER));
        }

        public DummyRecording[] ReadDummyRecordings()
        {
            var recording1 = _sharp.Read<DummyRecordingData>(Slot1Address + DUMMY_RECORDING_SIZE * 0, false);
            var recording2 = _sharp.Read<DummyRecordingData>(Slot1Address + DUMMY_RECORDING_SIZE * 1, false);
            var recording3 = _sharp.Read<DummyRecordingData>(Slot1Address + DUMMY_RECORDING_SIZE * 2, false);

            return new[]
            {
                new DummyRecording(recording1),
                new DummyRecording(recording2),
                new DummyRecording(recording3)
            };
        }

        public DummyRecording ReadDummyRecording(int slotNr)
        {
            var data = _sharp.Read<DummyRecordingData>(Slot1Address + DUMMY_RECORDING_SIZE * slotNr, false);

            return data.Length > 0 ? new DummyRecording(data) : null;
        }

        public void WriteDummyRecording(DummyRecording recording, int slotNr)
        {
            _sharp.Write(Slot1Address + slotNr * DUMMY_RECORDING_SIZE, recording.Data, false);
        }
    }
}
