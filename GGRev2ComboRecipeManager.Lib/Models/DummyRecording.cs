using System;

namespace GGRev2ComboRecipeManager.Lib.Models
{
    public class DummyRecording
    {
        public const int DUMMYRECORDING_SIZE = 4808;
        public byte[] RecordingData;

        public DummyRecording(byte[] recordingData, int slotNr)
        {
            Array.Copy(recordingData, DUMMYRECORDING_SIZE * slotNr, RecordingData, 0, DUMMYRECORDING_SIZE);
        }

    }
}
