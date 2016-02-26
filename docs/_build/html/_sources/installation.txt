The SSRS report project in this .sln is part of SSDT-BI for VS 2013. You can download from here: 
http://www.microsoft.com/en-us/download/details.aspx?id=42313

Installation
============

1. Clone the repository: :command:`git clone https://www.github.com/gracenote/Repographer.git`
2. In Visual Studio, open src\\Repographer.sln

Deploy the Database
-------------------
3. Right-click on Repographer.Database and select "Publish."  
4. Name the database "Repographer" and supply a valid connection string.

Deploy the Reports
------------------
5. Right-click on Repographer.Reports and modify the Properties.  Change the Report Server URL to the desired server.
6. In Repographer.Reports, right-click on the "Repographer" data source and update the connection string to where the database is published.
7. Right-click on Repographer.Reports and "Publish"

Compile the Executable
----------------------
8. Enable NuGet Restore for the .sln
9. Compile Repographer

Once compiled, you can distribute the binaries as a .zip to others in your organization that have use of the tool.  

*Note:* It is highly recommended to put repo.exe on your PATH!