# Notatka dla osób stawiających serwer
## Wymagane oprogramowanie
1. Zainstalowany .NET SDK w wersji 6. Można ściągnąć [tutaj](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).
2. Zainstalowany i skonfigurowany serwer MySQL w wersji 8.0.27, lokalnie, lub na serwerze zewnętrznym. Do ściągnięcia [tutaj](https://dev.mysql.com/downloads/mysql/).
3. Zainstalowany Entity Framework Core. Można zainstalować wywołując polecenie:  
```dotnet tool install --global dotnet-ef```
## Przygotowanie środowiska
1. Po pobraniu kodu źródłowego w katalogu ```KormoranAdminSystemRevamed``` należy zapisać plik ```appsettings.json.template``` jako ```appsettings.json``` oraz zastąpić ```${CONNECTION_STRING}``` na prawidłowy ciąg połączenia to bazy danych. Powinien on wyglądać mniej więcej tak:  
```Server=localhost;Port=3306;Database=kormoran;Uid=gargamel;Pwd=124Smerfy!;```.
2. W tym samym katalogu wywołać polecenie ```dotnet ef database update```. Spowoduje to utworzenie się tabel w bazie danych.  
**UWAGA**  
Polecenie to warto wywołać za każdym razem po ściągnięcu zmian poleceniem ```git pull```. Automatycznie zaktualizuje tabele jeżeli będzie taka potrzeba. Jeżeli po wywołaniu tej komendy pokaże się błąd można spróbować usunąć tabele z bazy (nie powinno się zdarzać często, chyba że zostały wprowadzone drastyczne zmiany).
3. Serwer wystartować poleceniem ```dotnet run```
