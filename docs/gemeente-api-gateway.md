---
layout: page-with-side-nav
---
# Use case: Web applicatie (zowel on-premise als SAAS)

Gemeente heeft een (on-premis of SAAS) web applicatie die gebruik maakt van een Haal Centraal API.

## Gemeente doet authenticatie en autorisatie

- Web applicatie gebruikt IdP van gemeente om medewerkers te authenticeren
- API Gateway gemeente routeert de API aanroep:
- naar een intermediate endpoint als proxy van de Haal Centraal API die wordt aangeroepen door web applicatie, bijv. voor het fileren van responses.
- direct naar de landelijke Haal Centraal API

Voordelen:

- Authenticatie en autorisatie wprdt door de gemeente zelf gedaan
- Protocollering wordt door de gemeente zelf gedaan
- Authenticatie en autorisatie tussen landelijke API provider en Gemeente kan met dubbelzijdig TLS + api key of met OAuth (client credentials flow).

Uitzoeken:

Hoe zorgen we ervoor dat een leverancier niet zelf de url van de proxy API's bij gemeenten hoeft te beheren in de cloud applicatie? Te onderzoeken:
- Kan het mee als claim in het token? 
- Of via een discovery service?

### Routeer rechtstreeks naar Haal Centraal API van de landelijke voorziening

![Gemeente doet authenticatie en authorisatie scenario BRK](./scenario-apigateway-routeert.jpg)

### Routeer via een intermediate endpoint bij gemeente naar Haal Centraal API van de landelijke voorziening

![Gemeente doet authenticatie en authorisatie scenario BRP](./scenario-apigateway-routeert-bij-brp.jpg)
