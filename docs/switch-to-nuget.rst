Switching References to NuGet Packages
======================================
Commands::

	repo.exe switch2nuget
	repo.exe removeproj

The process of switching project and assembly references to NuGet Package references is actually quite trivial.  You simply run the ``switch2nuget`` command, supply the name of the reference to switch (as viewed in the "References" list for any given project), indicate the NuGet package version number, and provide the absolute path to where the .dll lives in the main "packages" folder.

Ok, well, that last part is a *little* tricky.  One relatively simple way to figure that out is to run ``nuget.exe Install-Package`` on the command line to some temporary folder for the package you are switching to.  Take a look in the directory structure created by that package to find the .dll you care about.  Then just use that as the relative path from your central "packages" folder to supply as an absolute path to the ``switch2nuget`` command.

``switch2nuget`` takes care of updating all of the reference HintPath entries, as well as making sure the packages.config file for each project is present and correct.

Once the project references are all switched, you will want to remove the old shared project as a member from all .sln files in your ball of mud repository.  This is done using the ``removeproj`` command - see the documentation for usage.