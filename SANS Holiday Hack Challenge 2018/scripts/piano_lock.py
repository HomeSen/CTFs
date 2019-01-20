#!/usr/bin/env python

import requests

tune = ['E', 'D#', 'E', 'D#', 'E', 'E', 'D#', 'E', 'F#', 'G#', 'F#', 'G#', 'A', 'B', 'A#', 'B', 'A#', 'B']
keys = {'C':0, 'C#':1, 'D':2, 'D#':3, 'E':4, 'F':5, 'F#':6, 'G':7, 'G#':8, 'A':9, 'A#':10, 'B':11}
nums = ['C', 'Csh', 'D', 'Dsh', 'E', 'F', 'Fsh', 'G', 'Gsh', 'A', 'Ash', 'B', 'C']
URL = 'https://pianolock.kringlecastle.com/checkpass.php?i=%s&resourceId=9953dfa5-49bd-441d-a5fd-b154af72f24a'

def transpose(key):
  k = ''
  for i in xrange(len(tune_nums)):
    k += nums[tune_nums[i] + key]
    
  rsp = requests.get(URL % k)
  if not 'offkey' in rsp.text and not 'Incorrect' in rsp.text:
    print(k)
    print(rsp.text)
    return True
    
  return False


tune_nums = []
for t in tune:
  tune_nums.append(keys[t])


if transpose(1):
  exit(0)

for i in xrange(-1, -4, -1):
  if transpose(i):
    exit(0)