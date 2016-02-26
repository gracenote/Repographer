Referencing Solutions by Project
================================

Identifies projects that are members of multiple Solutions, aka "Multi-homed Projects."  This is generally considered a bad practice.

Parameters
----------

* Solution (optional):

	* Filters the list of projects to those that are members of the given Solution

* Project (optional):

	* Filters the report by project name.

* Homing Option:

	* Single shows projects that a member of only one solution.  Multi shows projects that are members of multiple solutions.  All shows all projects regardless of solution membership. 

Columns
-------
* Name

	* The name of the project

* Member Of

	* The number of solutions that have the given project as a member

Grouping
--------
There is one grouping row per project in the repository.  The path to that project is also provided.

Detail Rows
-----------
Each detail row under the grouping represents a solution that has the project as a member.  The name and path to the .sln file is provided.

Sorting
-------
The groupings are sorted in descending order by the count of solutions that have the project as a member, then alphabetically by project name.  

Usage
-----
This is a very powerful report for determining projects to extract into NuGet packages.  Typically, any project that is a member of multiple solutions is a very good candidate to be a NuGet package.  Additionally, this report is very helpful for an engineer that is working on a specific solution.  The engineer can filter the report by that solution, and then use the Homing Option to determine which projects are "owned" by that solution, and which ones are shared with others.  If changes are required in a shared project, the report can be filtered to show all solutions that have that project as a member.  Each of these solutions will need to be recompiled to determine if changes to the shared project introduced any breakages.