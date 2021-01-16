[POST: /api/accounts](#post) - Adding new an account  
[POST: /api/accounts/NL12ABCD3456789012/deposit](#deposit) - Deposite

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
   "IBANNumber": "NL12ABCD3456789012",
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
    "IBANNumber": "NL12ABCD3456789012",
    "Name": "Foo Bar",
    "IsActive": 1,
    "CurrentBalance": 0,
    "CreatedDate": "2021-01-16T12:34:59.9201838+07:00",
    "Transactions": []
}
```
***


***
<a name="deposit"></a>
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
   "amount": "1000"
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
    "IBANNumber": "NL12ABCD3456789012",
    "Name": "Foo Bar",
    "IsActive": 1,
    "CurrentBalance": 999,
    "CreatedDate": "2021-01-01T12:34:07.243",
    "Transactions": [
        {
            "Id": 10,
            "IBANNumber": "NL12ABCD3456789012",
            "Type": 1,
            "StatementType": 1,
            "Amount": 1000,
            "Fee": 1,
            "OutStandingBalance": 999,
            "CreatedDate": "2021-01-16T12:34:26.2196752+07:00",
            "PartnerIBANNuberRef": null
        }
    ]
}
```
***