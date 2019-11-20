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