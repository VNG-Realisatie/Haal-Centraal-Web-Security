# Use case Applicatie leverancier wordt in de cloud gehost

Gemeente wil gebruik maken van een applicatie die wordt gehost in de cloud. Deze applicatie maakt gebruik van een basisregistratie API.

## Gemeente doet authenticatie en authorisatie

- WebApp Leverancier gebruikt IdP van gemeente om medewerkers te authenticeren
- API Gateway gemeente biedt proxy van Kadaster API die wordt aangeroepen door WebApp Leverancier
- API Gateway gemeente routeert de API aanroep naar de Kadaster API

Voordelen:

- Protocollering wordt door de gemeente zelf gedaan
- Authenticatie en authorisatie tussen Kadaster en Gemeente kan met dubbelzijdig TLS + api key blijven of met client credentials flow

Uitzoeken:

- Hoe zorgen we ervoor dat een leverancier niet de url van de proxy API's hoeft te beheren. Kan het mee als claims? Dit kan heel veel zijn. Of via een discovery service?

![test](./scenario-apigateway-routeert.jpg)

