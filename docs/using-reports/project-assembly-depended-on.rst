Projects or Assemblies Depended On by Other Projects
====================================================

Answers the question, "What projects have a reference to this item, and where do they live in the repository?"

Parameters
----------
* Projects (true/false):

	* True to include projects in the report, false to exclude

* Assemblie (true/false):

	* True to include assemblies in the report, false to exclude

* Item Name (optional):

	* The name of a specific project or assembly to look up

Columns
-------
* Item Name

	* The name of the project or assembly

* Path

	* The relative path to the item from the repository root

Grouping
--------
There is one grouping row per project or assembly in the repository.  This row contains the name of the item and the count of the referencing projects.

Detail Rows
-----------
Each detail row under the grouping represents a project that references the grouping item, and the path to that project.

Sorting
-------
The groupings are sorted in descending order by the count of reference items.  The detail rows are sorted alphabetically by Path, so that projects under a similar directory hierarchy will cluster together.

Usage
-----
This report is helpful when an engineer is making a change to a heavily-referenced project, but they are uncertain about all of the reference projects that will be impacted by that change.  It also helps to expose when projects have a reference to an item unintentionally that was not otherwise apparent or intuitive.