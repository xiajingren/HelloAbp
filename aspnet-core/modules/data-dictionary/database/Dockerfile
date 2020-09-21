FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
COPY . .

WORKDIR /templates/service/host/IdentityServerHost
RUN dotnet restore
RUN dotnet ef migrations script -i -o migrations-IdentityServerHost.sql

WORKDIR /templates/service/host/Xhznl.DataDictionary.Host
RUN dotnet restore
RUN dotnet ef migrations script -i -o migrations-DataDictionary.sql

FROM mcr.microsoft.com/mssql-tools AS final
WORKDIR /src
COPY --from=build /templates/service/host/IdentityServerHost/migrations-IdentityServerHost.sql migrations-IdentityServerHost.sql
COPY --from=build /templates/service/host/Xhznl.DataDictionary.Host/migrations-DataDictionary.sql migrations-DataDictionary.sql
COPY --from=build /templates/service/database/entrypoint.sh .
RUN /bin/bash -c "sed -i $'s/\r$//' entrypoint.sh"
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]