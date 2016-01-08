#!/usr/bin/env python

def frombits(bits):
	chars = []
	for b in range(len(bits) / 8):
		byte = bits[b*8:(b+1)*8]
		chars.append(chr(int(''.join([str(bit) for bit in byte]), 2)))
	return ''.join(chars)

#0 = pagh
#1 = wa'
#10 = wa'maH
#100 = wa'vatlh
#1000 = wa'SaD
#10000 = netlh
#100000 = bIp
#1000000 = 'uy'

text = ''
with open('Klag.txt', 'r') as f:
	text = f.read()
	
text = text.replace("wa'SaD", "1000").replace("SaD", "1000")
text = text.replace("wa'vatlh", "100").replace("vatlh", "100")
text = text.replace("wa'maH", "10").replace("maH", "10")
text = text.replace("netlh", "10000")
text = text.replace("wa'", "1")
text = text.replace("pagh", "0")
text = text.replace(" ", "")

print text + "\n\n" + frombits(text)