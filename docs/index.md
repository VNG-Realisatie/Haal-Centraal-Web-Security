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

# Wat is veilig gebruik van een HC API met vertrouwelijke gegevens?
* Dat uitsluitend geautoriseerde (gebruikers van) applicaties toegang krijgen tot de gegevens die de API levert
* Dat gebruikers uitsluitend toegang krijgen tot gegevens waarvoor zij zijn geautoriseerd
* Dat een (SaaS)applicatie een uitsluitend toegang krijgt tot de gegevens namens een gebruiker van jouw gemeente, dus alleen als jouw medewerker achter de knoppen zit!

# Wat moet een gemeente daarvoor regelen?

## (Toegangs)beveiliging, autorisatie en filtering
Gemeenten bieden een breed palet aan producten en diensten die allemaal een andere minimale set gegevens en functionaliteit nodig hebben. Onze diensten worden geleverd door veel verschillende medewerkers met verschillende rollen en rechten. We kunnen (nog) niet aan een landelijke voorziening vragen om hiervoor de toegangsbeveiliging en autorisatie te organiseren. Dat moet een gemeente zelf doen. Minimaal is nodig:
* Een Identity Provider (IP) voor het authenticeren van de eindgebruiker waarin de claims voor het gebruik van de API van alle gebruikers van jouw gemeente centraal zijn vastgelegd. Nadat de Identity provider heeft vastgesteld wie de ingelogde gebruiker is, kan deze claims in een token aan allerlei applicaties worden verstrekt.
* een STS (Security Token Service) voor het veilig identificeren van een client (SaaS)appliactie
* Een API Gateway voor de (toegangs)beveiliging van de API's. Een API Gateway is vaak onderdeel van een product voor "full life cycle API Management", en bevat ondersteuning voor het design, publiceren, documenteren, beveiligen en analyseren van APIs. Zie voor [productinspiratie](https://www.gartner.com/en/documents/3990768/magic-quadrant-for-full-life-cycle-api-management). Een API Gateway is een must have voor iedere gemeente die gevoelige API's aan afnemers aanbiedt.   
* Een proxy of een "facade" voor autorisatie op detailniveau. Deze proxy kun je als extra laag of intermediate endpoint (Zie REST constraint ["Layered System"](https://restfulapi.net/rest-architectural-constraints/#layered-system)) aan de architectuur kunt toevoegen, in dit geval om het request naar de LV API conform de rechten van de gebruiker of applicatie aan te passen (met de fields parameter) en de response hierop te controleren. Dit kun je prima beleggen bij een API Gateway, een ESB, maar je kunt er ook voor kiezen om dit te laten uitvoeren door een applicatie die bijvoorbeeld andere gegevensbronnen (zonder API's) voor jouw afnemers beschikbaar maakt en integreert met (HC) API's. 


## vastleggen, vaststellen en verstrekken van identiteit, rollen en rechten 
 Bij voorkeur wordt de Identity Provider gevoed door een IAM systeem, waarmee je de rollen en rechten van jouw gebruikers voor verschillende systemen centraal kunt beheren. Een zogenaamde "identity governance and administration" oplossing automatiseert bijvoorbeeld het creeeren, updaten en verwijderen van accounts, behandelt aanvragen van gebruikers om toegang te krijgen tot een bepaald systeem, en beheert wachtwoorden, gebruikerstoegang en toegangscertificeringsprocessen. In de gemeente Den Haag is het IAM systeem bijvoorbeeld gekoppeld aan het HR systeem, zodat een medewerker automatisch alle rechten verliest op het gebruik van BRP gegevens als hij/zij voor andere afdeling gaat werken. Zo'n IAM voorziening is optioneel maar heel belangrijk, zeker voor de wat grotere gemeenten. Het beheren van veel gebruikers met verschillende accounts in allerlei processystemen zoals nu in veel gemeenten gebruikelijk is, is dan een beveiligingsrisico.

## logging en protocollering
Een gemeente moet beschikken over een logging- of protocolleringsvoorziening, waarin API requests en evt. ook responses onweerlegbaar worden vastgelegd, samen met het token met de identiteit, rollen en rechten van de eindgebruiker. Door de API Gateway te laten loggen en de toegangsbeveiling in toenemende mate te baseren op eindgebruikercredentials hoef je in de meeste gevallen niet meer te protocolleren in de afnemende applicatie, en kun je burgerverzoeken in het kader van de AVG beter en sneller afhandelen. Voor het verzamelen, opslaan en analyse van de logging kun je bijvoorbeeld gebruik maken van de Elastic (ELK) Stack, Splunk, LogRhythm, Graylog etc.    


## Contact 
* Product Owner: [Cathy Dingemanse](mailto:cathy.dingemanse@denhaag.nl) 
* Ontwikkelaar: [Melvin Lee](mailto:melvin.lee@iswish.nl) 

