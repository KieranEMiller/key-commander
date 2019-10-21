@ECHO OFF

ECHO running build.ps1
ECHO %DATE% %TIME%

Powershell -executionpolicy remotesigned ./build.ps1 --verbosity=quiet > build.log 2>&1 & type build.log