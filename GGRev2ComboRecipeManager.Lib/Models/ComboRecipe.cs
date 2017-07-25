using System;
using GGRev2ComboRecipeManager.Lib.Extensions;

namespace GGRev2ComboRecipeManager.Lib.Models
{
    public class ComboRecipe
    {
        public const int SLOT_CHARCODE_SIZE = 4;
        public const int SLOT_DATA_SIZE = 1028;

        public CharacterCode CharacterCode;
        public byte[] MoveData = new byte[SLOT_DATA_SIZE - SLOT_CHARCODE_SIZE];

        public ComboRecipe(byte[] comboData, int slotNr = 0)
        {
            CharacterCode = (CharacterCode) BitConverter.ToInt32(comboData, slotNr * SLOT_DATA_SIZE);
            Array.Copy(comboData, slotNr * SLOT_DATA_SIZE + SLOT_CHARCODE_SIZE, MoveData, 0, SLOT_DATA_SIZE - SLOT_CHARCODE_SIZE);
        }

        public byte[] ToRecipeData()
        {
            return ByteArrayExtensions.Combine(BitConverter.GetBytes((int) CharacterCode), MoveData);
        }
    }

    public enum CharacterCode
    {
        Sol       = 0x0,
        Ky        = 0x1,
        May       = 0x2,
        Millia    = 0x3,
        Zato      = 0x4,
        Potemkin  = 0x5,
        Chipp     = 0x6,
        Faust     = 0x7,
        Axl       = 0x8,
        Venom     = 0x9,
        Slayer    = 0xA,
        Ino       = 0xB,
        Bedman    = 0xC,
        Ramlethal = 0xD,
        Sin       = 0xE,
        Elphelt   = 0xF,
        Leo       = 0x10,
        Johnny    = 0x11,
        JackO     = 0x12,
        Jam       = 0x13,
        Haehyun   = 0x14,
        Raven     = 0x15,
        Dizzy     = 0x16,
        Baiken    = 0x17,
        Answer    = 0x18,
        Unknown   = -1
    }
}
