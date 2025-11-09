setlocal enabledelayedexpansion
echo Minifying all project JS. Ignore *.min.js files because they are already minimized
for /f "delims=" %%f in ('dir /b /s *.js ^|findstr /vi "*.min.js"') do (
 @ajaxminifier -clobber -silent "%%f" -o "%%f"
 )
echo Minifying all project CSS. Ignore *.min.css files because they are already minimized
  for /f "delims=" %%f in ('dir /b /s *.css ^|findstr /vi "*.min.css"') do (
 @ajaxminifier -clobber -silent "%%f" -o "%%f"
 )