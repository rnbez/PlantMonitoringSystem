import getpass, httpclient, api, json

__auth_data__ = None

def getUserAndPass():
    username = raw_input("Username: ")
    password = getpass.getpass()
    return {"username": username,
            "pass": password}

def authenticate():
    global __auth_data__

    if __auth_data__ is None:
        __auth_data__ = getUserAndPass()

    response = httpclient.post(api.__authenticate__, __auth_data__)
    if response.status == 200:
        usr = json.loads(response.body)
        api.AUTH_TOKEN = usr['token']
    return response

def checkResponse(response):
    global __auth_data__
    if response.status == 403:
        return authenticate()
    return
