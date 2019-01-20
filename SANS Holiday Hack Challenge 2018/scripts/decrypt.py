#!/usr/bin/env python

# PS C:\Users\ptwa> echo $(B2H $b_k)
# 5da1d0617617f434b38658d3fda61e8b
# PS C:\Users\ptwa> echo $p_k_e_k
# 53d1874febfd734aca720cb8318893c6d8f144037f70f1b305ab74f8901fa15e09c0dddb683b60cfd455a71ca20e39c55aa1562bcdddacc7318a2d09
# b4b970bf0913aab03c54978c227b89b51e7f339f2966d848dbf0a7482f44ea52541ec840b6701e31f1b16f4610e3c0c77062354ddea8ed881a3bf81e
# b103d2c8422d77b37bbacd06cb1cbc38e5ce384ca197e2a49ffa6890479afb8a5d8d27442ad7fbf5052e8c483fa5a7eb5d9b9ad1c6edac2d377e2538
# 09d07384e180db28f6d9f961ebb071f1262c3f3bcf32f63336e163d6994bfed6dc48b11dada9c9f46eddb9807e7f1edd83bd93c03008c554cbb39133
# 69e251b127e202aa96f56f12aec550e3

from Crypto.Cipher import AES
from Crypto.PublicKey import RSA
from Crypto.Cipher import PKCS1_OAEP
from struct import unpack

ENCRYPTED = open('alabaster_passwords.elfdb.wannacookie', 'rb').read()
pkek = open('p_k_e_k.txt', 'rb').read().decode('hex')
rsa = RSA.importKey(open('wanna_cookie.key').read())

cipher = PKCS1_OAEP.new(rsa)
pk = cipher.decrypt(pkek)

IV_len = unpack('I', ENCRYPTED[:4])[0]
print('IV length: %d' % IV_len)
IV = ENCRYPTED[4:4+IV_len]
print('IV: %s' % IV.encode('hex'))

print('\nTrying key [%s]...' % pk.encode('hex'))
with open('alabaster_passwords.elfdb', 'wb') as f:
  try:
    f.write(AES.new(pk, AES.MODE_CBC, IV).decrypt(ENCRYPTED[4+IV_len:]))
  except Error as err:
    print err
      