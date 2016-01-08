#!/usr/bin/env python

from z3 import *

c, b, d, g, f, i, h, j, l, o, n, q, s, u, t, w, v, y, x, z = BitVecs('c b d g f i h j l o n q s u t w v y x z', 32)

def fn(*args):
  res = 0
  for idx in range(len(args)):
    res += args[idx] * 10**(len(args)-idx-1)
  return res

solver = Solver()

solver.add(fn(b,y,t,w,y,c,j,u) + fn(y,z,v,y,j,j,d,y) ^ fn(v,u,g,l,j,t,y,n) + fn(u,g,d,z,t,n,w,v) | fn(x,b,f,z,i,o,z,y) == fn(b,z,u,w,t,w,o,l))
solver.add(fn(w,w,n,n,n,q,b,w) - fn(u,c,l,f,q,v,d,u) & fn(o,n,c,y,c,b,x,h) | fn(o,q,c,n,w,b,s,d) ^ fn(c,g,y,o,y,f,j,g) == fn(v,y,h,y,j,i,v,b))
solver.add(fn(y,z,d,g,o,t,b,y) | fn(o,i,g,s,j,g,o,j) | fn(t,t,l,i,g,x,u,t) - fn(d,h,c,q,x,t,f,w) & fn(s,z,b,l,g,o,d,f) == fn(s,f,g,s,o,x,d,d))
solver.add(fn(y,j,j,o,w,d,q,h) & fn(n,i,i,q,z,t,g,s) + fn(c,t,v,t,w,y,s,u) & fn(d,i,f,f,h,l,n,l) - fn(t,h,h,w,o,h,w,n) == fn(x,s,v,u,o,j,t,x))
solver.add(fn(n,t,t,u,h,l,n,q) ^ fn(o,q,b,c,t,l,z,h) - fn(n,s,h,t,z,t,n,s) ^ fn(h,t,w,i,z,v,w,i) + fn(u,d,l,u,v,h,c,z) == fn(s,y,h,j,i,z,j,q))
solver.add(fn(b,y,t,w,y,c,j,u) ^ fn(w,w,n,n,n,q,b,w) & fn(y,z,d,g,o,t,b,y) + fn(y,j,j,o,w,d,q,h) - fn(n,t,t,u,h,l,n,q) == fn(f,j,i,v,u,c,t,i))
solver.add(fn(y,z,v,y,j,j,d,y) ^ fn(u,c,l,f,q,v,d,u) & fn(o,i,g,s,j,g,o,j) + fn(n,i,i,q,z,t,g,s) - fn(o,q,b,c,t,l,z,h) == fn(z,o,l,j,w,d,f,l))
solver.add(fn(v,u,g,l,j,t,y,n) ^ fn(o,n,c,y,c,b,x,h) & fn(t,t,l,i,g,x,u,t) + fn(c,t,v,t,w,y,s,u) - fn(n,s,h,t,z,t,n,s) == fn(s,u,g,v,q,g,w,w))
solver.add(fn(u,g,d,z,t,n,w,v) ^ fn(o,q,c,n,w,b,s,d) & fn(d,h,c,q,x,t,f,w) + fn(d,i,f,f,h,l,n,l) - fn(h,t,w,i,z,v,w,i) == fn(u,x,z,t,i,y,w,n))
solver.add(fn(x,b,f,z,i,o,z,y) ^ fn(c,g,y,o,y,f,j,g) & fn(s,z,b,l,g,o,d,f) + fn(t,h,h,w,o,h,w,n) - fn(u,d,l,u,v,h,c,z) == fn(j,q,x,i,z,z,x,q))

solver.add(c < 10, b < 10, d < 10, g < 10, f < 10, i < 10, h < 10, j < 10, l < 10, o < 10, n < 10)
solver.add(c >= 0, b >= 0, d >= 0, g >= 0, f >= 0, i >= 0, h >= 0, j >= 0, l >= 0, o >= 0, n >= 0)
solver.add(q < 10, s < 10, u < 10, t < 10, w < 10, v < 10, y < 10, x < 10, z < 10)
solver.add(q > 0, s >= 0, u >= 0, t >= 0, w >= 0, v >= 0, y >= 0, x >= 0, z >= 0)

print(solver.check())
print(solver.model())
