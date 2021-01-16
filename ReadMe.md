[POST: /api/accounts](#post) - Adding new an account

***
<a name="post"></a>
## URL: `/api/v1/account`
## METHOD: `POST`
### Request
Header
```json
{
   "Content-Type": "application/json"
}
```
Body
```json
{
   "IBANNumber": "NL59ABNA1458509540",
   "Name": "Foo Bar"
}
```
### Response
Header
```json
{
   "Content-Type": "application/json"
}
```
Body _Success_
```json
{
    "IBANNumber": "NL59ABNA1458509540",
    "Name": "Foo Bar",
    "IsActive": 1,
    "CurrentBalance": 0,
    "CreatedDate": "2021-01-16T12:34:59.9201838+07:00",
    "Transactions": []
}
```
***