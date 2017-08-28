using System;
using Binarysharp.MemoryManagement;
using GGRev2ComboRecipeManager.Lib.Models;
using static GGRev2ComboRecipeManager.Lib.Models.ComboRecipe;

namespace GGRev2ComboRecipeManager.Lib
{
    public class ComboRecipeManager
    {
        public const int SLOT1_OFFSET_POINTER = 0x00BD4B94;
        public static readonly int COMBO_RECIPE_SIZE = ComboRecipeData.Size;

        private readonly MemorySharp _sharp;
        public IntPtr Slot1Address;

        public ComboRecipeManager(MemorySharp sharp)
        {
            _sharp = sharp;
            Slot1Address = sharp.Read<IntPtr>(new IntPtr(SLOT1_OFFSET_POINTER));
        }

        public ComboRecipe[] ReadComboRecipes()
        {
            var combo1 = _sharp.Read<ComboRecipeData>(Slot1Address + COMBO_RECIPE_SIZE * 0, false);
            var combo2 = _sharp.Read<ComboRecipeData>(Slot1Address + COMBO_RECIPE_SIZE * 1, false);
            var combo3 = _sharp.Read<ComboRecipeData>(Slot1Address + COMBO_RECIPE_SIZE * 2, false);
            var combo4 = _sharp.Read<ComboRecipeData>(Slot1Address + COMBO_RECIPE_SIZE * 3, false);
            var combo5 = _sharp.Read<ComboRecipeData>(Slot1Address + COMBO_RECIPE_SIZE * 4, false);

            return new[]
            {
                new ComboRecipe(combo1),
                new ComboRecipe(combo2),
                new ComboRecipe(combo3),
                new ComboRecipe(combo4),
                new ComboRecipe(combo5)
            };
        }

        public ComboRecipe ReadComboRecipe(int slotNr)
        {
            return new ComboRecipe(_sharp.Read<ComboRecipeData>(Slot1Address + COMBO_RECIPE_SIZE * slotNr, false));
        }

        public void WriteRecipe(ComboRecipe recipe, int slotNr = 0)
        {
            _sharp.Write(Slot1Address + slotNr * COMBO_RECIPE_SIZE, recipe.Data, false);
        }
    }
}
