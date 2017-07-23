using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using GGRev2ComboRecipeManager.Lib;

namespace GGRev2ComboRecipeManager.GUI
{
    public partial class RecipeManagerForm : Form
    {
        private List<ComboRecipe> ComboRecipes;

        public RecipeManagerForm()
        {
            InitializeComponent();

            btnExportSlot1.Enabled = false;
            btnExportSlot2.Enabled = false;
            btnExportSlot3.Enabled = false;
            btnExportSlot4.Enabled = false;
            btnExportSlot5.Enabled = false;
        }

        private void btnReadRecipes_Click(object sender, EventArgs e)
        {
            var recipes = ComboRecipeManager.ReadComboRecipes();

            if (recipes == null)
            {
                MessageBox.Show("Unable to read Combo Recipe data.");
                return;
            }

            ComboRecipes = recipes.ToList();

            lblCharacterSlot1.Text = ComboRecipes[0].CharacterCode.ToString();
            lblCharacterSlot2.Text = ComboRecipes[1].CharacterCode.ToString();
            lblCharacterSlot3.Text = ComboRecipes[2].CharacterCode.ToString();
            lblCharacterSlot4.Text = ComboRecipes[3].CharacterCode.ToString();
            lblCharacterSlot5.Text = ComboRecipes[4].CharacterCode.ToString();

            btnExportSlot1.Enabled = ComboRecipes[0].CharacterCode != CharacterCode.Unknown;
            btnExportSlot2.Enabled = ComboRecipes[1].CharacterCode != CharacterCode.Unknown;
            btnExportSlot3.Enabled = ComboRecipes[2].CharacterCode != CharacterCode.Unknown;
            btnExportSlot4.Enabled = ComboRecipes[3].CharacterCode != CharacterCode.Unknown;
            btnExportSlot5.Enabled = ComboRecipes[4].CharacterCode != CharacterCode.Unknown;
        }

        private void btnExportSlot1_Click(object sender, EventArgs e)
        {
            ExportComboRecipe(0);
        }

        private void btnExportSlot2_Click(object sender, EventArgs e)
        {
            ExportComboRecipe(1);
        }

        private void btnExportSlot3_Click(object sender, EventArgs e)
        {
            ExportComboRecipe(2);
        }

        private void btnExportSlot4_Click(object sender, EventArgs e)
        {
            ExportComboRecipe(3);
        }

        private void btnExportSlot5_Click(object sender, EventArgs e)
        {
            ExportComboRecipe(4);
        }

        private void btnImportSlot1_Click(object sender, EventArgs e)
        {
            var recipe = ImportComboRecipe(0);
            if (recipe == null) return;

            lblCharacterSlot1.Text = recipe.CharacterCode.ToString();
            btnExportSlot1.Enabled = recipe.CharacterCode != CharacterCode.Unknown;
        }

        private void btnImportSlot2_Click(object sender, EventArgs e)
        {
            var recipe = ImportComboRecipe(1);
            if (recipe == null) return;

            lblCharacterSlot2.Text = recipe.CharacterCode.ToString();
            btnExportSlot2.Enabled = recipe.CharacterCode != CharacterCode.Unknown;
        }

        private void btnImportSlot3_Click(object sender, EventArgs e)
        {
            var recipe = ImportComboRecipe(2);
            if (recipe == null) return;

            lblCharacterSlot3.Text = recipe.CharacterCode.ToString();
            btnExportSlot3.Enabled = recipe.CharacterCode != CharacterCode.Unknown;
        }

        private void btnImportSlot4_Click(object sender, EventArgs e)
        {
            var recipe = ImportComboRecipe(3);
            if (recipe == null) return;

            lblCharacterSlot4.Text = recipe.CharacterCode.ToString();
            btnExportSlot4.Enabled = recipe.CharacterCode != CharacterCode.Unknown;
        }

        private void btnImportSlot5_Click(object sender, EventArgs e)
        {
            var recipe = ImportComboRecipe(4);
            if (recipe == null) return;

            lblCharacterSlot5.Text = recipe.CharacterCode.ToString();
            btnExportSlot5.Enabled = recipe.CharacterCode != CharacterCode.Unknown;
        }

        private void ExportComboRecipe(int slotNr)
        {
            var sfd = new SaveFileDialog
            {
                Filter = "Guilty Gear Combo Recipe|*.ggcr",
                FileName = $"Combo{slotNr + 1}.ggcr"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sfd.FileName, ComboRecipes[slotNr].ToRecipeData());
            }
        }

        private ComboRecipe ImportComboRecipe(int slotNr)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Guilty Gear Combo Recipe|*.ggcr"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var data = File.ReadAllBytes(ofd.FileName);
                if (data.Length != ComboRecipeManager.SLOT_DATA_SIZE)
                {
                    MessageBox.Show("Invalid size for GGCR file");
                    return null;
                }

                var recipe = new ComboRecipe(data);
                ComboRecipeManager.WriteRecipe(recipe, slotNr);

                return recipe;
            }

            return null;
        }
    }
}
