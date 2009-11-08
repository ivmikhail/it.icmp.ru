@echo off
x:
cd \it2_builder
svn up .
msbuild

del PrecompiledWeb\src\web.config
xcopy /y /s PrecompiledWeb\src\* x:\it2
