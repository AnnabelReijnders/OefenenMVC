# TicketScore Web Application

TicketScore is een webapplicatie waarmee gebruikers evenementen kunnen bekijken, tickets kunnen kopen en hun account kunnen beheren. De applicatie biedt een gebruiksvriendelijke interface voor zowel organisatoren als bezoekers.

## Kenmerken

- **Evenementenbeheer**: Organisatoren kunnen evenementen aanmaken, bewerken en verwijderen.
- **Ticketverkoop**: Gebruikers kunnen tickets kopen voor verschillende evenementen.
- **Accountbeheer**: Gebruikers kunnen zich registreren, inloggen en hun accountgegevens bekijken.
- **Responsive Design**: De applicatie is geoptimaliseerd voor verschillende apparaten en schermformaten.

## Technische Specificaties

- **Framework**: ASP.NET Core
- **Database**: SQL Server
- **Authenticatie**: Cookie-gebaseerde authenticatie
- **Frontend**: HTML, CSS, Bootstrap

## Installatie

Volg deze stappen om de applicatie lokaal te draaien:

1. Clone de repository naar je lokale machine:
   ```bash
   git clone https://github.com/jouwgebruikersnaam/TicketScore.git
2. Configureer de SQL Server database. Zorg ervoor dat je een SQL Server-database hebt draaien. Maak een database aan voor dit project en pas de verbindingsreeks aan in het appsettings.json-bestand van de applicatie.
   ```bash DATABASE_URL=mysql+pymysql://<username>:<password>@<host>:<port>/<database_name>
