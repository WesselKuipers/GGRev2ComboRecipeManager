# Combo Recipe Manager for Rev2
Update 2.02 of GG Rev2 added the option to record and store 'combo recipes' which are a more limited but customizable form of combo trials. ArcSys however didn't give us the option to save these combo recipes and share them with are friends, rendering the option to record your own trials and practice them fairly pointless. After all, you already performed it!

This tool allows you to export and import combo recipes stored in slots 1 through 5. Recipes are stored in `1028kB` sized files, containing information about which character the combo is for and room for up to 31 moves.

## How to use
Make sure you **unzipped** the tool and put it in a location that has read/write access.
Make sure Guilty Gear Xrd Rev2 is running.

**Combo Recipes**
1. Press the `Read Combo Recipes` button to read the currently stored combos. If all went well, the import and export buttons will be enabled and the character names for your currently saved combos will be displayed.
3. Record your combo.
4. Save your combo to one of the slots
5. Hit `Read Combo Recipes` again to refresh the currently stored combo recipes
6. Press the `Export` button next to the corresponding slot to export the combo file, resulting in a `.ggcr` file
7. Press the `Import` button next to a slot to import a combo from a `.ggcr` file.

**Dummy Recordings**
1. Press the `Read Dummy Recordings` button to read the currently stored dummy recordings.
2. Record your dummy and note which slot you selected
3. Hit `Read Dummy Recordings` again to refresh the currently stored combo recipes.
4. Press the `Export` button next to the corresponding slot to export the dummy recording, resulting in a `.ggdr` file.
5. Press the `Import` button next to a slot to import a dummy recording from a `.ggdr` file.

## Download
The latest release can be found [here](https://github.com/WesselKuipers/GGRev2ComboRecipeManager/releases).

----------

Disclaimer: This tool was put together as a quick and dirty way to share combos. It is by no means foolproof and probably has some bugs, so let's slap a "use at your own risk" label on this just in case.
Feel free to report bugs or even submit a pull request yourself!
