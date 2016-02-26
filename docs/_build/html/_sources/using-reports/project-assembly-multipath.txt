Projects or Assemblies Referenced via Multiple Paths
====================================================

Identifies broken reference paths.  This usually happens when projects or assemblies are moved to different locations in the repository, but not all referencing projects or solutions have their HintPaths updated.

Parameters
----------

* Item Name (optional):

	* The name of a specific project or assembly to look up

Columns
-------
* Count

	* The number of distinct paths used to reference an item (based on the item's name)

* Name

	* The name of the project or assembly

* Path

	* Used in the Detail Row (described below)

Grouping
--------
There is one grouping row per project or assembly that is being referenced at more than one path.

Sub-Grouping
------------
Expanding the top-level grouping exposes an additional set of grouping rows.  Each row represents each distinct referencing path, and the count of items that use that path.

Detail Rows
-----------
Each detail row under the sub-grouping represents a project that references the item using the path under which it is grouped.  The Name and Path to the referencing item is provided.  You can describe each detail row as "*This* project (at the path indicated) references *that* item (in the top-level grouping) using *this* path (in the sub-grouping)."

Sorting
-------
The groupings are sorted in descending order by the count of referencing paths, then alphabetically by item name.  The sub-groupings are sorted in descending order by the count of items using the referencing path, then alphabetically by path.  The detail rows are sorted alphabetically by item name.

Usage
-----
This report exposes broken references.  Typically under each grouping you will see the largest number of referencing items using the correct path as the first row in the sub-grouping.  Additional rows in the sub-grouping are projects that have a broken reference path and probably are failing to compile as a result.