Project and Assembly Dependency Summary
=======================================

Identifies high-impact projects and assemblies to convert to NuGet packages

Parameters
----------
* Item Name (optional):

	* The name of a specific project or assembly to look up

Columns
-------
* Item Name

	* The name of the project or assembly

* Item Path

	* The relative path to the item from the repository root

* Depends On

	* The number of items referenced by this item.  Only applies to Project items.

* Referenced By

	* The number of items referencing this item.

Usage
-----
The default sorting for the report places items with few dependencies that are heavily referenced towards the top.  These represent projects or assemblies that will have the highest impact if refactored into a NuGet package.  

*Note*: This report is intended to be a starting point for analysis and is designed to work in conjunction with the other reports.  Highly-referenced projects should be looked up in the "Reference Solutions by Project" report as well as the "Projects or Assemblies Depended On by Other Projects" report to determine if these references are because it is a shared project across many solutions (undesirable), or if it simply is a very important low-level shared component of a single system (and thus referenced by many other projects in that same solution).