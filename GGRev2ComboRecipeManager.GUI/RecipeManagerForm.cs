using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using GGRev2ComboRecipeManager.Lib;
using GGRev2ComboRecipeManager.Lib.Models;

namespace GGRev2ComboRecipeManager.GUI
{
    public partial class RecipeManagerForm : Form
    {
        private List<ComboRecipe> ComboRecipes;
        private List<DummyRecording> DummyRecordings;
        private ModuleManager mm;

        public RecipeManagerForm()
        {
            InitializeComponent();

            mm = new ModuleManager();

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

            lblCharacterSlot1.Text = recipe.Data.CharacterCode.ToString();
            btnExportRecipeSlot1.Enabled = recipe.Data.CharacterCode != CharacterCode.Unknown;
        }

        private void btnImportSlot2_Click(object sender, EventArgs e)
        {
            var recipe = ImportComboRecipe(1);
            if (recipe == null) return;

            lblCharacterSlot2.Text = recipe.Data.CharacterCode.ToString();
            btnExportRecipeSlot2.Enabled = recipe.Data.CharacterCode != CharacterCode.Unknown;
        }

        private void btnImportSlot3_Click(object sender, EventArgs e)
        {
            var recipe = ImportComboRecipe(2);
            if (recipe == null) return;

            lblCharacterSlot3.Text = recipe.Data.CharacterCode.ToString();
            btnExportRecipeSlot3.Enabled = recipe.Data.CharacterCode != CharacterCode.Unknown;
        }

        private void btnImportSlot4_Click(object sender, EventArgs e)
        {
            var recipe = ImportComboRecipe(3);
            if (recipe == null) return;

            lblCharacterSlot4.Text = recipe.Data.CharacterCode.ToString();
            btnExportRecipeSlot4.Enabled = recipe.Data.CharacterCode != CharacterCode.Unknown;
        }

        private void btnImportSlot5_Click(object sender, EventArgs e)
        {
            var recipe = ImportComboRecipe(4);
            if (recipe == null) return;

            lblCharacterSlot5.Text = recipe.Data.CharacterCode.ToString();
            btnExportRecipeSlot5.Enabled = recipe.Data.CharacterCode != CharacterCode.Unknown;
        }

        private void btnImportDummySlot1_Click(object sender, EventArgs e)
        {
            var recording = ImportDummyRecording(0);
            if (recording == null) return;

            btnExportDummySlot1.Enabled = recording.Data.Length > 0;
        }

        private void btnImportDummySlot2_Click(object sender, EventArgs e)
        {
            var recording = ImportDummyRecording(1);
            if (recording == null) return;

            btnExportDummySlot2.Enabled = recording.Data.Length > 0;
        }

        private void btnImportDummySlot3_Click(object sender, EventArgs e)
        {
            var recording = ImportDummyRecording(2);
            if (recording == null) return;

            btnExportDummySlot3.Enabled = recording.Data.Length > 0;
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

        private void btnClearDummySlot1_Click(object sender, EventArgs e)
        {
            ClearDummyRecordingData(0);
            btnExportDummySlot1.Enabled = false;
        }

        private void btnClearDummySlot2_Click(object sender, EventArgs e)
        {
            ClearDummyRecordingData(1);
            btnExportDummySlot2.Enabled = false;
        }

        private void btnClearDummySlot3_Click(object sender, EventArgs e)
        {
            ClearDummyRecordingData(2);
            btnExportDummySlot3.Enabled = false;
        }

        private void btnOpenRecipeFolder_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recipes"));
        }

        private void btnOpenRecordingFolder_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recordings"));
        }

