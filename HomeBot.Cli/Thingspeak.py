import urllib, urllib2, json


class ThingspeakClient(object):
    """description of class"""
    
    
    def __init__(self, apiKey):
        self._apiKey = apiKey
        self._apiUrl = "https://api.thingspeak.com/update"

    

    def send(self, model):
        
        par = { 'api_key': self._apiKey }
        parCount = 0
        for z in model:
            if z.startswith('field'): # and (str(model[z])!='0'):
                par[z] = model[z]
                parCount+=1

        if parCount > 0:
            params = urllib.urlencode(par)
            print('Send to thingspeak: {0}'.format(params));
        
            try:
                response = urllib2.urlopen(self._apiUrl, params).read()
                print('{0}'.format(response))
            except urllib2.HTTPError as err:
                print("Exception: {0}".format(err))
            except:
                print("Unhadled error:", sys.exc_info()[0])
            pass


