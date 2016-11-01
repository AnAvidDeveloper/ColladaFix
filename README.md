# ColladaFix
Utility to fix Collada DAE files so they have local paths to image files

About 70% of the Collada DAE files I've downloaded from the internet contain references to the texture images in the form of absolute paths.  Such paths are inevitably wrong, and the images are almost always in the same folder as the DAE file.  This can be fixed with a  couple minutes effort per model, but why bother? This utility searches for such absolute paths (e.g. init_from elements having values starting with file:///) and strips the path part form them.  A file being processed is automatically backed up (model.dae has a copy named model.dae.bak_20161101T183001 made, for example) and output is written back over the original file

Usage:
ColladaFix model.dae

On Windows machines, you can also simply place the executable (or a shortcut to it) on the desktop and drag Collada files to it.
