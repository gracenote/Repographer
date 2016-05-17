# Repographer

Repographer is a command-line tool to assist in breaking up large Microsoft .NET repositories to use artifact-based dependencies (NuGet Package References) for shared libraries over direct source code references (Project References).

## Features

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

## Documentation

Documentation is hosted over at [Read The Docs](http://repographer.readthedocs.io/en/latest/).

## License

Repographer source is made available under the Apache License 2.0.  See the [LICENSE.md](LICENSE.md) for more information.

## Support

We recommend asking questions on Stack Overflow using the `repographer` tag.

## Contributing

If you wish to contribute, create a fork of the repository and open a pull request with your changes.  Take a look at one of the existing Modules under the main Repographer project - the easiest way to add new actions is to copy one of these in its entirety and rename the classes/namespaces accordingly.  Once you have your new action's Module class defined, add it to the ContainerBuilder in Program.cs, and AutoFac will take care of wiring up the rest.