#!/usr/bin/env python

data = ''
output = ''

with open('pass', 'rb') as f:
	data = f.read()

for i in range(0, len(data)/4):
	output += data[4*i+3] + data[4*i+2] + data[4*i+1] + data[4*i]
	
with open('pass.mp3', 'wb') as f:
	f.write(output)
	