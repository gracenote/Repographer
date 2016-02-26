Visualize the Repository Graph
==============================

Command: ``repo.exe graph --export``

Repographer supports exporting the graph into GEXF format, and XML-based standard for representing the nodes and relationships.  Run the above command with the documented arguments, and it will output a repository.gexf file.

This file can be imported into any Graph Visualizer application that supports the GEXF format.  One popular application is Gephi, which can be used to run Force Atlas or other popular node distribution algorithms to generate some pretty interesting results.

*Note*: Only nodes of type ``Solution``, ``Project``, and ``Assembly`` are represented in the exported results.  This is because all projects will ultimately share the .NET Framework in common, and the intent of the analysis is to examine projects that are good candidates to be extracted into NuGet packages.  So, filtering out nodes of type ``GAC`` and ``NuGet Package`` ensure properly isolated systems will show as such in the exported results.

Additionally, any nodes of type ``Assembly`` ought to be looked up to see if there is a public NuGet package available on nuget.org.  If not, you should consider creating your own Unofficial package and hosting on your local NuGet server rather than storing the binary files in the repository itself.

Some interesting properties you can set in Gephi:

1. Set the node color based on the NodeType

2. Set the node size based on the InDegrees, OutDegrees, or both.

	* InDegrees make Project and Assembly nodes larger based on the number of references to it

	* OutDegrees make Solutions and Projects larger based on the number of dependencies they have

	* Both will make nodes in general larger based on their overall importance in the graph.