.. Repographer documentation master file, created by
   sphinx-quickstart on Tue Feb 23 13:54:38 2016.
   You can adapt this file completely to your liking, but it should at least
   contain the root `toctree` directive.

Repographer Documentation
=========================
Repographer is a command-line tool to assist in breaking up large Microsoft .NET repositories.  In an ideal configuration, every .NET system (whether desktop, web, or service) is contained within its own repository, uses one .sln file per independently deployable unit of the system, and has each .sln only containing projects that define that unit.  Projects that are shared by multiple units, or even multiple systems, are done so via versioned NuGet packages.  This allows each deployable unit to be versioned independently, and to define the NuGet package versions on which it depends independent of updates made to those packages.

Many companies, however, still have one very large .NET repository consisting of many .sln files.  These .sln files not only contain code that defines the system, but also cross-referenced shared projects that often are scattered throughout the repository.  It is difficult to distinguish the code that is "owned" by any one system vs. code that is shared across multiple systems.  This causes problems when breaking changes are made to a shared project without the awareness of all .sln files that reference that project.  Likewise when shared project locations are moved, breaking reference paths that are difficult to identify.  This, in effect, makes the repository a "big ball of mud" and the stability of any one given system as it progresses through the development pipeline highly unpredictable.

Features
========
Repographer has commands for the following:

* Change the NuGet package location to a central (relative) path in ALL projects

	* This is helpful if you want to use a nuget.config to provide a root-level "global" packages folder

* Delete all folders that have a given name out of the repository, regardless of location

	* Again, helpful if you want to purge out the individual "packages" folders created by NuGet by default to switch to a central "packages" folder.

* Change a project reference to a NuGet Package reference in ALL projects

	* For when you extract out a shared project into a NuGet package, and want to update all existing references to that project accordingly.

* Remove a project from ALL .sln files in a repository

	* After switching a shared project to a NuGet package reference, you'll want to remove that project from the respective .sln files.

* Remove a reference from ALL projects in a repository
 	
 	* In case you ever have an old, obsolete reference to a non-existant project after refactoring efforts.

Contents:

.. toctree::
   :maxdepth: 1

   installation
   graphing-repository
   visualize-repository
   using-reports/index
   centralize-nuget
   extract-shared-libraries
   switch-to-nuget
   miscellaneous
   command-ref/index