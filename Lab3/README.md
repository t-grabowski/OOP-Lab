# System zniżek w aplikacji e-commerce

Aplikacja ma na celu pokazać prosty system zniżek oparty na wzorcu projektowym strategii.

## Najważniejsze Klasy

- Entity - klasa abstrakcyjna reprezentująca obiekt z ID, zawiera metody pomagające porównywać obiekty po ich ID
- Basket - koszyk na produkty, oblicza koszt oraz rabaty
- Product - produkt, ma nazwę, cenę oraz może posiadać zniżkę
- Discount - klasa abstrakcyjna reprezentująca zniżkę, dziedziczą po niej wszystkie typy zniżek, np. procentowa, 2 w cenie 1
- DataContext - reprezentuje bazę danych frameworku Entity Framework Core
- DbConfiguration - zawiera konfiguracje obiektów oraz ich relacji w bazie danych

## Połączenie z bazą danych
Aplikacja używa EntityFramework Core i, do celów pokazowych, bazy danych SQLite.

