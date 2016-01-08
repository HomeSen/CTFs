#!/usr/bin/env python

import telnetlib

prev = 2
cur = 3

HOST = 'challenges.hackvent.hacking-lab.com'
PORT = 8888

tn = telnetlib.Telnet(HOST, PORT)

print tn.read_until('... ?')

send = prev+cur
prev = cur
cur = send
print send
tn.write(str(send) + '\r\n')

rsp = tn.read_until('...')
print rsp

while rsp.strip()=='ACK, go ahead...':
	send = prev+cur
	prev = cur
	cur = send
	tn.write(str(send) + '\r\n')

	rsp = tn.read_until('...')
	print rsp