# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY ./published/ ./
RUN rm -rf CodeCoverage
#RUN ls 
ENTRYPOINT ["dotnet", "PaymentGateway.WebApi.dll"]
