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

# Veilig gebruik van een HC API met vertrouwelijke gegevens, wat is dat?
* Dat uitsluitend geautoriseerde (gebruikers van) applicaties toegang krijgen tot de gegevens die de API levert
* Dat gebruikers uitsluitend toegang krijgen tot gegevens waarvoor zij zijn geautoriseerd
* Dat een (SaaS)applicatie een uitsluitend toegang krijgt tot de gegevens namens een gebruiker van jouw gemeente, dus alleen als jouw medewerker achter de knoppen zit!

# Wat moet een gemeente daarvoor regelen?
Gemeenten bieden een breed palet aan producten en diensten die allemaal een andere minimale set gegevens en functionaliteit nodig hebben. We kunnen (nog) niet aan een landelijke voorziening vragen om hiervoor de toegangsbeveiliging en autorisatie te organiseren. Dat moeten gemeenten dus zelf doen. Gemeenten hebben daarom minimaal de volgende voorzieningen nodig:
* Een API Gateway voor de (toegangs)beveiliging van de API's. Een API Gateway is vaak onderdeel van een product voor "full life cycle API Management", en bevat ondersteuning voor het design, publiceren, documenteren, beveiligen en analyseren van APIs. Zie voor product suggersties https://www.gartner.com/en/documents/3990768/magic-quadrant-for-full-life-cycle-api-management   
* Een voorziening die filtert op gegevens (API response) en toegang tot API functionaliteit conform de rechten van de gebruiker of applicatie. Dit kan overigens prima een API Gateway zijn. Je kunt er ook voor kiezen om deze filterfunctie te beleggen bij een voorziening die bijvoorbeeld andere gegevensbronnen (niet API's) voor jouw afnemers beschikbaar maakt en integreert met (HC) API's. 
* Een Identity Provider, waarin de rollen en rechten van alle gebruikers van jouw gemeente centraal zijn vastgelegd, zodat ze bijvoorbeeld in een token aan allerlei applicaties kunnen worden verstrekt. Bij voorkeur wordt de Identity Provider gevoed door een IAM systeem, waarmee je de rollen en rechten van jouw gebruikers voor verschillende systemen centraal kunt beheren. Een zogenaamde "identity governance and administration" oplossing automatiseert bijvoorbeeld het creeeren, updaten en verwijderen van accounts, behandelt aanvragen van gebruikers om toegang te krijgen tot een bepaald systeem, en beheert wachtwoorden, gebruikerstoegang en toegangscertificeringsprocessen. In de gemeente Den Haag is het IAM systeem gekoppeld aan het HR systeem, zodat een medewerker automatisch alle rechten verliest op het gebruik van BRP gegevens als hij/zij op andere afdeling gaat werken. Zo'n voorziening is optioneel maar heel belangrijk, zeker als je een wat grotere gemeente bent. Het beheren van meerdere accounts van jouw gebruikers in allerlei verschillende processystemen zoals nu in veel gemeenten gebruikelijk is, is een beveiligingsrisico.
* Een logging- of protocolleringsvoorziening, waarin API request en response onweerlegbaar worden vastgelegd, samen met het token met de identiteit, rollen en rechten van de eindgebruiker. Door de API Gateway te laten loggen en de toegangsbeveiling in toenemende mate te baseren op eindgebruikercredentials hoef je in de meeste gevallen niet meer te protocolleren in de afnemende applicatie, en kun je burgerverzoeken in het kader van de AVG beter en sneller afhandelen. Voor het verzamelen, opslaan en analyse van de logging wordt vaak gebruik gemaakt van Elastic (ELK) Stack, Splunk, LogRhythm, Graylog etc.    


## Contact 
* Product Owner: [Cathy Dingemanse](mailto:cathy.dingemanse@denhaag.nl) 
* Ontwikkelaar: [Melvin Lee](mailto:melvin.lee@iswish.nl) 

