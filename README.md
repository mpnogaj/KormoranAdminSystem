# Deweloperka
## Najważniejsza informacja w galaktyce i w tym repo:  
**PLIS PRZY PISANU KODU UŻYWAJ TABÓW O DŁUGOŚCI 4, A NIE SPACJI JAK JAKIŚ BARBARZYŃCA**
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
## Jak, co, po co i dlaczego
Generalnie wszystko to co jest w katalogu **ClientApp** to nic innego jak aplikacja Reactowa pisana w **TypeScripcie**. Cała reszta, napisana w **C#** przy użyciu **ASP .NET Core 6.0**, to kod wykonywany po strone serwera.

Do nauki wyżej wymienionych technologi polecam:
* TypeScript:
	* Po angliesku:
		* [Dla osób znających JS](https://www.typescriptlang.org/docs/handbook/typescript-in-5-minutes.html)
		* [Dla osób znających języki obiektowe](https://www.typescriptlang.org/docs/handbook/typescript-in-5-minutes-oop.html)
		* [Dla świeżaków](https://www.typescriptlang.org/docs/handbook/typescript-from-scratch.html)
	* Po polsku:
		* [Cały kurs](https://typeofweb.com/kurs/typescript)
		* [Najważniejsze informacje](http://adriankurek.pl/2020/05/30/wstep-do-typescript-instalacja-i-typowanie-statyczne/)
		* [Kurs na YT](https://www.youtube.com/watch?v=nUjl2nK0FAY&list=PLfE0DpqEANZ0CQ9pCGlxGKPvYb1Sj6ybV)
* React:
	* [Oficjalna dokumentacja i samouczki](https://pl.reactjs.org/tutorial/tutorial.html)
	* [Inny kurs](https://typeofweb.com/kurs/react-js)
* C#:
	* [Oficjalna dokupentacja po polsku i angliesku](https://docs.microsoft.com/pl-pl/dotnet/csharp/)
	* Po angielsku:
		* [Dla osób znających Jave](https://nerdparadise.com/programming/csharpforjavadevs)
		* [Dla świeżaków](https://www.youtube.com/watch?v=GhQdlIFylQ8)
	* Po polsku:
		* [Dla świeżaków](https://youtu.be/pYdROHWkxgw?t=363)
		* [Od zera do koksa](http://kurs.aspnetmvc.pl/Csharp)
		* [Książka z której ja się uczyłem](https://www.microsoftpressstore.com/store/microsoft-visual-c-sharp-step-by-step-9781509307760)
* ASP .NET Core:
	* [Oficjalna dokupentacja po polsku i angliesku](https://docs.microsoft.com/pl-pl/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-5.0&tabs=visual-studio) 
	* Po angliesku: 
		* [Hindus godny polecenia (część MVC)](https://www.youtube.com/watch?v=C5cnZ-gZy2I&t=3523s)
		* [Kurs ASP + fragmenty C# dla początkujących](https://www.youtube.com/watch?v=1ck9LIBxO14)
	* Po polsku:
		* [Dla znających C#](https://www.youtube.com/watch?v=xcDEYIUFmU4)
  
# Inne informacjie
README do poprawy i do rozwinięcia