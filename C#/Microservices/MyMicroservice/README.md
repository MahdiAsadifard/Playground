[.NET Tutorial - Your First Microservice](https://dotnet.microsoft.com/en-us/learn/aspnet/microservice-tutorial/intro)

## Create project
```
dotnet new webapi -o MyMicroservice --no-https
```

## Create Docketfile
```
fsutil file createnew Dockerfile 0
```
```
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY MyMicroservice.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MyMicroservice.dll"]
```
### .dockerignore
```
fsutil file createnew .dockerignore 0
```
```
Dockerfile
[b|B]in
[O|o]bj
```

## Run docker 
Build image from Dockerfile
```
docker build -t mymicroservice .
```
List of images on machine
```
docker images
```
List of containers
```
docker ps
```

## Create container and run on port 3000
```
docker run -it --rm -p 3000:8080 --name mymicroservicecontainer mymicroservice
```
| Flag	| Description|
|:---|--------:|
|-it    | Interactive mode with a terminal |
| -p     | 3000:8080	Maps host port 3000 → container port 8080 |
| --name | mymicroservicecontainer	Names the container |
| mymicroservice |	The image name to run |


