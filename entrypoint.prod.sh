#!/bin/bash

cd Infrastructure && dotnet ef database update; cd ..

dotnet run --project Web/Web.csproj --urls "http://0.0.0.0:5000;https://0.0.0.0:5001"
