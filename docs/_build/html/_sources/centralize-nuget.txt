Centralizing NuGet Packages
===========================
By default, NuGet creates a "packages" folder in the directory that houses the .sln file from where the NuGet Package reference is created.  This design makes the assumption that the .sln is housed in the top-most directory for the system, and that all child projects reside in directories beneath this top-level directory.  In this way, all external assembly dependencies referenced by projects within that Solution share the NuGet package binaries.

In "big ball of mud" repositories, this is usually not the case.  Instead, you need to create a single top-level "packages" location at the root of the repository, and have all projects therein reference NuGet packages at this location.  This creates a predictable location by which all projects will find their external dependencies, allow projects referenced by multiple Solutions to use NuGet package references regardless of which .sln location was initially used to create the reference, and allow re-use of NuGet Packages across all projects with the reference rather than each project potentially downloading its own copy of the package.

Luckily, NuGet now provides an option to change the default "packages" location behavior (as of v2.1).  You can find the documentation here:
http://docs.nuget.org/release-notes/nuget-2.1

In a nutshell, place a ``nuget.config`` file at the root of your repository, and set a "repositoryPath" value to point at a top-level "packages" folder that you create.

Update NuGet Package Reference Location
---------------------------------------
Commands: 
	``repo.exe purgefolder``
	``repo.exe movenuget``

Unfortunately, neither NuGet nor Visual Studio provide a way to update the HintPath for all NuGet Package references in projects to the new location.  Further, the release notes state
	
	Note that if you have an existing packages folder underneath your solution root, you will need to delete it before NuGet will place packages in the new location.

So there are two commands we need to use.  The first is to remove all of the current "packages" folders that might exist within your repository, and the second is to update all of the current NuGet Package reference HintPaths in the project files to the new packages folder.

Run the "purge folder" command first, supplying "packages" as the folder (it will recursively look for all instances of the packages folder, regardless of nesting).  After, run the "move nuget" command and specify the absolute path to the new location (per the documentation for the command).  This will look for all current HintPaths that have "packages" in the text and update them accordingly.