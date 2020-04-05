# Bol.com webshop

Mika Krooswijk, 2111998

Whitney Cheung, 2113440

05-04-2020

---

## Git

Dit project is gehost op [Github]([https://github.com/mikakrooswijk/afstuderenlight](https://github.com/mikakrooswijk/afstuderenlight). Het project kan gedownload worden, of geclonet met het volgense command:

```shell
git clone https://github.com/mikakrooswijk/afstuderenlight.git
```



## Project starten

Om het project te starten moet Docker desktop geinstaleerd zijn op de machine. Als dit het geval is kan het volgende command in de root map van de repository worden uitgevoerd om het project op te starten. 

```shell
docker-compose up
```

Dit bouwt de projecten en download de benodigde images. vervolgens zijn de services beschikbaar op de volgende porten:

| Bestellingen | localhost:5006/api/bestellingen |
| ------------ | ------------------------------- |
| Webshop      | localhost:5007/api/bestellingen |





Op deze endpoints kan een POST of GET request worden gebruikt op de volgende manier:



### Webshop Service



bestelling object in json:

```json
{
    "klant": "klantnaam"
    "product": "productnaam"
}
```

| Method | Endpoint          | JSON body  | Response type |
| ------ | ----------------- | ---------- | ------------- |
| GET    | /api/bestellingen | -          | JSON array    |
| POST   | /api/bestellingen | bestelling | 200 OK        |

### Bestelling Service

| Method | Endpoint          | JSON body | Response type |
| ------ | ----------------- | --------- | ------------- |
| GET    | /api/besetllingen | -         | JSON array    |


