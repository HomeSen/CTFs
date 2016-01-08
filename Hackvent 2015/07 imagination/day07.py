#!/usr/bin/env python

from math import sqrt
from PIL import Image

num = 0x1fc137f82a7a0dd05d76ebbcbb74815d82c720ff555fc018801f78baaf93c051d55e46346fd16dd457f54451df65fcec3a493768ffc00948aff4154e090627753ffebafa7ddd568860a87a3fd88eb
bits = bin(num)[2:]

print bits
print len(bits)
print sqrt(len(bits))

width = int(sqrt(len(bits)))

img = Image.new('RGB', (width+2,width+2), 'white')
px = img.load()

i = 0
for b in bits:
	if b == '1':
		px[(i%width)+1,int(i/width)+1] = (0,0,0)
	i += 1
	
img.resize(((width+2)*10,(width+2)*10)).save('flag.png')