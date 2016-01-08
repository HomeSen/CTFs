#!/usr/bin/env python

from suds.client import Client
import subprocess

client = Client('http://hackvent.org/DailyS04p/server.php?WSDL')
print client
session = client.service.getSession()
print 'SessionID: ' + session

for i in range(0,20):
  quest = client.service.getQuest(session)

  print '\n%2d. Quest: %s' % (i, quest)

  res = quest.replace('x*y*z = ', '')
  res = subprocess.check_output((['factor', res]))
  print res
  
  (s,x,y,z) = res.split(' ')
  solution = x.strip()+'*'+y.strip()+'*'+z.strip()
  rsp = client.service.submitSolution(session, solution)
  print rsp
