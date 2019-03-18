FROM microsoft/dotnet:2.2-sdk as publishcode
LABEL Author="Nikhil Sharma"
WORKDIR /publishcode
COPY Product.API.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish --output ./out --configuration release

FROM microsoft/dotnet
WORKDIR /app
COPY --from=publishcode /publishcode/out .
ENV ASPNETCORE_URLS=http://*:80
EXPOSE 80
ENTRYPOINT ["dotnet","Product.API.dll"]