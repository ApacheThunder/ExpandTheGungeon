ExpandTheGungeon - v2.6.4 by Apache Thunder.

Features:

1. Updated version of old Anywhere mod. Use load_flow to load a DungeonFlow. Command syntax is mostly the same as old anywhere mod. Use "load_flow help" in console to get help on how to use this command.

2. Three new secret floors! Both with brand new tilesets pulled from unused tilesets from old versions of the game.

3. Glitch Chests now take player to a full floor with glitched enemies instead of just a simple 3 room floor that goes direct to the boss.

4. Fixed pit generated under sell creep on floor 4. Pit edge decoration of floor tiles near left side of sell creep is no longer missing.

5. Fixed issue intreduced in FTA update that made it trouble-some to drop passives into sell creep in Bello's shop.

6. Added "Junk" enemies. A small chance a floor could have 1 or 2 critters that spawn Junk or Ammo when killed depending on which enemy was spawned. This was an idea from TheTurtleMelon so credits to him for this one.

7. Added custom rooms to room tables used by most floors. This adds new room layouts to the game! Most room designs courtasy of TheTurtleMelon!

8. A glitched elevator has a small chance of appearing on the floor. This takes the player to a glitch chest floor. Can only appear on rooms 1-4 and can only appear once in a run.

9. The few Winchester gun game rooms that were missing icons now have icons. This oversight from original game devs has been fixed.

10. Custom secret rooms. Staring with a ultra tiny secret room that is the smallest possible size (2x2). This secret room can appear in places you won't expect one to fit so be on the look out!

11. Glitch Floors have "corrupted" appearence via addition of sprite objects that use random tile map sprites.

12. Additions/Improvements to DD20 modifiers:

 * Rat's Revenge now spawn rat traps instead of flame traps. 
 * Infamous bug with Zone Control possibly soft locking the player if teleported out of room is fixed.
 * Don't Blink is changed to have enemies have stone effect instead of boring stun effect.
 * Blobulon Rancher can now spawn poisulins as well as the normal blobulins.
 * Two new DD20 modifiers. "Apache Thunder's Revenge". A custom modifier that has most of the Chaos mode mayhem from ChaosGlitchMod!. The second modifier is "Triple Trouble. A modifier for the Trigger Twins boss. As you can guess a third trigger twin will spawn.
 
13. Custom items!:
 * "Baby Good Hammer". An active item companion that summons a Dead Blow Forge Hammer to do your bidding! Thanks to Retrash for providing the item sprite.
 * "Corruption Bomb". A powerful bomb that corrupts the reality around you. Corrupted enemies can no longer attack. May have suprising effects on other objects. Test it out on things if you dare!
 * "Table Tech Assassin". Tables flipped by enemies are betrayed by their tables. Tabled flipped by enemies explode.
 * "Corrupted Junk". Junk warped by corruption. May grant player an obscene amount of a random consumable.
 * "Cronenberg Bullets". Bullets that have a chance to transmorgify enemies into a horrible cronenberg abomination. Sprite work courtasy of Neighborino!
 * "Mimiclay". Special item dropped by custom boss on custom secret floor. One time use active item. Clones another active or passive when used while said active/passive is spawned/near the player. This item idea/code was created thanks to Retrash.
 * "The Lead Key". Special active item taht will teleport the player to a new room not normally connected to the floor. Small chance of beign a shop, shrine, or chest room.
 * "The Bullet Kin Gun". Fires Bullet Kin....No further explenation needed.
 * "Baby Sitter". Companion item that gives you the COOP cultist as a non playable friend. Like Cop, he can die. When this happes you get curse and damage up.
 * "Rock Slide". Causes rocks to fall on your enemies! Amount of rocks is random. Sometimes all enemies get targeted while other times only a few do.
 * "Pow Block". Flips enemies and makes them easier to kill. Flipped enemies can no longer attack.
 
14. Certain NPCs and objects and even a copy of the player can appear as enemies on glitch floors.

15. An unused trap "Alarm Mushroom" has been revived and made usable. It currently appears in random rooms on the Mines, the floor the trap object was intended to appear on.

16. New type of elevator added. A small elevator that can take the player to another room on the floor. Used as a entrance elevator to the secret glitched elevator room on Hollow.

17. 1 new Custom Boss! Can be found on my custom secret floor!

Compiling and versions of Enter the Gungeon required:

This source code uses C#. Visual Studio 2015 or newer recommended.
This mod is intended for post AG&D versions of Enter the Gungeon and the Farewell to Arms update. Please ensure your game is up to date before attempting to use this mod.


Credits:

* KyleTheScientist, Zatherz, Abe Clancy, and PlaguedPixel for their help/code for improving/making certain features possible.
* TheTurtleMelon for name idea of mod and for custom room designs used on normal floors.
* Retrash for the Baby Good Hammer item sprite, Wooden Crest sprite, improvements to Jungle tree sprite, and for some room designs on Belly floor.
* Charcola for the Table Tech Assassin sprite design.
* Neighborino for help making sprites for the Cronenberg monsters that Cronenberg Bullets transform enemies into. He has also provided some additional room designs and revised versions of Cronenberg sprites for aggressive versions found on Belly.
* Goldenrevolver for helping adding the West Bros Boss to Old West and verious other improvements to existing assets.
* blazeykay for creating the sprite used by the item "The Third Eye".
* SmilingMustache for the Clown kin sprites! :D
* Dallan for the item sprite used for Portable Elevator.
* SomeBunny for the portable ship item sprite.
* https://github.com/khalladay/GlitchFX - One of the shaders used for transition fx on The Lead Key item.
* https://github.com/staffantan/unity-vhsglitch - Shader used for Old West Boss intro and glitch floors.
* C4ndy_cane for all the custom level music added to this mod!