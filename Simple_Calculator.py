#python calculator

operator = input ("Enter Operator (+ - * /): ")
num1 = float (input ("Enter 1st Number: "))
num2 = float (input ("Enter 2nd Number: "))

if operator == "+":
    result = num1 + num2
    print(round(result, 3))

elif operator == "-":
    result = num1 - num2
    print(round(result, 3))

elif operator == "*":
    result = num1 * num2
    print(round(result, 3))

elif operator == "/":
    if num2 != 0:
        result = num1 / num2
        print(round(result, 3))
    else:
        print("Error: Cannot divide by zero!")

else:
    print (f"{operator} is invalid! Please use (+, -, *, /) as an operator")
