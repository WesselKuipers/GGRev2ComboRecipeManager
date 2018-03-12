# Combo Recipe Manager for Rev2
Update 2.02 of GG Rev2 added the option to record and store 'combo recipes' which are a more limited but customizable form of combo trials. ArcSys however didn't give us the option to save these combo recipes and share them with are friends, rendering the option to record your own trials and practice them fairly pointless. After all, you already performed it!

This tool allows you to export and import combo recipes stored in slots 1 through 5. Recipes are stored in `1028kB` sized files, containing information about which character the combo is for and room for up to 31 moves.

Additionally, this tool can exprt and import dummy recordings stored in slots 1 through 3. These dummy recordings contain raw input data for each input (directions and buttons) on each frame, and are stored in `4804kB` sized files. 

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


## Finding pointer for dummy recordings
1. Load up training mode in GG
2. Load up Cheat Engine and attach to "GuiltyGearXrd.exe" process
3. Record and save a dummy recording on slot 1
4. Search for `0` or `1` depending on which side your dummy started recording (0 for left side, 1 for right side)
5. Record and save a dummy recording on the opposite side
6. Repeat until you're pretty sure you got the right address. This can be confirmed by highlighting the address and hitting ctrl B to view memory, the first 4 bytes are occupied by the direction, 4 bytes after this store the length of the recording
7. Right-click on address for direction -> Pointerscan -> OK
8. Restart GG and re-do steps 1-7 until you end up with a pointer that's always consistent

## Finding pointer for combo recipes
1. Load up training mode in GG
2. Load up Cheat Engine and attach to "GuiltyGearXrd.exe" process
3. Record and save a combo on slot 1 using Venom and start it with a 2S
4. Search for byte array `09 00 00 00 4E 6D 6C 41 74 6B 32 43` in memory
5. Right-click on this address -> Pointerscan -> OK
6. Restart GG and re-do steps 1-7 until you end up with a pointer that's always consistent

----------

Disclaimer: This tool was put together as a quick and dirty way to share combos. It is by no means foolproof and probably has some bugs, so let's slap a "use at your own risk" label on this just in case.
Feel free to report bugs or even submit a pull request yourself!
