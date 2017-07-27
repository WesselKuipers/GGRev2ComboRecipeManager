using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GGRev2ComboRecipeManager.Lib;
using GGRev2ComboRecipeManager.Lib.Models;

namespace GGRev2ComboRecipeManager.GUI
{
    public partial class RecipeManagerForm : Form
    {
        private List<ComboRecipe> ComboRecipes;
        private List<DummyRecording> DummyRecordings;

        public RecipeManagerForm()
        {
            InitializeComponent();

            Directory.CreateDirectory(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recipes"));
            Directory.CreateDirectory(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recordings"));
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
            btnExportRecipeSlot1.Enabled = recipe.CharacterCode != CharacterCode.Unknown;
        }

        private void btnImportSlot2_Click(object sender, EventArgs e)
        {
            var recipe = ImportComboRecipe(1);
            if (recipe == null) return;

            lblCharacterSlot2.Text = recipe.CharacterCode.ToString();
            btnExportRecipeSlot2.Enabled = recipe.CharacterCode != CharacterCode.Unknown;
        }

        private void btnImportSlot3_Click(object sender, EventArgs e)
        {
            var recipe = ImportComboRecipe(2);
            if (recipe == null) return;

            lblCharacterSlot3.Text = recipe.CharacterCode.ToString();
            btnExportRecipeSlot3.Enabled = recipe.CharacterCode != CharacterCode.Unknown;
        }

        private void btnImportSlot4_Click(object sender, EventArgs e)
        {
            var recipe = ImportComboRecipe(3);
            if (recipe == null) return;

            lblCharacterSlot4.Text = recipe.CharacterCode.ToString();
            btnExportRecipeSlot4.Enabled = recipe.CharacterCode != CharacterCode.Unknown;
        }

        private void btnImportSlot5_Click(object sender, EventArgs e)
        {
            var recipe = ImportComboRecipe(4);
            if (recipe == null) return;

            lblCharacterSlot5.Text = recipe.CharacterCode.ToString();
            btnExportRecipeSlot5.Enabled = recipe.CharacterCode != CharacterCode.Unknown;
        }

        private void btnImportDummySlot1_Click(object sender, EventArgs e)
        {
            var recording = ImportDummyRecording(0);
            if (recording == null) return;

            btnExportDummySlot1.Enabled = recording.RecordingData[0] > 0;
        }

        private void btnImportDummySlot2_Click(object sender, EventArgs e)
        {
            var recording = ImportDummyRecording(1);
            if (recording == null) return;

            btnExportDummySlot2.Enabled = recording.RecordingData[0] > 0;
        }

        private void btnImportDummySlot3_Click(object sender, EventArgs e)
        {
            var recording = ImportDummyRecording(2);
            if (recording == null) return;

            btnExportDummySlot3.Enabled = recording.RecordingData[0] > 0;
        }

        private void btnExportDummySlot1_Click(object sender, EventArgs e)
        {
            ExportDummyRecording(0);
        }

        private void btnExportDummySlot2_Click(object sender, EventArgs e)
        {
            ExportDummyRecording(1);
        }

        private void btnExportDummySlot3_Click(object sender, EventArgs e)
        {
            ExportDummyRecording(2);
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

            btnExportRecipeSlot1.Enabled = btnImportRecipeSlot1.Enabled = ComboRecipes[0].CharacterCode != CharacterCode.Unknown;
            btnExportRecipeSlot2.Enabled = btnImportRecipeSlot2.Enabled = ComboRecipes[1].CharacterCode != CharacterCode.Unknown;
            btnExportRecipeSlot3.Enabled = btnImportRecipeSlot3.Enabled = ComboRecipes[2].CharacterCode != CharacterCode.Unknown;
            btnExportRecipeSlot4.Enabled = btnImportRecipeSlot4.Enabled = ComboRecipes[3].CharacterCode != CharacterCode.Unknown;
            btnExportRecipeSlot5.Enabled = btnImportRecipeSlot5.Enabled = ComboRecipes[4].CharacterCode != CharacterCode.Unknown;
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

        private DummyRecording ImportDummyRecording(int slotNr)
        {
            if (Directory.Exists(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recordings")))
            {
                Directory.CreateDirectory(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recordings"));
            }

            var ofd = new OpenFileDialog
            {
                Filter = "Guilty Gear Dummy Recording|*.ggdr",
                InitialDirectory = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recordings"),
                RestoreDirectory = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var data = File.ReadAllBytes(ofd.FileName);
                if (data.Length != DummyRecording.DUMMYRECORDING_SIZE)
                {
                    MessageBox.Show("Invalid size for GGDR file");
                    return null;
                }

                var recording = new DummyRecording(data);
                DummyRecordingManager.WriteDummyRecording(recording, slotNr);

                return recording;
            }

            return null;
        }

        private void ExportDummyRecording(int slotNr)
        {
            if (Directory.Exists(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recordings")))
            {
                Directory.CreateDirectory(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recordings"));
            }

            var sfd = new SaveFileDialog
            {
                Filter = "Guilty Gear Combo Recipe|*.ggdr",
                FileName = $"Combo{slotNr + 1}.ggdr",
                InitialDirectory = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recordings")
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sfd.FileName, DummyRecordings[slotNr].RecordingData);
            }
        }

        private void btnReadDummyRecordings_Click(object sender, EventArgs e)
        {
            var recordings = DummyRecordingManager.ReadDummyRecordings();

            if (recordings == null)
            {
                MessageBox.Show("Unable to read Dummy Recording data.");
                return;
            }

            DummyRecordings = recordings.ToList();

            btnExportDummySlot1.Enabled = btnImportDummySlot1.Enabled = DummyRecordings[0].RecordingData[4] > 0;
            btnExportDummySlot2.Enabled = btnImportDummySlot2.Enabled = DummyRecordings[1].RecordingData[4] > 0;
            btnExportDummySlot3.Enabled = btnImportDummySlot3.Enabled = DummyRecordings[2].RecordingData[4] > 0;
        }
    }
}
