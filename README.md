# Gump Studio Resurrection

Original VB.net source code was lost so I've decompiled existing Gump Studio binary and converted it to C#.

**This version is to be considered alpha as there are still a lot of things that does not work.** 

If you want to help send me pull request.

## Original description

Gump Studio was designed and written by Bradley Uffner in 2004. It makes extensive use of a modified version of the UOSDK written by Krrios, available at www.RunUO.com. Artwork was created by Melanius, and several more ideas were contributed by the RunUO community.  Special thanks go to DarkStorm of the Wolfpack emulator for helping me to decode unifont.mul, allowing me to displaying UO fonts correctly.

## Goals

Finish the cleanup and make it working with recent UO clients.

## State of the code

Code is almost readable and can be compiled.

- contains some fixes by StaticZ (better unicode font handling and uo format updates)
- old saved *.gump files probably won't work due to changes in source code
- undo points are not always created at right time
- it uses outdated ultima SDK but can read UO files up to version 7.0.23.1
- unicode font loading needs to be dynamic as number of font mul files varies in different versions of UO
- no .uop support at the moment
- save/load for .gump files needs to be changed to xml serializer. Binary serializer has too many issues.
- plugins for RunUO/Sphere are probably broken and outdated
- POL plugin works most of the time but it is also outdated
- app settings needs change to better format.

## Building

Visual Studio 2019 with .Net Desktop development enabled and .net framework 4.8 should be enough.
