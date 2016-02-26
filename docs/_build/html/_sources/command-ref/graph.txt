Graph
=====

Command: ``repo.exe graph``

Used to either compile or export the dependency graph.

=========	=======================================================================================
Option		Description
=========	=======================================================================================
compile		Compiles the dependency graph and stores in the database
export		Exports the dependency graph from the database and outputs into a repository.gexf file
=========	=======================================================================================

Compile
-------
Command: ``repo.exe graph --compile``

=========	==================================================================================================
Option		Description
=========	==================================================================================================
directory	Absolute path to the repository
exclude		(optional) Subdirectories to exclude (recursive), comma-separated.  A good example would be ".git"
=========	==================================================================================================

Export
------
Command: ``repo.exe graph --export``

===========		=======================================================================================
Option			Description
===========		=======================================================================================
creator			Creator of the repository
description		Description of the repository
===========		=======================================================================================