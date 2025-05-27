## POST Request works by the followgin endpoint:

http://localhost:port/api/prescriptions

## GET request works by the following endpoint:

http://localhost:5234/api/patients/id

## Note:

The POST Request would create a new record only when we provide IdDoctor, otherwise we will get corresponding messag.

Working body to be used:(added "IdDoctor" field to the example Request from the task. Also Patient fields were fulfilled)

```json
{
    "patient": {
        "IdPatient": 1,
        "FirstName": "John",
        "LastName": "Doe",
        "BirthDate": "1984-04-07"
    },
    "medicaments": [
        {
            "IdMedicament": 1,
            "Dose": 3,
            "Description": "Some description..."
        }
    ],
    "Date": "2012-01-01",
    "DueDate": "2012-01-01",
    "IdDoctor": 1
}
