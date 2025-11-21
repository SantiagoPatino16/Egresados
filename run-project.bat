@echo off
echo Iniciando el proyecto con IIS Express...
echo.
echo El sitio estara disponible en: http://localhost:8080
echo Presiona Ctrl+C para detener el servidor
echo.
"C:\Program Files\IIS Express\iisexpress.exe" /path:"%~dp0Presentation" /port:8080
