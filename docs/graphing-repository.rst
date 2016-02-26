Creating a Repository Graph
===========================

Command: ``repo.exe graph --compile``

The first step to analyzing a large, complex repository is to map out the nodes and relationships.  From the command-line, navigate to the root folder of your repository.  From there, run the above command and supply the arguments as documented.

Repographer identifies five (5) different types of nodes:

1. Solution

	* Represents a .sln file

2. Project

	* Represents either a .csproj file, or a <ProjectReference> from another .csproj file.

3. Assembly

	* Represents a <Reference> to a .dll in a .csproj file.  The referenced .dll does not need to exist.

4. GAC

	* Represents a reference to a GAC assembly in a .csproj file.  Most often these are the .NET Framework assemblies.

5. NuGet Package

	* Same as Assembly, but contains "packages" as a folder in the path.

The identity of a node in the graph is a combination of its Name and its Absolute Path.  As such, two projects or assemblies with the same name but referenced via different paths will show as separate nodes.  This is useful for determining if there is a broken reference somewhere.

Storing the Results
-------------------
The Repographer database has two main tables: Node and Relation.

*Note*: You will want to delete all entries out of both of these tables between runs!

Once this command is run, you can view the results in the various Repographer Reports.