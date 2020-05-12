# AspWebFormContainer
ASP .NET WebForms (.NET 4.5) on Docker container demo

## Requirements:

1. Visual Studio 2015+.
2. Docker Desktop 2.2.0.5+.

## Branch description:

1. ***aspnet_45***. Web app with .NET 4.5.2.
2. ***aspnet_47***. Upgrade Web app to .NET 4.7.2.
3. ***config_builder***. Integrated Configuration Builders to externalize connection strings.
4. ***container***. Encapsulating the web app to run on Docker container.
5. ***nuget_local***. Integrating how to use NuGet Package source locally.

## Execution:

With Visual Studio: branches ***aspnet_45***, ***aspnet_47***, ***config_builder***.

1. Open the solution.
2. Debug as normal (F5).

With Docker: branches ***container***, ***nuget_local***.

1. Open a PowerShell terminal.
2. Change directory to solution folder.
3. Build the image:
```
docker build --tag {tag_name} .
```
4. Run the container:
```
docker run --name {container_name} -e "DefaultConnection={connection_string}" --rm -it -p {port}:80 {tag_name}
```
