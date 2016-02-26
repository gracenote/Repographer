Switch to Nuget
===============

Command: ``repo.exe switch2nuget``

Switches either a project or an assembly reference to a NuGet package reference

==============	=======================================================================================
Option			Description
==============	=======================================================================================
directory		Absolute path to the repository
exclude			(optional) Subdirectories to exclude from being searched (recursive), comma-separated
project			(boolean, no text value to supply) Indicates the reference to switch is a project
assembly		(boolean, no text value to supply) Indicates the reference to switch is an assembly
name			Name of the reference being switched
path			(optional) Current path to the project or assembly being switched
packageName		Name of the NuGet package
packagePath		Absolute path to the NuGet package .dll
packageVersion	Version of the new NuGet package
==============	=======================================================================================