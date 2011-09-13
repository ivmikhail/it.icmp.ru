@echo off
x:
cd \www\itc-mvc-builder\scripts

"C:\Program Files\Microsoft SQL Server\100\Tools\Binn\sqlcmd" -S itcommunity\itc -d itc-mvc -i backup.sql

rar a y:\backup\sql ..\it_backup.trn -x@ignore.txt -r -ag-yyyy-mm-dd -s -t
sc stop redmine
rar a y:\backup\redmine x:\www\redmine -x@ignore.txt -r -ag-yyyy-mm-dd -s -t
sc start redmine

sc stop svn
rar a y:\backup\repo x:\repo -x@ignore.txt -r -ag-yyyy-mm-dd -s -t
sc start svn

rar a y:\backup\www x:\www\itc-mvc -x@ignore.txt -r -ag-yyyy-mm-dd -s -t

call start_services.cmd