rmdir "X:\Lab Documents\network\htdocs\abf-browser" /Q /S
robocopy "C:\Users\swharden\Documents\GitHub\ABF-browser\abfjs\build" "X:\Lab Documents\network\htdocs\abf-browser" /E /NJH /NFL /NDL /NJS
robocopy "C:\Users\swharden\Documents\GitHub\ABF-browser\api" "X:\Lab Documents\network\htdocs\abf-browser\api" /E /NJH /NFL /NDL /NJS
pause