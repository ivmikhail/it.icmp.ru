@echo off
del site.rar
msbuild
del PrecompiledWeb\src\web.config
rar a site PrecompiledWeb -r