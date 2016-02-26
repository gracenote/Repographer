Using the Reports
=================

Repographer comes with a number of SSRS reports:

* Project and Assembly Dependency Summary

	* Identifies high-impact projects and assemblies to convert to NuGet packages

* Projects or Assemblies Depended on by Other Projects

	* Answers the question, "What projects have a reference to this item, and where do they live in the repository?"

* Projects or Assemblies Referenced via Multiple Paths

	* Identifies broken reference paths.  This usually happens when projects or assemblies are moved to different locations in the repository, but not all referencing projects or solutions have their HintPaths updated.

* Referencing Solutions by Project

	* Identifies projects that are members of multiple Solutions, aka "Multi-homed Projects."  This is generally considered a bad practice.

Contents:

.. toctree::
   :maxdepth: 1

   project-assembly-dependency-summary
   project-assembly-depended-on
   project-assembly-multipath
   ref-solutions-by-project