        private void btnReadRecipes_Click(object sender, EventArgs e)
        {
            var recipes = mm.ComboRecipeManager.ReadComboRecipes();

            if (recipes == null)
            {
                MessageBox.Show("Unable to read Combo Recipe data.");
                return;
            }

            ComboRecipes = recipes.ToList();

            var characterNames = ComboRecipes.Select(cr => Enum.IsDefined(typeof(CharacterCode), cr.Data.CharacterCode)
                ? cr.Data.CharacterCode.ToString()
                : CharacterCode.Unknown.ToString()).ToArray();

            lblCharacterSlot1.Text = characterNames[0];
            lblCharacterSlot2.Text = characterNames[1];
            lblCharacterSlot3.Text = characterNames[2];
            lblCharacterSlot4.Text = characterNames[3];
            lblCharacterSlot5.Text = characterNames[4];

            btnImportRecipeSlot1.Enabled = true;
            btnImportRecipeSlot2.Enabled = true;
            btnImportRecipeSlot3.Enabled = true;
            btnImportRecipeSlot4.Enabled = true;
            btnImportRecipeSlot5.Enabled = true;

            btnExportRecipeSlot1.Enabled = ComboRecipes[0].Data.CharacterCode != CharacterCode.Unknown;
            btnExportRecipeSlot2.Enabled = ComboRecipes[1].Data.CharacterCode != CharacterCode.Unknown;
            btnExportRecipeSlot3.Enabled = ComboRecipes[2].Data.CharacterCode != CharacterCode.Unknown;
            btnExportRecipeSlot4.Enabled = ComboRecipes[3].Data.CharacterCode != CharacterCode.Unknown;
            btnExportRecipeSlot5.Enabled = ComboRecipes[4].Data.CharacterCode != CharacterCode.Unknown;
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
                File.WriteAllBytes(sfd.FileName, ComboRecipes[slotNr].Data.ToByteArray());
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
                InitialDirectory = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recipes")
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var data = File.ReadAllBytes(ofd.FileName);
                if (data.Length != ComboRecipeManager.COMBO_RECIPE_SIZE)
                {
                    MessageBox.Show("Invalid size for GGCR file");
                    return null;
                }

                var recipe = new ComboRecipe(ComboRecipeData.FromByteArray(data));
                mm.ComboRecipeManager.WriteRecipe(recipe, slotNr);

                return recipe;
            }

            return null;
        }

        private void ClearDummyRecordingData(int slotNr)
        {
            var emptyRecording = new DummyRecording(DummyRecordingData.FromByteArray(new byte[DummyRecordingManager.DUMMY_RECORDING_SIZE]));
            mm.DummyRecordingManager.WriteDummyRecording(emptyRecording, slotNr);
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
                InitialDirectory = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recordings")
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var data = File.ReadAllBytes(ofd.FileName);
                if (data.Length != DummyRecordingManager.DUMMY_RECORDING_SIZE)
                {
                    MessageBox.Show("Invalid size for GGDR file");
                    return null;
                }

                var recording = new DummyRecording(DummyRecordingData.FromByteArray(data));
                mm.DummyRecordingManager.WriteDummyRecording(recording, slotNr);

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
                Filter = "Guilty Gear Dummy Recording|*.ggdr",
                FileName = $"Combo{slotNr + 1}.ggdr",
                InitialDirectory = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "\\Recordings")
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sfd.FileName, DummyRecordings[slotNr].Data.ToByteArray());
            }
        }

        private void btnReadDummyRecordings_Click(object sender, EventArgs e)
        {
            var recordings = mm.DummyRecordingManager.ReadDummyRecordings();

            if (recordings == null)
            {
                MessageBox.Show("Unable to read Dummy Recording data.");
                return;
            }

            DummyRecordings = recordings.ToList();

            btnImportDummySlot1.Enabled = true;
            btnImportDummySlot2.Enabled = true;
            btnImportDummySlot3.Enabled = true;

            btnExportDummySlot1.Enabled = DummyRecordings[0].Data.Length > 0;
            btnExportDummySlot2.Enabled = DummyRecordings[1].Data.Length > 0;
            btnExportDummySlot3.Enabled = DummyRecordings[2].Data.Length > 0;

            btnClearDummySlot1.Enabled = true;
            btnClearDummySlot2.Enabled = true;
            btnClearDummySlot3.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Button to test dirty character swapper
            var t = 0x018701E1;
            byte val = 0b1;
            byte val2 = 0b0;

            mm._sharp.Write(new IntPtr(t), val, false);
            Thread.Sleep(17);
            mm._sharp.Write(new IntPtr(t), val2, false);

            var ptr = new IntPtr(0x1BDCE04); // this one isn't static, will probably crash the game
            var r = new Random();

            var c1 = r.Next(0x18);
            var c2 = r.Next(0x18);

            mm._sharp.Write(ptr, c1);
            mm._sharp.Write(ptr + 4, c2);
            Thread.Sleep(256 + 16);

            mm._sharp.Write(new IntPtr(t), val, false);
            Thread.Sleep(17);
            mm._sharp.Write(new IntPtr(t), val2, false);
        }
    }
}
