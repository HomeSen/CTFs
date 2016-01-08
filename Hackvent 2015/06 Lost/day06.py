#!/usr/bin/env python3

import base64
import codecs

text = 'HR7DYQ3ON4TC6U2AFAZDGJK3J44TYXZUNRCTATK2GEZDGJR5JJUC6ULUFQZEI5L6HY======'
print('\t' + text)

text = base64.b32decode(text)
print('B32:\t' + str(text)[2:-1])

text = base64.a85decode(text, adobe=True)
print('A85:\t' + str(text)[2:-1])

text = codecs.encode(str(text), 'rot-13')
print('R13:\t' + str(text)[2:-1])