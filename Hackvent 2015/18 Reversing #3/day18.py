#!/usr/bin/env python

import string
from hashlib import md5
from itertools import product
from base64 import b64encode

flag = [ 'HV15', '', '', '', '', '' ]
hashes = [
'fab3e420d6d8a17b53b23ca4bb01866b',
'189f56eea9a9ba305dffa8425ba20048',
'2335667c646346b38c8f0f47b13fab13',
'f4709a7eef9d703920b910fc734b151c',
'b74e57f21f5a315550a9e2f6869d4e44',
'40abc257b6f0e0420dc9ae9ba19c8c8c'
]
parts_found = 0

if md5(b64encode(flag[0])).hexdigest() != hashes[0]:
	print '[-] Fatal error. Static part\'s hash doesn\'t match :/'
	exit()
else:
	print '[+] Initial test successfull.'
	parts_found += 1
	
for c in product(string.ascii_letters + string.digits, repeat=4):
	if parts_found == 6:
		break
		
	s = ''.join(c)
	print('\r[*] Trying: %s ...' % s),
	
	h = md5(b64encode(s)).hexdigest()
	for i in xrange(1,6):
		if h == hashes[i]:
			print '\n[+] Found part #%i: %s' % (i,s)
			flag[i] = s
			parts_found += 1
			break
			
print '\n\n' + '-'.join(flag) + '\n'
print 'Done.'