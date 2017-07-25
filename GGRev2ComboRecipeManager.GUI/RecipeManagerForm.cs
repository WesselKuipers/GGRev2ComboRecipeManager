using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using GGRev2ComboRecipeManager.Lib;
using GGRev2ComboRecipeManager.Lib.Models;

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

            btnImportSlot1.Enabled = false;
            btnImportSlot2.Enabled = false;
            btnImportSlot3.Enabled = false;
            btnImportSlot4.Enabled = false;
            btnImportSlot5.Enabled = false;

            Directory.CreateDirectory(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recipes"));
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

            var characterNames = ComboRecipes.Select(cr => Enum.IsDefined(typeof(CharacterCode), cr.CharacterCode)
                ? cr.CharacterCode.ToString()
                : CharacterCode.Unknown.ToString()).ToArray();

            lblCharacterSlot1.Text = characterNames[0];
            lblCharacterSlot2.Text = characterNames[1];
            lblCharacterSlot3.Text = characterNames[2];
            lblCharacterSlot4.Text = characterNames[3];
            lblCharacterSlot5.Text = characterNames[4];

            btnExportSlot1.Enabled = btnImportSlot1.Enabled = ComboRecipes[0].CharacterCode != CharacterCode.Unknown;
            btnExportSlot2.Enabled = btnImportSlot2.Enabled = ComboRecipes[1].CharacterCode != CharacterCode.Unknown;
            btnExportSlot3.Enabled = btnImportSlot3.Enabled = ComboRecipes[2].CharacterCode != CharacterCode.Unknown;
            btnExportSlot4.Enabled = btnImportSlot4.Enabled = ComboRecipes[3].CharacterCode != CharacterCode.Unknown;
            btnExportSlot5.Enabled = btnImportSlot5.Enabled = ComboRecipes[4].CharacterCode != CharacterCode.Unknown;
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
            if (Directory.Exists(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recipes")))
            {
                Directory.CreateDirectory(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recipes"));
            }

            var sfd = new SaveFileDialog
            {
                Filter = "Guilty Gear Combo Recipe|*.ggcr",
                FileName = $"Combo{slotNr + 1}.ggcr",
                InitialDirectory = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recipes")
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sfd.FileName, ComboRecipes[slotNr].ToRecipeData());
            }
        }

        private ComboRecipe ImportComboRecipe(int slotNr)
        {
            if (Directory.Exists(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recipes")))
            {
                Directory.CreateDirectory(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recipes"));
            }

            var ofd = new OpenFileDialog
            {
                Filter = "Guilty Gear Combo Recipe|*.ggcr",
                InitialDirectory = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recipes"),
                RestoreDirectory = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var data = File.ReadAllBytes(ofd.FileName);
                if (data.Length != ComboRecipe.SLOT_DATA_SIZE)
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

        private void btnOpenRecipeFolder_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recipes"));
        }
    }
}
