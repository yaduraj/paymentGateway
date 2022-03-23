#!/bin/bash
 docker build -t paymentgatewayimage . --no-cache
 docker run -t -p 8080:80 --name paymentGateway paymentgatewayimage