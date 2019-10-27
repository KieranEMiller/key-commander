@ECHO OFF

ECHO compiling sass files...

sass ./src/KeyCdr.UI.Web/assets/css/master.scss ./src/KeyCdr.UI.Web/assets/css/master.min.css --style compressed

PAUSE
