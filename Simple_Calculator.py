#python calculator

operator = input ("Enter Operator (+ - * /): ")
num1 = float (input ("Enter 1st Number: "))
num2 = float (input ("Enter 2nd Number: "))

if operator == "+":
    result = num1 + num2
    print(result)

elif operator == "-":
    result = num1 - num2
    print(result)

elif operator == "*":
    result = num1 * num2
    print(result)

elif operator == "/":
    if num2 != 0:
        result = num1 / num2
        print(result)
    else:
        print("Error: Cannot divide by zero!")
