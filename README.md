# Portal Ogłoszeniowy

Aplikacja internetowa do zarządzania ogłoszeniami, zbudowana w **ASP.NET Core MVC** (.NET 8). Projekt zaliczeniowy z przedmiotu *Aplikacje internetowe na platformie .NET* — 3. rok Informatyki Stosowanej.

**Repozytorium:** [github.com/Piotr0605/PortalOgloszeniowy](https://github.com/Piotr0605/PortalOgloszeniowy)

---

## Opis projektu

Portal umożliwia przeglądanie, dodawanie, edycję i usuwanie ogłoszeń. Każde ogłoszenie zawiera tytuł, opis, cenę, kategorię oraz datę dodania. Aplikacja oferuje inteligentne wyszukiwanie z rankingiem trafności oraz filtrowanie po kategorii.

Aplikacja działa **lokalnie** (`localhost`) — uruchamiasz ją na swoim komputerze i prezentujesz w przeglądarce.

---

## Funkcjonalności

| Funkcja | Opis |
|---------|------|
| **CRUD** | Pełna obsługa ogłoszeń — tworzenie, odczyt, edycja, usuwanie |
| **Wyszukiwanie** | Wielosłowne wyszukiwanie w tytule i opisie z rankingiem trafności |
| **Filtrowanie** | Wybór kategorii: Motoryzacja, Nieruchomości, Elektronika, Praca, Inne |
| **Walidacja** | Walidacja po stronie serwera (Data Annotations) i klienta (jQuery) |
| **Responsywność** | Interfejs Bootstrap 5 — działa na telefonie i komputerze |
| **Testy** | Testy jednostkowe i integracyjne (xUnit) |

---

## Stos technologiczny

| Warstwa | Technologia |
|---------|-------------|
| Framework | ASP.NET Core MVC (.NET 8) |
| ORM | Entity Framework Core 8 |
| Baza danych | SQLite (plik lokalny) |
| UI | Razor Views + Bootstrap 5 |
| Testy | xUnit, WebApplicationFactory, EF InMemory |

---

## Wymagania

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Terminal lub edytor (Visual Studio, VS Code, Rider)

### Instalacja .NET 8 (macOS)

Jeśli polecenie `dotnet` nie jest rozpoznawane, zainstaluj SDK:

**Opcja A — instalator graficzny (zalecane):**
1. Pobierz .NET 8 SDK z [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Uruchom instalator `.pkg`
3. Zamknij i otwórz ponownie terminal

**Opcja B — skrypt w terminalu:**

```bash
curl -fsSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh
bash /tmp/dotnet-install.sh --channel 8.0 --install-dir "$HOME/.dotnet"
echo 'export PATH="$HOME/.dotnet:$PATH"' >> ~/.zshrc
source ~/.zshrc
```

Sprawdzenie:

```bash
dotnet --version
# Oczekiwany wynik: 8.x.x
```

---

## Uruchomienie (localhost)

### 1. Sklonuj repozytorium

```bash
git clone https://github.com/Piotr0605/PortalOgloszeniowy.git
cd PortalOgloszeniowy
```

### 2. Przywróć zależności

```bash
dotnet restore
```

### 3. Uruchom aplikację

```bash
cd PortalOgloszeniowy
dotnet run
```

### 4. Otwórz w przeglądarce

| Protokół | Adres |
|----------|-------|
| HTTPS | `https://localhost:7180` |
| HTTP | `http://localhost:5180` |

> Dokładny adres pojawi się w terminalu po `dotnet run` — skopiuj go stamtąd.

Przy pierwszym uruchomieniu aplikacja automatycznie:

- wykona migracje bazy danych (`ogloszenia.db`),
- doda 3 przykładowe ogłoszenia, jeśli baza jest pusta.

### 5. Certyfikat HTTPS (opcjonalnie)

Jeśli przeglądarka ostrzega o certyfikacie deweloperskim:

```bash
dotnet dev-certs https --trust
```

### 6. Zatrzymanie aplikacji

W terminalu naciśnij `Ctrl + C`.

---

## Rozwiązywanie problemów

| Problem | Rozwiązanie |
|---------|-------------|
| `command not found: dotnet` | Zainstaluj .NET 8 SDK (sekcja wyżej) i dodaj do PATH |
| Błąd kompilacji | Uruchom `dotnet build` i sprawdź komunikat w terminalu |
| Ostrzeżenie certyfikatu HTTPS | `dotnet dev-certs https --trust` |
| Pusta baza po usunięciu pliku `.db` | Usuń `ogloszenia.db` i uruchom ponownie — seed utworzy dane od nowa |

---


## Testy

```bash
dotnet test
```

Projekt `PortalOgloszeniowy.Tests` obejmuje:

- **WyszukiwarkaOgloszenTests** — normalizacja tekstu, ranking trafności, sortowanie
- **OgloszeniaIntegracjaTests** — żądania HTTP (lista, wyszukiwanie, filtr, szczegóły)

---

## Struktura projektu

```
PortalOgloszeniowy/                  # Katalog główny repozytorium
├── PortalOgloszeniowy.sln           # Rozwiązanie Visual Studio
├── README.md
│
├── PortalOgloszeniowy/              # Aplikacja webowa
│   ├── Controllers/
│   │   ├── OgloszeniaController.cs  # CRUD i wyszukiwanie
│   │   └── HomeController.cs        # Strona błędu
│   ├── Data/
│   │   └── AppDbContext.cs          # Kontekst Entity Framework
│   ├── Models/
│   ├── Services/
│   │   └── WyszukiwarkaOgloszen.cs  # Inteligentne wyszukiwanie
│   ├── Views/
│   │   ├── Ogloszenia/              # Index, Details, Create, Edit, Delete
│   │   └── Shared/                  # Layout, walidacja, Error
│   ├── Migrations/                  # Migracje EF Core
│   ├── wwwroot/css/                 # Style
│   ├── Program.cs                   # Konfiguracja, DI, seed danych
│   └── appsettings.json             # Connection string SQLite
│
└── PortalOgloszeniowy.Tests/        # Testy xUnit
```

---

## Model danych — `Ogloszenie`

| Pole | Typ | Opis |
|------|-----|------|
| `Id` | `int` | Klucz główny |
| `Tytul` | `string` | Tytuł (max 200 znaków) |
| `Opis` | `string` | Opis (max 2000 znaków) |
| `Cena` | `decimal` | Cena w PLN (≥ 0) |
| `Kategoria` | `enum` | Motoryzacja, Nieruchomosci, Elektronika, Praca, Inne |
| `DataUtworzenia` | `DateTime` | Data dodania (ustawiana automatycznie) |

---

## Baza danych

- SQLite — plik `ogloszenia.db` w folderze `PortalOgloszeniowy/`
- Tworzony automatycznie przy pierwszym `dotnet run`
- Plik `.db` jest w `.gitignore` (nie trafia na GitHub)

Ręczne migracje (opcjonalnie):

```bash
cd PortalOgloszeniowy
dotnet ef migrations add NazwaMigracji
dotnet ef database update
```

> Wymaga: `dotnet tool install --global dotnet-ef`

---

## Autor

**Piotr Sienkiewicz** — projekt zaliczeniowy, Informatyka Stosowana, 3. rok.

---

## Licencja

Projekt edukacyjny — do użytku w ramach zajęć i portfolio.
