cd .\LabCMS.Gateway.Server\
dotnet publish -c Release -r win10-x64 -p:ReadyToRun=true -p:PublishSingleFile=true -p:DeleteExistingFiles=true --no-self-contained -o ..\Publish\LabCMS.Gateway.Server\
cd ..\LabCMS.ProjectDomain.Server\
dotnet publish -c Release -r win10-x64 -p:ReadyToRun=true -p:PublishSingleFile=true -p:DeleteExistingFiles=true --no-self-contained -o ..\Publish\LabCMS.ProjectDomain.Server\

cd ..\LabCMS.EquipmentDomain.Server\
dotnet publish -c Release -o ..\Publish\LabCMS.EquipmentDomain.Server\
cd ..