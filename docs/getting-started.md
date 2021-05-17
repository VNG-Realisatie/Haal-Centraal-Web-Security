---
layout: page-with-side-nav
title: Getting Started
---
De Haal Centraal Web Security repo bevat een starter kit waarmee je OpenId Connect en OAuth 2 kunt uitproberen. De starter kit bestaat uit een aantal web applicaties waarmee de OAuth 2 rollen worden ingevuld:

- HaalCentraal.IdentityServer: deze web applicatie vervult de rol van de **Autorisatie Server** binnen OAuth
- HaalCentraal.Viewer: een traditionele web applicatie (code wordt uitgevoerd op een server) die binnen OAuth de rol van de **Client** vervult en is consumer van de BAG-, BRK- en BRP Bevragen APIs
- HaalCentraal.ApiGateway: binnen OAuth vervult deze web applicatie de **Resource** rol, en is geïmplementeerd als een proxy voor de echte BAG-, BRK- en BRP Raadplegen APIs

Verder bevat de starter kit een aantal ondersteunende web applicaties:

- NGINX, een web server geconfigureerd als reverse proxy voor het routeren van requests. In de starter kit is NGINX geconfigureerd om requests voor
  - oidc.haalcentraal.local te routeren naar de IdentityServer web applicatie
  - viewer.haalcentraal.local te routeren naar de Viewer web applicatie
  - api.haalcentraal.local te routeren naar de ApiGateway web applicatie

Voor het hosten van de web applicaties wordt gebruik gemaakt van Docker. Docker maakt het mogelijk om applicaties te containerizen. Een gecontainerizede applicatie bevat behalve de applicatie ook al zijn afhankelijkheden, zodat de applicatie op elke PC kan worden gedraaid zonder de afhankelijkheden te moeten installeren op de PC.

## Vereiste

