# XBank

criar bases com efCOre

cd XBank.Repository
dotnet ef --startup-project ..\XBank\ migrations add initXBank
dotnet ef --startup-project ..\XBank\ database update