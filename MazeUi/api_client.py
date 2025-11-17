import requests

API_URL = "http://localhost:5131/api/solve/solve"


def call_solver(payload):
    print("\n====== SENDING TO API ======")
    print(payload)

    response = requests.post(API_URL, json=payload)

    print("\n====== RAW RESPONSE ======")
    print("Status:", response.status_code)
    try:
        print("Body:", response.text)
    except:
        pass

    response.raise_for_status()
    return response.json()
