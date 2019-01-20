#!/usr/bin/env python

import requests

def de_bruijn(k, n):
  """
  de Bruijn sequence for alphabet k
  and subsequences of length n.
  Source: https://en.wikipedia.org/wiki/De_Bruijn_sequence#Algorithm
  """
  try:
    # let's see if k can be cast to an integer;
    # if so, make our alphabet a list
    _ = int(k)
    alphabet = list(map(str, range(k)))

  except (ValueError, TypeError):
    alphabet = k
    k = len(k)

  a = [0] * k * n
  sequence = []

  def db(t, p):
    if t > n:
      if n % p == 0:
        sequence.extend(a[1:p + 1])
    else:
      a[t] = a[t - p]
      db(t + 1, p)
      for j in range(a[t - p] + 1, k):
        a[t] = j
        db(t + 1, t)
  db(1, 1)
  return "".join(alphabet[i] for i in sequence)
  
  
print('Calculating De Bruijn sequence for k=4, n=4 ...')
sequence = de_bruijn(4, 4)
# add the first 3 chars in order to honor wrapping
sequence += sequence[:3]

print('Trying potential keys ...')
for i in xrange(len(sequence) - 3):
  key = sequence[i:i+4]
  rsp = requests.get('https://doorpasscode.kringlecastle.com/checkpass.php?i=' + key + '&resourceId=802432c0-0246-4a2a-ba37-1da75bbcf6f4')
  if '"success":true' in rsp.text:
    print('Found key: ' + key)
    print('Server response:\n' + rsp.text)
    break
    
print('Done.')