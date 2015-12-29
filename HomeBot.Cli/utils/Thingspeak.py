import urllib, urllib2, sys, json, time
import thread


class ThingspeakClient(object):
    """Communicates with thingspeak.com server """
    
    
    def __init__(self, apiKey):
        self._apiKey = apiKey
        self._apiUrl = "https://api.thingspeak.com/update"
        self._thingspeak_delay = 15
        self._lastSendTime = 0.0
        self._lastParamsSent = {}

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
                response = urllib.request.urlopen(self._apiUrl, params).read()
                print('{0}'.format(response))
                self._lastSendTime = time.time()
                self._lastParamsSent = par.copy()
            except urllib2.HTTPError as err:
                print("Exception: {0}".format(err))
            except:
                print("Unhadled error:", sys.exc_info()[0])
            pass

    def sendIfAvailable(self, model):
        if (self._lastSendTime + self._thingspeak_delay) < time.time():
            self.send(model)
            return True
        return False

    def sendIfAvailableAndChanged(self, model):
        
        if (self._lastSendTime + self._thingspeak_delay) < time.time():
            changed = False
            for z in model:
                if z not in self._lastParamsSent:
                    changed = True
                else:
                    if self._lastParamsSent[z] != model[z]:
                        changed = True
            #print('sendIfAvailableAndChanged {0} == {1}: {2}'.format(model, self._lastParamsSent, changed))
            if changed:
                self.send(model)
                return True
        return False