using System;
using System.Runtime.InteropServices;
using GGRev2ComboRecipeManager.Lib.Extensions;

namespace GGRev2ComboRecipeManager.Lib.Models
{
    public class ComboRecipe
    {
        public ComboRecipeData Data { get; set; }

        public ComboRecipe(ComboRecipeData data)
        {
            Data = data;
        }
    }

    public struct ComboRecipeData
    {
        [MarshalAs(UnmanagedType.I4)]
        public CharacterCode CharacterCode;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public byte[] MoveData;

        public byte[] ToByteArray()
        {
            return ByteArrayExtensions.Combine(BitConverter.GetBytes((int) CharacterCode), MoveData);
        }

        public static int Size => 1028;

        public static ComboRecipeData FromByteArray(byte[] data)
        {
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var crd = (ComboRecipeData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(ComboRecipeData));
            handle.Free();

            return crd;
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
