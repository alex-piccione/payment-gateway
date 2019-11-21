# Payment Gateway

Payment Gateway solution to experiment with .Net Core 3.0, NLog, CircleCI and Docker.  



[![CircleCI](https://circleci.com/gh/alex75it/payment-gateway.svg?style=svg)](https://circleci.com/gh/alex75it/payment-gateway)


## Docker image

The commit generate a new docker image that will be published on my public Docker repository.  

To run and test the Docker image locally:  
``docker run -d -p 8080:80 --name payment alessandropiccione/payment-gateway:0.1.92``

``curl http://localhost:8080/health``  
or  
``curl http://localhost:8080/metrics``



## API documentation

Any endpoint can return an ERROR, HTTP Status: 500.  

Sample payload below:
```json
{
    "status": "error",
    "error": "General Error"
}
```

### Process a payment
```
POST /payments  
```

**Headers:**  
Content-Type: _application/json_

**Parameters in the body:**

Name         | Type   | Mandatory | Description/Example
------------ | ------ | --------- | ------------
CardNumber   | String | Yes       | "1234-1234-1234-1234"
CardHolder   | String | Yes       | "mr John Do"
ExpiryYear   | Number | Yes       | 2025
ExpiryMonth  | Number | Yes       | 5
CCV          | Number | Yes       | 1234
Amount       | Number | Yes       | 100.23
Currency     | String | Yes       | "GBP"

**Responses:**

#### Succes/Created
HTTP Status: _201_
```javascript
{
    "paymentId": "0b389436-7acc-40ab-a009-49d1bdc5ff62"
}
```


HTTP Status: _400_



### Retrieve an executed payment
```
GET /payments/<payment-id>
```

**Responses:**  

HTTP Status: _200_  

```javascript
{
    "id": "0b389436-7acc-40ab-a009-49d1bdc5ff62",
    "executionDate": "2019-09-20T21:24:17.6330953Z",
    "cardNumber": "XXXX-XXXX-XXXX-XXXX",
    "amount": 100.23,
    "currency": "EUR"
}

