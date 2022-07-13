rmdir /s /q X:\Software\ProjectServer
dotnet publish --configuration Release ..\src2
robocopy ..\src2\ProjectServer\bin\Release\net6.0\publish X:\Software\ProjectServer /E /NJH /NFL /NDL
pause