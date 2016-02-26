Extracting Shared Libraries
===========================

The amount of time and effort it will take to extract shared libraries out of your repository and into NuGet Package repositories depends on just how big and complex your particular ball of mud is.  If you only have a few projects referenced by multiple solutions, and they are all nicely modularized as a "framework" of sorts, this effort can be completed in under an hour.  If you have several hundred projects cross-referenced by several hundred solutions, this process can take several months of effort.  Either way, the process itself can be broken down as:

1. Identify the shared project(s) to extract using the Repographer Reports

2. Determine if you wish to extract just the single project into its own repository, or cluster a few related projects that version together.  

	* For example, perhaps you have base Data Access project with a few child projects holding specific database implementations.  Any changes to the base Data Access classes will want to immediately be reflected in the derived children, so it may make sense to keep all of these projects together.  You can still deploy multiple NuGet Packages out of a single repository (one per each database implementation), as that is more a function of your build and package process than the actual place the projects live.

3. Move all of the projects you wish to extract into its own repository under one folder of the current repository.  Do not worry about breaking reference paths in doing so, as you will use Repographer to fix those for you.

4. Extract that folder out into its own repository.  Different SCM systems (i.e. Mercurial, Git, SVN, TFS) have different ways to achieve this.  For example, in Git you would use the ``git subtree`` command.

5. Create and publish the NuGet Packages from the new repository, either manually or via an automated build and deploy process.  See the NuGet documentation for instructions on creating packages.

6. Use Repographer to switch existing project references to NuGet references in the ball of mud repository, and remove those projects as members of the various .sln files (see the next section).

7. Remove the old extracted project code from the ball of mud repository.

8. Commit!