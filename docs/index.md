---
layout: page-with-side-nav
title: Haal Centraal Web Security
---

# Haal Centraal Web-Security

Repo voor het ontwikkelen van security best practices voor het gebruik van Haal Centraal API's. Doel is om samen met gemeenten en leveranciers een praktische gids te ontwikkelen hoe je security kunt inrichten voor veilig gebruik van Haal Centraal API's. Dit doen we aan de hand van referentie-implementaties, zodat je:

* inzicht krijgt in hoe het werkt, hoe dit bij een gemeente kan worden ingericht, en welke componenenten je nodig hebt;
* in jouw gemeente kan testen of de componenten (Identity Provider, API Gateway) goed zijn ingericht
* als leverancier de inrichting kunt kopieren om tegenaan te ontwikkelen
* als leverancier kunt testen of jouw SAAS implementatie werkt met deze inrichting.

# Wat is de kern van veilig gebruik van een HC API met vertrouwelijke gegevens?
* Dat uitsluitend geautoriseerde (gebruikers van) applicaties toegang krijgen tot de gegevens die de API levert
* Dat gebruikers uitsluitend toegang krijgen tot gegevens waarvoor zij zijn geautoriseerd
* Dat een (SaaS)applicatie uitsluitend toegang krijgt tot de gegevens namens een gebruiker van jouw gemeente, dus alleen als jouw medewerker achter de knoppen zit!

# Waar moeten we als gemeente naar toe (soll situatie)?

