#!/usr/bin/env python

import string

cipher = "iw, hu, fv, lu, dv, cy, og, lc, gy, fq, od, lo, fq, is, ig, gu, hs, hi, ds, cy, oo, os, iu, fs, gu, lh, dq, lv, gu, iw, hv, gu, di, hs, cy, oc, iw, gc"

text = ''

for c in cipher.translate(string.maketrans('xzcvsqlgbfiojuhwndyt', '10399583568702741624')).split(', '):
	text += chr(int(c))
	
print text