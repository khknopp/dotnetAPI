# Proste REST API w ASP.NET CORE
Ten program został napisany i przetestowany na platformie GNU/Linux x64 z:

- .NET SDK 7.0.103
- Microsoft.AspNetCore.App 7.0.3
- Microsoft.NETCore.App 7.0.3

## Funkcjonalności
Ten program to najprostsze możliwe API (napisane zgodnie z regułą KISS) i zawiera:
1. klasę tabeli produktu (`Product.cs`) z autominkrementującym indeksem oraz
konstruktor z datami utworzenia i ostatniej modyfikacji. Tabela `Product` posiada kolumny:
- `ID`
- Nazwę (`Name`)
- Opis (`Description`)
- Cenę (`Price`)
- Datę utworzenia (CreatedDate)
- Datę ostatniej modyfikacji (LastEdited)

2. Lokalną bazę danych opartą na silniku SQLite oraz Entity Framework;
ustawienia są trzymane w pliku `appsettings.json`, baza danych w pliku `api.db`

3. System autoryzacji poprzez token JWT (wymagane jest utworzenie tokenu w 
celu komunikacji z API)

4. Następujące metody API:
- **GET** `/produkty`: zwraca wszystkie obecnie dostępne produkty
- **GET** `/produkty/{id}`: zwraca produkt z identyfikatorem `id`
- **POST** `/produkty`: dodaje produkt do bazy danych
- **PUT** `/produkty/{id}`: modyfikuje produkt z identyfikatorem `id`
- **DELETE** `/produkty/{id}`: usuwa element z identyfikatorem `id`

## Użytkowanie
1. Należy utworzyć autoryzowanego użytkownika, zgodnie z opisem dostępnym [tutaj](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authn?view=aspnetcore-7.0&tabs=linux)
2. Następnie można uruchomić program: `dotnet run`
3. Można spopulować bazę danych używając przygotowanych plików (`json/addEntry.json` oraz `json/putEntry.json`) oraz następujących komend:
- Testowanie **GET**:
```bash
curl --location 'http://localhost:{port}/produkty' \
--header 'Authorization: Bearer {token}'
```
lub
```bash
curl --location 'http://localhost:{port}/produkty/{id}' \
--header 'Authorization: Bearer {token}'
```

- Testowanie **POST**:
```bash
curl --data @json/addEntry.json --location 'http://localhost:{port}/produkty' \
--header 'Content-Type: application/json' \
--header 'Authorization: Bearer {token}'
```

- Testowanie **PUT**
```bash
curl --data @json/putEntry.json \ 
--location --request PUT 'http://localhost:{port}/produkty/{id}' \
--header 'Content-Type: application/json' \
--header 'Authorization: Bearer {token}'
```

- Testowanie **DELETE**
```bash
curl --location --request DELETE 'http://localhost:{port}/produkty/{id}' \
--header 'Authorization: Bearer {token}'
```
