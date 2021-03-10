---
layout: page-with-side-nav
title: Haal Centraal Web Security
---

# Haal Centraal Web-Security

Repo voor het ontwikkelen van security best practices voor het gebruik van Haal Centraal API's. Doel is te laten zien hoe je security kunt inrichten voor Haal Centraal API's aan de hand van referentie-implementaties, zodat je:

* inzicht krijgt in hoe het werkt, hoe het bij een gemeente kan worden ingericht, en welke componenenten je nodig hebt;
* in jouw gemeente kan testen of de componenten (Identity Provider, API Gateway) goed zijn ingericht
* als leverancier de inrichting kunt kopieren om tegenaan te ontwikkelen
* als leverancier kunt testen of jouw SAAS implementatie werkt met deze inrichting.

# Wat moet een gemeente regelen voor het gebruik van een HC API met vertrouwelijke gegevens?
* Dat uitsluitend geautoriseerde (gebruikers van) applicaties toegang krijgen tot de gegevens
* Dat gebruikers uitsluitend toegang krijgen tot gegevens waarvoor zij zijn geautoriseerd
* Dat een (SaaS)applicatie een uitsluitend toegang krijgt namens een gebruiker van jouw gemeente, dus alleen als die gebruiker achter de knoppen zit!

# Wat moet een gemeente daarvoor regelen?
Gemeenten bieden een breed palet aan producten en diensten die allemaal een andere minimale set gegevens en functionaliteit nodig hebben. We kunnen (nog) niet aan een landelijke voorziening vragen om hiervoor de toegangsbeveiliging en autorisatie te organiseren. Dat moeten gemeenten dus zelf doen.     
Gemeenten hebben daarom de volgende voorzieningen nodig:
* Een API Gateway voor de toegangsbeveiliging van de API's. Een API Gateway is vaak onderdeel van een product voor "full life cycle API Management", en bevat ondersteuning voor het design, publiceren, documenteren, beveiligen en analyseren van APIs. Zie voor product suggersties https://www.gartner.com/en/documents/3990768/magic-quadrant-for-full-life-cycle-api-management   
* Een voorziening die autorisatie uitvoert op de functionele mogelijkheden van de API (functie wel/niet gebruiken) en de inhoud van de response filtert (uitsluitend gegevens waarvoor men gerechtigd is). Dit kan overigens ook een API Gateway zijn. Of je kiest er voor om deze functionaliteit te beleggen bij een product dat bijvoorbeeld ook de integratie met andere gegvensbronnen (niet API's) voor jouw afnemers verzorgt. 
* Een Identity Provider, waarin de rollen en rechten van alle gebruikers van jouw gemeente centraal zijn vastgelegd. Bij voorkeur wordt deze gevoed door een IAM systeem, waarmee je de rollen en rechten van jouw gebruikers voor verschillende systemen centraal kunt beheren. Een zogenaamde "identity governance and administration" oplossing automatiseert bijvoorbeeld het creeeren, updaten en verwijderen van accounts, behandelt aanvragen van gebruikers om toegang te krijgen tot een bepaald systeem, en beheert wachtwoorden, gebruikerstoegang en toegangscertificeringsprocessen. Deze voorziening is optioneel maar heel belangrijk, zeker als je een wat grotere gemeente bent. Het beheren van meerdere accounts van jouw gebruikers in allerlei legacysystemen zoals nu in veel gemeenten gebruikelijk is, is een beveiligingsrisico.


## Contact 
* Product Owner: [Cathy Dingemanse](mailto:cathy.dingemanse@denhaag.nl) 
* Ontwikkelaar: [Melvin Lee](mailto:melvin.lee@iswish.nl) 

