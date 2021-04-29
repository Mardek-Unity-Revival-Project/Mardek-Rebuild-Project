# Venator's Branch

This serves as a) a test that I set up my branch correctly, and b) a short description of my plans for handling the portraits. This file can be deleted if it gets in the way, but at the moment I'm using it for notetaking.

I can get the animation data as an xml file (involves JPEXS and Adobe Animate - presumably there's a better way). I can convert this to an animation asset with CreateAsset(). Then I just have to write a MonoBehavior script to animate the portraits and handle changing expressions.

I intend to have a Portrait class (which inherits from Monobehavior), and then child classes for each type of portrait (human male, human female, Annunaki, etc.). I'll start with the Annunaki since they are relatively simple (no morph shapes, few expressions). 

I still am not clear on how the morph shapes work - I'll look more into that later. It matters for the blinking animation, and maybe other things.

I also might need a mask to cleanly cut off the bottom of the portraits - some of the assets extend farther than they should.

Also reminder to myself to check for the background glow for some characters (Rohoph, maybe others). Does it correspond to whether their text glows? (I.e. does Clavis glow, and if so, does he only glow once he reveals himself?)

I assume, once I've set up the portrait class scripts, that I should create prefabs for each character? I'm not completely clear on when to make prefabs yet. I'll also have to go in to each svg and adjust the control point, since they're all in different spots based on the dimensions of the asset. This won't be tedious at all.