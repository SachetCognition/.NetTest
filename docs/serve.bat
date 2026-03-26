@echo off
echo URS document available at http://localhost:8080
cd /d "%~dp0"
python -m http.server 8080
