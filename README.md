# Payment Gateway

Payment Gateway solution to experiment with .Net Core 3.0, NLog, CircleCI and Docker.  



[![CircleCI](https://circleci.com/gh/alex75it/payment-gateway.svg?style=svg)](https://circleci.com/gh/alex75it/payment-gateway)


## Web API Docker image

Every commit that pass the unit tests generates a new docker image that will be published on a public Docker repository.  

To run and test the Docker image locally:  
``docker run -d -p 8080:80 --name payment alessandropiccione/payment-gateway:0.1.100``

``curl http://localhost:8080/health``  
or  
``curl http://localhost:8080/metrics``

---

## Mocking

The current API uses a mocked Bank client and a mocked storage.  
The Payments are always accepted (no validation) and stored in memory, 
so that the returned _Payment ID_ can be used to query the service.


---

## Web API documentation

Any endpoint can return an ERROR (HTTP Status: _500_).  

Common JSON payload for error response below:
```json
{
    "status": "error",
    "error": "General Error"
}
```

### - Process a payment
```
POST /payments  
```

**Headers:**  
Content-Type: _application/json_

**Parameters in the body:**

Name         | Type   | Mandatory 
------------ | ------ | --------- 
CardNumber   | String | Yes       
CardHolder   | String | Yes       
ExpiryYear   | Number | Yes       
ExpiryMonth  | Number | Yes       
CCV          | Number | Yes       
Amount       | Number | Yes       
Currency     | String | Yes       

Example:  
```json
{
    "CardNumber": "1234-1234-1234-1234",
    "CardHolder": "mr John Do",
    "ExpiryYear": 2025,
    "ExpiryMonth": 5,
    "CCV": 1234,
    "Amount": 100.23,
    "Currency": "EUR"
}

```

**Responses:**

#### Succes (Created)
HTTP Status: _201_  
Content-Type: _application/json_  
```json
{
    "paymentId": "0b389436-7acc-40ab-a009-49d1bdc5ff62"
}
```

#### Validation error or unprocessable payment
HTTP Status: _400_  

Content-Type: _text/plain_  
_Payment amount cannot be zero or negative_


### - Retrieve an executed payment
```
GET /payments/<paymentId>
```

**Responses:**  

#### Success  
HTTP Status: _200_  

Content-Type: _application/json
```json
{
    "id": "0b389436-7acc-40ab-a009-49d1bdc5ff62",
    "executionDate": "2019-09-20T21:24:17.6330953Z",
    "cardNumber": "XXXX-XXXX-XXXX-1234",
    "amount": 100.23,
    "currency": "EUR"
}
```


#### Not Found
HTTP Status: _404_  

No content.  


### - Status Check
```
GET /health
```

Both text and JSON requests are accepted.  

Response body for Content-Type=_text/html_:  
```
OK
```

Response body for Content-Type=_application/json_:  
```json
{
    "status": "ok"
}
```


### - Service diagnostics
```
GET /metrics
```

Returns some values indicating the status and usage of the service.  



**Response:**  
```json
{
    "upSince": "2019-09-20T21:50:29.2284655Z",
    "totalErrors": 1,
    "createdPayments": 6,
    "failedPayments": 1,
    "lastPaymentCreationTime": 0
}
```


--- 

## Known issues

NLog seems not writing the logs properly in the Docker container (it's ok on local machine).  
Need to be investigated.