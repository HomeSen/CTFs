#!/usr/bin/env python

import string
from hashlib import sha1

code = 'HV15-g_uj-1yq7-_dyc-2wlr-e6_j'
lc_hash = 'B39ECFBC2C64ADBB7C7A9292EEE31794D28FE224'
hash = '0D353038908AD0FC8C51A5312BB3E2FEE1CDDF83'

FOUND=False
lc_chars = string.ascii_lowercase + string.digits

for a in lc_chars:
	if FOUND:
		break
			
	for b in lc_chars:
		if FOUND:
			break			

		for c in lc_chars:
			if FOUND:
				break
			
			test = code.lower()
			test = test[:6] + a + test[7:15] + b + test[16:27] + c + test[28:]
			
			if sha1(test).hexdigest().upper() == lc_hash:
				FOUND = True
				code = code[:6] + a + code[7:15] + b + code[16:27] + c + code[28:]
				print '[+] Found lower-case code: ' + code
				
if FOUND == False:
	print '[-] Could not find matching lower-case code!'
	exit()
	
for a in (code[5].lower(), code[5].upper()):																	# g
	for b in (code[6].lower(), code[6].upper()):																# _
		for c in (code[7].lower(), code[7].upper()):															# u
			for d in (code[8].lower(), code[8].upper()):														# j
				for e in (code[11].lower(), code[11].upper()):												# y
					for f in (code[12].lower(), code[12].upper()):											# q
						for g in (code[15].lower(), code[15].upper()):										# _
							for h in (code[16].lower(), code[16].upper()):									# d
								for i in (code[17].lower(), code[17].upper()):								# y
									for j in (code[18].lower(), code[18].upper()):							# c
										for k in (code[21].lower(), code[21].upper()):						# w
											for l in (code[22].lower(), code[22].upper()):					# l
												for m in (code[23].lower(), code[23].upper()):			# e
													for n in (code[25].lower(), code[25].upper()):			# e
														for o in (code[27].lower(), code[27].upper()):		# _
															for p in (code[28].lower(), code[28].upper()):	# j
																test = code
																test = test[:5] + a + b +c + d + test[9:11] + e + f + test[13:15] + g + h + i + j + test[19:21] + k + l + m + test[24:25] + n + test[26] + o + p
																
																if sha1(test).hexdigest().upper() == hash:
																	code = test
																	print '[+] Found code: ' + code
																	exit()

print '[-] Could not find matching code!'