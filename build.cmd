@echo off
del site.rar
msbuild
del PrecompiledWeb\src\web.config
rd /s /q PrecompiledWeb\src\files
rar a site PrecompiledWeb -r