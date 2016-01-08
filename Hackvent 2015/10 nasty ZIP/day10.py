#!/usr/bin/env python

from zipfile import ZipFile

filename = 'nasty-Shit.zip'
i=1
input_zip=ZipFile(filename)

while filename[-4:].lower() == '.zip':
		print '[*] Extracting ' + filename
		input_zip.extractall('out/')
		filename = 'out/' + input_zip.namelist()[0]
		input_zip=ZipFile(filename)
		i += 1
		
print '[+] Done.'