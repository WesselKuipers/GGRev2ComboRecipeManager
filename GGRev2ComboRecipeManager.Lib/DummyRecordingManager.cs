using GGRev2ComboRecipeManager.Lib.Models;
using static GGRev2ComboRecipeManager.Lib.Models.DummyRecording;

namespace GGRev2ComboRecipeManager.Lib
{
    public class DummyRecordingManager
    {
        private const int DUMMY_SLOT1_POINTER = 0x00BAF62C;
        const string PROCESS_NAME = "GuiltyGearXrd";

        public static DummyRecording[] ReadDummyRecordings()
        {
            var dummyRecordingData = ProcessMemoryManager.ReadProcessMemory(PROCESS_NAME, DUMMY_SLOT1_POINTER, DUMMYRECORDING_SIZE * 5, true);

            var recording1 = new DummyRecording(dummyRecordingData, 0);
            var recording2 = new DummyRecording(dummyRecordingData, 1);
            var recording3 = new DummyRecording(dummyRecordingData, 2);

            return new[] {recording1, recording2, recording3};
        }

        public static DummyRecording ReadDummyRecording(int slotNr)
        {
            var data = ProcessMemoryManager.ReadProcessMemory(PROCESS_NAME, DUMMY_SLOT1_POINTER, DUMMYRECORDING_SIZE, true, DUMMYRECORDING_SIZE * slotNr);

            return data != null ? new DummyRecording(data) : null;
        }

        public static void WriteDummyRecording(DummyRecording recording, int slotNr)
        {
            var data = recording.RecordingData;

            ProcessMemoryManager.WriteProcessMemory(PROCESS_NAME, DUMMY_SLOT1_POINTER, data, true, DUMMYRECORDING_SIZE * slotNr);
        }
    }
}
