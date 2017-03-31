@echo off
RMDIR /S /Q "Bin"
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S bin') DO RMDIR /S /Q "%%G"
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S obj') DO RMDIR /S /Q "%%G"
"%ProgramFiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" build.xml /target:Build /p:BuildConfiguration=Debug /p:Version="1.2.0" /p:FileVersion="1.2.0" /p:VersionLabel="1.2.0" /v:n /fileLogger /m /p:VisualStudioVersion=14.0