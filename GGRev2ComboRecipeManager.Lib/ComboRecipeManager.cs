using GGRev2ComboRecipeManager.Lib.Models;
using static GGRev2ComboRecipeManager.Lib.Models.ComboRecipe;

namespace GGRev2ComboRecipeManager.Lib
{
    public class ComboRecipeManager
    {
        const int SLOT1_OFFSET_POINTER = 0x00BD3E94;
        const string PROCESS_NAME = "GuiltyGearXrd";

        public static ComboRecipe[] ReadComboRecipes()
        {
            var comboRecipeData = ProcessMemoryManager.ReadProcessMemory(PROCESS_NAME, SLOT1_OFFSET_POINTER, SLOT_DATA_SIZE * 5, true);

            var combo1 = new ComboRecipe(comboRecipeData, 0);
            var combo2 = new ComboRecipe(comboRecipeData, 1);
            var combo3 = new ComboRecipe(comboRecipeData, 2);
            var combo4 = new ComboRecipe(comboRecipeData, 3);
            var combo5 = new ComboRecipe(comboRecipeData, 4);

            return new[] {combo1, combo2, combo3, combo4, combo5};
        }

        public static ComboRecipe ReadComboRecipe(int slotNr)
        {
            var data = ProcessMemoryManager.ReadProcessMemory(PROCESS_NAME, SLOT1_OFFSET_POINTER, SLOT_DATA_SIZE, true, slotNr * SLOT_DATA_SIZE);

            return data != null ? new ComboRecipe(data) : null;
        }

        public static void WriteRecipe(ComboRecipe recipe, int slotNr = 0)
        {
            var data = recipe.ToRecipeData();

            ProcessMemoryManager.WriteProcessMemory(PROCESS_NAME, SLOT1_OFFSET_POINTER, data, true, SLOT_DATA_SIZE * slotNr);
        }
    }
}
