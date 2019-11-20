# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
#COPY --from=build-env /app/published .RUN ls
COPY ./published/ ./
#RUN ls 
ENTRYPOINT ["dotnet", "PaymentGateway.WebApi.dll"]
