cd ../aspnet-core/src/Xhznl.HelloAbp.IdentityServer
start dotnet run

cd %~dp0

cd ../aspnet-core/src/Xhznl.HelloAbp.HttpApi.Host
start dotnet run

cd %~dp0

cd ../vue-element-admin-i18n
start npm run dev
