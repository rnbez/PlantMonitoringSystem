import getpass, httpclient, api, json

def getUserAndPass():
    username = raw_input("Username: ")
    password = getpass.getpass()
    return {"username": username,
            "pass": password}

def authenticate():
    response = httpclient.post(api.__authenticate__, getUserAndPass())
    if response.status == 200:
        usr = json.loads(response.body)
        api.AUTH_TOKEN = usr['token']
    return response
