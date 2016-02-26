Miscellaneous
=============
Commands::

	repo.exe removeref
	repo.exe changenet

There are a couple of additional miscellaneous commands as part of Repographer.  

Removing Old References
-----------------------
``removeref`` removes all references to the given project or assembly throughout the repository.  This can be useful if you refactor code from multiple shared libraries into a single NuGet Package library.  You can run ``switch2nuget`` against one of the references, and then ``removeref`` for the other.

Changing the .NET Framework Version
-----------------------------------
``changenet`` switches all projects in the repository from one .NET version to another.  For example, if you wish to upgrade all of your projects from .NET 4.5 to .NET 4.6.  This is very painful and tedious to do in Visual Studio for a single Solution, let alone for ALL of them!