@ECHO ON

ECHO running build.ps1
Powershell -executionpolicy remotesigned ./build.ps1 > build.log 2>&1 & type build.log