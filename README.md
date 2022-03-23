1. Clone the repo from https://github.com/yaduraj/paymentGateway 
2. Linux /MacOS
   Run the following command from the root folder from the terminal: 
   sh startup.sh

   Windows
   Go to Powershell and cd to the root folder and run the following commands.
   1. bash
   2. sh startup.sh
3. Hit http://localhost:8080/Admin endpoint. Since the Acquiring bank is mocked, this endpoint allows you to configure the payment status from the acquiring bank. By default the payment status is set as successful.
   You can set the status to false and provide an error message for why is the API failing.
4. Hit http://localhost:8080 to access the main page to access the APIs.
   Here you can enter various values like Amount, Credit card number, CVV, expiry date and currency. Validation is provided for all fields.
5. If you wish to make separate curl calls and not use the UI, samples are provided at the bottom of the document.


Sample Curl Calls:

Curl call for getting a transaction
curl --location --request GET 'http://localhost:8080/Payments/transaction/{transactionID}'



Curl call for making a transaction
curl --location --request POST 'http://localhost:8080/Payments/transaction' \
--header 'Content-Type: application/json' \
--data-raw '{
    "CardNumber": "5555555555554444",
    "Amount": 1100,
    "CVV": "444",
    "ExpiryDate": "11/2023",
    "Currency": "GBP"
}'

Curl call to set the acquiring bank status

curl --location --request POST 'http://localhost:8080/Admin/SetBankStatus' \
--header 'Content-Type: application/json' \
--header 'Cookie: JSESSIONID.a4bb93c5=node01us4qkg3oig0m17i6ec6n9nn0h0.node0' \
--data-raw '{
    "IsSuccessful" : false,
    "ErrorMessage" : "Insufficient funds"
}'
![image](https://user-images.githubusercontent.com/1962992/159688903-ca372d29-7278-4802-bf33-b8077c51955c.png)

