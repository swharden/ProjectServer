rmdir /s /q X:\Software\ProjectServer
dotnet publish --configuration Release ..\src
robocopy ..\src\ProjectServer\bin\Release\net6.0\publish X:\Software\ProjectServer /E /NJH /NFL /NDL
pause