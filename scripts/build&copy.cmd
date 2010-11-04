@echo off
x:
cd \www\it-mvc-builder
rd /s /q x:\www\itc-mvc\bin
svn up .
C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\msbuild /p:OutDir=..\PrecompiledWeb\
if %errorlevel%==0 goto copying

:error
echo Error building

:copying
echo ok

del PrecompiledWeb\_PublishedWebsites\ITCommunity\web.config
del PrecompiledWeb\_PublishedWebsites\ITCommunity\app.config
xcopy /y /s PrecompiledWeb\_PublishedWebsites\ITCommunity\* x:\www\itc-mvc
