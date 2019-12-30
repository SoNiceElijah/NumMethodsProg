import numpy as np
import argparse

parser = argparse.ArgumentParser()
parser.add_argument('-n', type=int,   help='number of iterations')
parser.add_argument('-x', type=float, help='X0')
parser.add_argument('-s', type=float, help='step')
parser.add_argument('-a', type=str,  help='calculate automaticaly')
parser.add_argument('-m', type=float, help='maximum x value')
parser.add_argument('-e', type=float, help='epsilon')

args = parser.parse_args() 

auto = False
A    = np.array([[-500.005, 499.995], [499.995, -500.005]])
N    = 10000
x    = 0
xmax = 800
h    = 0.001
eps  = 0.00001

if args.n:
    N = args.n

if args.x:
    x = args.x

if args.s:
    h = args.s

if args.a:
    auto = False if 'False' == args.a else 'True'

if args.m:
    xmax = args.m

if args.e:
    eps = args.e
    
p = 2        # порядок метода

def U(x):
    a1    = np.array([[1], [1]])                  # вектор (1, 1)
    a2    = np.array([[1], [-1]])                 # вектор (1, -1)

    temp1 = np.multiply(10.0, a1)                 # умножение 10 на вектор (1, 1)
    temp1 = np.multiply(temp1, np.exp(-0.01 * x)) # умножение полученного вектора на 'e' в степени -0.01 * х

    temp2 = np.multiply(3.0, a2)                  # умножение 3 на вектор (1, -1)
    temp2 = np.multiply(temp2, np.exp(-1000 * x)) # умножение полученного вектора на 'e' в степени -1000 * х
    
    return np.subtract(temp1, temp2)              # вычитание из первого полученного вектора второго полученного вектора


def rk(v, h):
    E = np.array([[1, 0], [0, 1]]) # единичная матрица 2х2

    temp = np.multiply(h / 2, A)   # умножение половины шага на матрицу А
    temp = np.subtract(E, temp)    # вычитание из единичной матрицы полученной матрицы
    temp = np.linalg.inv(temp)     # обратная матрица
    temp = np.multiply(h, temp)    # умножение шага на обратную матрицу
    temp = temp.dot(A.dot(v))      # умножение матрицы А на вектор v

    return np.add(v, temp)         # сложение вектора v и полученного вектора temp


v = U(x)

print('i: ' + str(0) + ', ' \
      'x: ' + str(x) + ', ' \
      'h: ' + str(h) + ', ' \
      'v:' + '[' + str(float(v[0])) + ', ' + str(float(v[1])) + ']\n')

Hprev = h
Vstep = v.copy()
Vhalf = v.copy()
Vprev = v.copy()

i  = 1
c1 = 0
c2 = 0
max_dif = 0

while i <= N:
    Hprev = h
    Vstep = rk(Vprev, h)
    Vhalf = rk(Vprev, h * 0.5)
    Vhalf = rk(Vhalf, h * 0.5)
    S1 = float((Vhalf[0] - Vstep[0]) / (np.power(2, p) - 1))
    S2 = float((Vhalf[1] - Vstep[1]) / (np.power(2, p) - 1))
    S = S1 if np.abs(S1) > np.abs(S2) else S2

    e = U(x + h)
    x += h

    if np.abs(S) > eps:
        x -= h
        h *= 0.5
        c1 += 1
        continue
    elif np.abs(S) < (eps / np.power(2, p + 1)):
        c2 += 1
        h *= 2.0

    print('\n\ni: ' + str(i) + ', x: ' + str(x) + ', h: ' + str(Hprev) + ':\n')

    print('Решение, вычисленное неявным методом 2 порядка:\n') 
    print('\t' + '[' + str(float(Vstep[0])) + ', ' + str(float(Vstep[1])) + ']\n')
        
    print('Точное решение в точке х:\n')
    print('\t[' + str(float(e[0])) + ', ' + str(float(e[1])) + ']\n')    

    print('|V(x) - Vточ(x)|:\n')
    print('\t[' + str(abs(float(e[0]) - float(Vstep[0]))) + ', ' + str(abs(float(e[1]) - float(Vstep[1]))) + ']\n')    

    print('||V(x) - Vточ(x)||:\n')
    print('\t' + str(np.power(np.linalg.norm(np.subtract(e, Vstep)), 2)), '\n')

    print('S в точке x:\n')
    print('\t' + str(S), '\n')

    if not auto:
        string = input()
        if 'auto' == string:
            auto = True

    if x >= xmax:
        break

    Vprev = Vstep
    i += 1

    if np.power(np.linalg.norm(np.subtract(e, Vstep)), 2) > max_dif:
        max_dif = np.power(np.linalg.norm(np.subtract(e, Vstep)), 2)

print('\n\nmax||V(x) - Vточ(x)||:\n')
print('\t' + str(max_dif), '\n')
print('Уменьшений шага:', c1)
print('Увеличений шага:', c2)
print()

exit(0)