## (Toegangs)beveiliging, autorisatie en filtering
Gemeenten bieden een breed palet aan producten en diensten die allemaal een andere minimale set gegevens en functionaliteit nodig hebben. Onze diensten worden geleverd door veel verschillende medewerkers met verschillende rollen en rechten. We kunnen (nog) niet aan een landelijke voorziening vragen om hiervoor de toegangsbeveiliging en autorisatie te organiseren. Dat moet een gemeente zelf doen. Daar hebben we minimaal voor nodig:
* een Identity Provider (IP) voor het authenticeren van de eindgebruiker waarin de claims voor het gebruik van de API van alle gebruikers van jouw gemeente centraal zijn vastgelegd. Nadat de Identity provider heeft vastgesteld wie de ingelogde gebruiker is en welke applicatie namens de gebruiker de API wil bevragen, kunnen tokens (al dan niet met gebruikersclaims) aan client applicaties worden verstrekt. Hiermee kan de client (SaaS)applicatie namens de gebruiker de API bevragen.
* een Security Token Service (STS) voor het uitgeven, valideren, vernieuwen en beeindigen van security tokens en het veilig identificeren van een client (SaaS)applicatie. Hoort bij de Identity Provider.
* Een API Gateway voor de (toegangs)beveiliging van de API's. Een API Gateway is vaak onderdeel van een product voor "full life cycle API Management", en bevat ondersteuning voor het design, publiceren, documenteren, beveiligen en analyseren van APIs. Zie voor [productinspiratie](https://www.gartner.com/en/documents/3990768/magic-quadrant-for-full-life-cycle-api-management). Een API Gateway is een must have voor iedere gemeente die gevoelige API's aan afnemers aanbiedt.   
* Een proxy of een "facade" voor autorisatie op detailniveau. Deze proxy kun je als extra laag of intermediate endpoint (Zie REST constraint ["Layered System"](https://restfulapi.net/rest-architectural-constraints/#layered-system)) aan de architectuur toevoegen, in dit geval om het request naar de LV API conform de rechten van de gebruiker of applicatie aan te passen (met de fields parameter) en de response hierop te controleren. Of om te controleren of de gebruiker bijvoorbeeld kadastraal onroerende zaken mag zoeken op BSN. Dit kun je beleggen bij een product dat gebruik maakt van een API Gateway framework, maar je kunt er ook voor kiezen om dit te laten uitvoeren door een gespecialiseerde applicatie. 

## beheren van identiteit, rollen en rechten  
Bij voorkeur wordt de Identity Provider gevoed door een IAM systeem, waarmee je de rollen en rechten van jouw gebruikers voor verschillende systemen centraal kunt beheren. Een zogenaamde "identity governance and administration" oplossing automatiseert bijvoorbeeld het creëren, updaten en verwijderen van accounts, behandelt aanvragen van gebruikers om toegang te krijgen tot een bepaald systeem, en beheert wachtwoorden, gebruikerstoegang en toegangscertificeringsprocessen. Je kunt het IAM systeem koppelen aan het HR systeem, zodat een medewerker bijvoorbeeld automatisch alle rechten verliest op het gebruik van BRP gegevens als hij/zij voor een andere afdeling gaat werken. Zo'n IAM voorziening is optioneel maar heel belangrijk, zeker als je in een wat grotere gemeente werkt. Het beheren van veel gebruikers met verschillende accounts in allerlei processystemen zoals nu in veel gemeenten gebruikelijk is, is een beveiligingsrisico.

## controle achteraf: logging en protocollering
Een gemeente moet beschikken over een centrale logging- of protocolleringsvoorziening, waarin API requests en evt. ook responses onweerlegbaar worden vastgelegd, samen met het token dat de identiteit en claims van de eindgebruiker bevat. Door de API Gateway te laten loggen en de toegangsbeveiling voor nieuwe applicaties te baseren op eindgebruikercredentials hoef je straks in de meeste gevallen niet meer te protocolleren in de afnemende applicatie. Ook kun je burgerverzoeken in het kader van de AVG beter en sneller afhandelen door de logginggegevens te verrijken met de informatie uit je verwerkingsregister. Voor het verzamelen, opslaan en analyse van de logging kun je bijvoorbeeld gebruik maken van de Elastic ELK Stack, Splunk, LogRhythm, Graylog etc.    

# Hoe komen we daar?
## Stap 1
Haal Centraal maakt aansluiten goedkoper en vermindert het aantal lokale kopieën, te beginnen bij het gegevensmagazijn/distributriesysteeem. Wil je deze besparing op korte termijn inboeken, dan moet je alle afnemers van het gegevensmagazijn aansluiten op API's, ook de legacy applicaties. Daarvoor geldt dat je, totdat je opnieuw aanbesteedt, de applicatie een systeemaccount (met een passende autorisatie) kunt geven, en eventueel "vertalers" kunt gebruiken die de API gegevens omzetten in oudere formaten. Gebruikersbeheer, autorisatie en logging op gebruikersniveau blijft dan onveranderd. Om deze stap te kunnen zetten heb je alleen een *API Gateway* nodig en een *proxy voor autorisatie op detailniveau* (evt. via een API Gateway framework). Met deze voorziening(en) kun je dus al van start met de transitie!
## Stap 2
Ga je gebruik maken van een moderne SaaS oplossing, of wil je alle HC bevragen API's ontsluiten in een generieke viewer voor alle medewerkers van de gemeente, dan is dát het moment om een *Identity provider/STS* te introduceren, en tegelijkertijd een start te maken met een *centrale logging- en protocolleringsvoorziening*. Begin met het aansluiten van 1 applicatie, en breid dit langzaam uit voor alle nieuwe (opnieuw aanbestede) applicaties. Zo kun je al doende leren en verbeteren, en jouw organisatie in eigen tempo verder professionaliseren.  
## Stap 3
Naarmate jouw landschap meer applicaties bevat die gebruik maken van rollen en rechten in een Identity Provider, hoe belangrijker het wordt om identiteiten, rollen en rechten centraal te beheren met een *IAM systeem*. Daarbij speelt ook de grootte van jouw gemeente een rol. Stel jezelf de vraag wanneer de provisioning van jouw Identity Provider niet meer goed lukt via een formulier of een beheerder. Houd er rekening mee dat een IAM project, zeker in een grote gemeente 30% technisch, en 70% cultuur is! Begrip van de noodzaak en draagvlak van management en HR afdeling is een absolute voorwaarde voor succes. Daar kun je nu al mee beginnen... :-)!    

## Contact 
* Product Owner: [Cathy Dingemanse](mailto:cathy.dingemanse@denhaag.nl) 
* Ontwikkelaar: [Melvin Lee](mailto:melvin.lee@iswish.nl) 

