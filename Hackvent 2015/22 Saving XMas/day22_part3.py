#!/usr/bin/env python

from Crypto.PublicKey import RSA
import gmpy2

N=0x8ED6168915ED61C560B04D212FC032C107B8BA9BF1179B97DEABFA71F111E749
C='39C9FB8503B3F73BB24069AFE0F2C0416177A40EE60E57134C00ABE8BEDE45BD'

# RFC
e = 65537

# factordb.com
p =  237024794671302122535260220812153587643
q = 272573531366295567443756143024197333707

print "N = %d\ne = %d\np = %d\nq = %d" % (N,e,p,q)
print "N = p*q? %s" % (p*q==N)

d = gmpy2.invert(e,(p-1)*(q-1))
print "d = %d\n" % d

privKey = RSA.construct((long(N), long(e), long(d), long(p), long(q)))
message = privKey.decrypt(C.decode('hex'))
print '\nPlain text:\n' + message + '\n\n'