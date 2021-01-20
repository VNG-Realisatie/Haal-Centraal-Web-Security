---
layout: page-with-side-nav
---
# Use case: Web applicatie (zowel on-premise als SAAS)

Gemeente heeft een (on-premis of SAAS) web applicatie die gebruik maakt van een Haal Centraal API.

## Gemeente doet authenticatie en autorisatie

- Web applicatie gebruikt IdP van gemeente om medewerkers te authenticeren
- API Gateway gemeente biedt een intermediate endpoint als proxy van de Haal Centraal API die wordt aangeroepen door web applicatie
- API Gateway gemeente routeert de API aanroep naar de Haal Centraal API

Voordelen:

- Protocollering wordt door de gemeente zelf gedaan
- Authenticatie en authorisatie tussen API provider en Gemeente kan met dubbelzijdig TLS + api key blijven of met OAuth (client credentials flow)

Uitzoeken:

- Hoe zorgen we ervoor dat een leverancier niet zelf de url van de proxy API's bij gemeenten hoeft te beheren in de cloud applicatie. Kan het mee als claim? Of via een discovery service?

### Routeer rechtstreeks naar Haal Centraal API van de landelijke voorziening

![Gemeente doet authenticatie en authorisatie scenario BRK](./scenario-apigateway-routeert.jpg)

### Routeer via een intermediate endpoint bij gemeente naar Haal Centraal API van de landelijke voorziening

![Gemeente doet authenticatie en authorisatie scenario BRP](./scenario-apigateway-routeert-bij-brp.jpg)
