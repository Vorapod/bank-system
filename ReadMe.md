# Database
[Table: Account](#tbl_account)  
[Table: Transaction](#tbl_transaction)

***
# API Specification  
[POST: /api/accounts](#post) - Adding new an account  
[POST: /api/accounts/{IBANNumber}/deposit](#deposit) - Deposite  
[POST: /api/accounts/transfer](#transfer) - Transfer  
[GET: /api/accounts/{IBANNumber}](#get) - Get account by IBAN number
***
<a name="tbl_account"></a>
## Table: Account
| Fields    |     Type      |Required| Description | Example
|---------- |:------------- |:----:|:-----------|:-----------| 
| IBANNumber (PK)   |  nchar (18)   |true| IBAN number | NL12ABCD3456789012
| Name |  nvarchar(50)|true| Account name | Foo Bar
| IsActive    |  bit     |true| Account status | 1 = Active, 0 = Inactive
| CurrentBalance    |  float     |true| Account balance | 1000.00
| CreatedDate    |  datetime     |true| Account created date | 2021-01-16T12:34:26.22
***
<a name="tbl_transaction"></a>
## Table: Transaction
| Fields    |     Type      |Required| Description | Example
|---------- |:------------- |:----:|:-----------|:-----------| 
| id (PK)   |  int (auto)   || Identity number | 1
| IBANNumber (PK)   |  nchar (18)   |true| IBAN number | NL12ABCD3456789012
| Type    |  int     |true| Transaction type | 1 = Deposit, 2 = Transfer
| StatementType    |  int     |true| Statement type | 1 = Debit, 2 = Credit
| Amount    |  float     |true| Transaction amount | 1000.00
| Fee    |  float     |true| Transaction fee | 1.00
| OutStandingBalance    |  float     |true| Balance after completing the transaction | 999.00
| CreatedDate    |  datetime     |true| Transaction created date | 2021-01-16T12:34:26.22
| PartnerIBANNuberRef    |  nchar (18)   |false| In case of transferring if sender used to refer to the recipient account and receiver used to refer to the sender account | NL99ABCD09876543212
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
<a name="deposit"></a>
## URL: `/api/accounts/{IBANNumber}/deposit`
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
<a name="transfer"></a>
## URL: `/api/accounts/transfer`
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
	"SenderIBANNumber": "NL12ABCD3456789012",
	"ReceiverIBANNumber": "NL99ABCD09876543212",
	"Amount": 1000
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
    "CurrentBalance": 899,
    "CreatedDate": "2021-01-16T12:34:07.243",
    "Transactions": [
        {
            "Id": 1,
            "IBANNumber": "NL12ABCD3456789012",
            "Type": 1,
            "StatementType": 1,
            "Amount": 1000,
            "Fee": 1,
            "OutStandingBalance": 999,
            "CreatedDate": "2021-01-01T12:34:26.22",
            "PartnerIBANNuberRef": null
        },
        {
            "Id": 2,
            "IBANNumber": "NL12ABCD3456789012",
            "Type": 2,
            "StatementType": 2,
            "Amount": -100,
            "Fee": 0,
            "OutStandingBalance": 899,
            "CreatedDate": "2021-01-02T12:35:57.786778+07:00",
            "PartnerIBANNuberRef": "NL99ABCD09876543212"
        }
    ]
}
```
***
<a name="get"></a>
## URL: `/api/account/{IBANNumber}`
## METHOD: `GET`
### Request
Header
```json
{
   "Content-Type": "application/json"
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
    "CurrentBalance": 899,
    "CreatedDate": "2021-01-16T12:34:07.243",
    "Transactions": [
        {
            "Id": 1,
            "IBANNumber": "NL12ABCD3456789012",
            "Type": 1,
            "StatementType": 1,
            "Amount": 1000,
            "Fee": 1,
            "OutStandingBalance": 999,
            "CreatedDate": "2021-01-01T12:34:26.22",
            "PartnerIBANNuberRef": null
        },
        {
            "Id": 2,
            "IBANNumber": "NL12ABCD3456789012",
            "Type": 2,
            "StatementType": 2,
            "Amount": -100,
            "Fee": 0,
            "OutStandingBalance": 899,
            "CreatedDate": "2021-01-02T12:35:57.786778+07:00",
            "PartnerIBANNuberRef": "NL99ABCD09876543212"
        }
    ]
}
```
***