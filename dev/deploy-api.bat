:: deploy manually via the X-drive because the remote server does 
:: not have internet access and can't pull the latest code using git

rmdir "X:\Lab Documents\network\htdocs\abf-browser\api" /Q /S
robocopy "C:\Users\swharden\Documents\GitHub\ABF-browser\api" "X:\Lab Documents\network\htdocs\abf-browser\api" /E /NJH /NFL /NDL
::pause