Voor het draaien van de web applicaties is [Docker Desktop](https://www.docker.com/products/docker-desktop) een vereiste. Optioneel kan ook [Postman] worden geïnstalleerd. Postman is een generieke consumer voor APIs en ondersteunt ook OAuth als security protocol. Met Postman kan dus ook een API worden aangeroepen die is beveiligd met OAuth.

## Configuratie

Om het uitproberen van de web applicaties makkelijk te maken, wordt de configuratie van de web applicaties gerealiseerd met behulp van json bestanden. Deze configuratie bestanden zijn te vinden in de `config` map. Voor het draaien van de web applicaties as-is, moet minimaal de configuratie van de ApiGateway worden aangepast.

### HaalCentraal.ApiGateway configuratie

De configuraties voor de HaalCentraal.ApiGateway applicatie is te vinden in de `config/HaalCentraal.ApiGateway` map en bestaat uit de volgende json bestanden:

- auth.json. In dit bestand worden de OAuth settings geconfigureerd:
  - OpenIdConnect:authority. De url van de OpenId Connect en OAuth 2 provider
- ocelot.json. In dit bestand zijn de routeringen naar de BAG-, BRK- en BRP Bevragen APIs geconfigureerd. De volgende json snippet is de configuratie voor de BAG bevragen API routering:

``` json
    {
      "UpstreamPathTemplate": "/bag/{everything}", // alle urls die beginnen met bag moeten worden gerouteerd naar de BAG Bevragen API
      "DownstreamPathTemplate": "/esd/huidigebevragingen/v1/{everything}",
      "DownstreamScheme": "https",
      "DownstreamHostAndPorts": [
        {
          "Host": "api.bag.acceptatie.kadaster.nl",
          "Port": "443"
        }
      ],
      "UpstreamHeaderTransform": {
        "X-Api-Key": "bag-api-key" // de header die moet worden toegevoegd. bag-api-key moet worden vervangen met uw bag api key
      },
      "AuthenticationOptions": {
        "AuthenticationProviderKey": "oidc", // hiermee wordt alle endpoints die beginnen met /bag beveiligd met OAuth
        "AllowedScopes": [ "BAG" ] // de autorisatie scopes die gelden voor de /bag endpoints
      }
    }
```

Voor de BAG-, BRK- en BRP Bevragen API routeringen moeten de placeholders `bag-api-key`, `brk-api-key` en `brp-api-key` worden vervangen met echte api keys voor de desbetreffende APIs.

### HaalCentraal.Viewer configuratie

De configuratie voor de HaalCentraal.Viewer applicatie is te vinden in de `config/HaalCentraal.Viewer` map en bestaat uit één json bestand:

- auth.json. In dit bestand worden de OAuth settings geconfigureerd:

  ``` json
  {
    "OpenIdConnect": {
        "authority": "https://oidc.haalcentraal.local:44395", // de url van de OpenId Connect en OAuth 2 provider
        "clientid": "haalcentraalviewer", // de clientid van de viewer. Deze clientid moet in de bovengenoemde provider zijn geregistreerd
        "clientsecret": "secret", // de secret voor de viewer. Deze moet overeenkomen met de secret die voor de clientid in de bovengenoemde provider is opgenomen
        "scopes": "openid profile gemeente-specifiek BAG BRK BRP". // De scopes waarvoor de client toegang wil krijgen. Deze scopes moeten ook voor de client in de bovengenoemde provider zijn opgenomen. Het kunnen minder scopes zijn dan geconfigureerd, maar niet meer.
    }
  }
  ```

### HaalCentraal.IdentityServer configuratie

De configuratie voor de HaalCentraal.IdentityServer applicatie is te vinden in de `configuratie/HaalCentraal.IdentityServer` map en bestaat uit de volgende json bestanden:

- identityserver.json. In dit bestand worden de clients, de api scopes en de user claims gedefinieerd
- testusers.json. In dit bestand worden de users en hun claims gedefinieerd

#### API Scopes

Binnen OAuth wordt een scope gebruikt om een permissie op een API te definiëren. Een scope kan gelden voor de gehele API of voor één of meerdere endpoints van de API. In de starter kit is voor elk van de Haal Centraal APIs 1 scope gedefinieerd, de `BAG`, `BRK` en `BRP` scopes voor respectievelijk de BAG-, BRK- en BRP Bevragen API. In het identityserver.json bestand worden de scopes gedefinieerd onder de **ApiScopes** root element.

Voor een scope kunnen `UserClaims` worden gedefinieerd. Deze user claims worden in de access token opgenomen wanneer de client de bijbehorende scope wordt toegekend. In de volgende snippet is de definitie van de BRP scope met bijbehorende user claims:

``` json
"ApiScopes": [
    {
    "Name": "BRP",
    "UserClaims": [ "brp-permission", "gemeente" ]
    }
]
```

#### Clients

Een applicatie die toegang wil krijgen tot de APIs die beveiligd zijn met OAuth, moet bij de IdP/STS van de APIs bekend zijn. Voor HaalCentraal.IdentityServer zijn de clients gedefinieerd onder de **Clients** root element. In de volgende json snippet zijn de client definities van de HaalCentraal.Viewer applicatie en een machine to machine client te zien:

``` json
"Clients": [
    {
    "ClientId": "haalcentraalviewer",
    "ClientSecrets": [ { "Value": "K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=" } ],
    "AllowedGrantTypes": [ "authorization_code" ], // authorization_code is de aanbevolen flow voor traditionele web applicaties
    "RequirePkce": true, // authorization_code + Pkce (Proof Key for Code Exchange) is vanaf OAuth 2.1 de aanbevolen flow voor alle web applicaties
    "AllowedScopes": [ "openid", "profile", "gemeente-specifiek", "BAG", "BRK", "BRP" ], // De user gegevens (openid, profile, gemeentespecifiek) en APIs (BAG, BRK, BRP) waarvoor deze client toegang/recht wil krijgen
    "RedirectUris": [ "https://viewer.haalcentraal.local:44395/signin-oidc" ], // De url van de client waar de IdP moet redirect-en als de user is geauthenticeerd door de IdP
    "PostLogoutRedirectUris": [ "https://viewer.haalcentraal.local:44395/signout-callback-oidc" ], // De url van de client waar de IdP moet redirect-en als de user zich heeft uitgelogd
    "RequireConsent": true // tonen van de consent scherm. Hiermee kan de user de client toegang tot user gegevens en/of APIs ontnemen door één of meerdere scopes uit te vinken
    },
    {
    "ClientId": "m2m",
    "ClientSecrets": [ { "Value": "K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=" } ],
    "AllowedGrantTypes": [ "client_credentials" ], // deze client heeft geen user interactie. client_credentials is in dit geval de aanbevolen flow
    "AllowedScopes": [ "BAG", "BRK", "BRP" ]
    }
]
```

#### Identity Resources (User Scopes)

In [IdentityServer4](https://identityserver4.readthedocs.io), een OpenId Connect en OAuth 2 framework voor .NET waarmee HaalCentraal.IdentityServer is geïmplementeerd, worden user claims gedefinieerd met behulp van IdentityResources. Een IdentityResource is een naam die als user scope wordt gebruikt, om één of meerdere user claims aan te duiden. Voor de starter kit is als voorbeeld een IdentityResource `gemeente-specifiek` gedefinieerd om alle gemeente-specifieke user claims aan te duiden. Voorbeelden van gemeente-specifieke user claims zijn:

- gemeente. De gemeente waar de ingelogde user werkt. Hiermee kan de HaalCentraal.ApiGateway bepalen of de ingelogde gebruiker een binnen/buiten gemeentelijke bevraging op de BRP doet
- bag-permission, brk-permission, brp-permission. Met een scope wordt op API of endpoint niveau de toegang bepaald. Voor fijnmaziger autorisatie kunnen user claims worden gebruikt.

In de volgende json snippet is de definitie van de `gemeente-specifiek` IdentityResource te zien:

``` json
"IdentityResources": [
    {
    "Required": true, // verplicht/niet verplicht maken van een user scope. Verplichte scopes kunnen in het consent scherm niet worden uitgevinkt
    "Name": "gemeente-specifiek",
    "Description": "Gemeente specifieke claims", // omschrijving die wordt getoond in het consent scherm
    "UserClaims": [ "bag-permission", "brk-permission", "brp-permission", "gemeente" ] // de user claims die vallen onder deze IdentityResource
    }
]
```

#### Test Users

Test users moeten worden gedefinieerd in het testusers.json bestand. In het volgende json snippet is de definitie van een test user:

``` json
{
    "SubjectId": "1",
    "Username": "jan",
    "Password": "jan",
    "Claims": [
        {
            "Type": "name",
            "Value": "Jan Janssen"
        },
        {
            "Type": "bag-permission",
            "Value": "read"
        },
        {
            "Type": "brk-permission",
            "Value": "read-excl-bsn"
        },
        {
            "Type": "brp-permission",
            "Value": "read"
        },
        {
            "Type": "gemeente",
            "Value": "0518"
        }
    ]
}
```

### Domein namen toevoegen aan hosts bestand

De volgende regels moeten worden toegevoegd aan het hosts bestand van uw PC:

``` text
127.0.0.1   oidc.haalcentraal.local
127.0.0.1   viewer.haalcentraal.local
127.0.0.1   api.haalcentraal.local
```

Het pad naar het hosts bestand:

- Windows: C:\Windows\System32\drivers\etc\hosts
- Mac: /private/etc/hosts
- Linux: /etc/hosts

### installeren van de PFX bestanden

In de certificate map zijn er drie pfx bestanden:

- api.haalcentraal.local.pfx
- oidc.haalcentraal.local.pfx
- viewer.haalcentraal.local.pfx

Deze moeten worden geïmporteerd in de trusted root certificate store.

Zie [Installeer self-signed certificates]('./installeer-self-signed-certificates') voor een overzicht van de te doorlopen stappen.

## Bouwen Docker Images

Het bouwen van de Docker Images voor de web applicaties wordt opgestart met behulp van de volgende statement: `docker-compose -f docker-compose-id4.yml build`

Het bouwen van de Docker Images hoeft alleen de eerste keer te gebeuren of na het pullen van updates uit de Haal-Centraal-Web-Security repo.

## Opstarten Docker Containers

Nadat de Docker Images voor de web applicaties zijn gebouwd, kunnen de web applicaties worden opgestart met de volgende statement: `docker-compose -f docker-compose-id4.yml up -d`

## Stoppen Docker Containers

De Docker containers kunnen worden gestopt met behulp van de volgende statement: `docker-compose -f docker-compose-hc.yml down`
