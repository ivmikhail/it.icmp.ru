@echo off
msbuild
del PrecompiledWeb\src\web.config
rar a site PrecompiledWeb -r