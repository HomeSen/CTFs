#!/usr/bin/env python

from PIL import Image, ImageSequence

img = Image.open('fast_response_code.gif', 'r')

frames = [frame.copy() for frame in ImageSequence.Iterator(img)]

i = 0
for f in frames:
	f.save('out/%02d.gif' % i)
	i+=1
	
