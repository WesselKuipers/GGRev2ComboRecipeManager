# Combo Recipe Manager for Rev2
Update 2.02 of GG Rev2 added the option to record and store 'combo recipes' which are a more limited but customizable form of combo trials. ArcSys however didn't give us the option to save these combo recipes and share them with are friends, rendering the option to record your own trials and practice them fairly pointless. After all, you already performed it!

This tool allows you to export and import combo recipes stored in slots 1 through 5. Recipes are stored in `1028kB` sized files, containing information about which character the combo is for and room for up to 31 moves.

## How to use
1. Make sure Guilty Gear Xrd Rev2 is running
2. Press the `Read Combo Recipes` button to read the currently stored combos.
3. If all went well, the import and export buttons will be enabled and the character names for your currently saved combos will be displayed
4. Record your combo.
5. Save your combo to one of the slots
6. Hit `Read Combo Recipes` again to refresh the currently stored combo recipes
7. Press the `Export` button next to the corresponding slot to export the combo file, resulting in a `.ggcr` file
8. Press the `Import` button next to a slot to import a combo from a `.ggcr` file

Disclaimer: This tool was put together as a quick and dirty way to share combos. It is by no means foolproof and probably has some bugs, so let's slap a "use at your own risk" label on this just in case.
Feel free to report bugs or even submit a pull request yourself!
