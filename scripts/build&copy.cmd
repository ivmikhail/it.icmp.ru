@echo off
x:
cd \www\itc-mvc-builder

svn up .

"C:\Program Files\Microsoft SQL Server\100\Tools\Binn\sqlcmd" -S itcommunity\itc -d itc-mvc -i db\procs.sql

C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\msbuild /p:OutDir=..\PrecompiledWeb\
if %errorlevel%==0 goto copying

:error
echo Error building
goto exit

:copying
echo ok

rd /s /q x:\www\itc-mvc\bin
del PrecompiledWeb\_PublishedWebsites\ITCommunity\web.config
del PrecompiledWeb\_PublishedWebsites\ITCommunity\app.config
xcopy /y /s PrecompiledWeb\_PublishedWebsites\ITCommunity\* x:\www\itc-mvc
:exit